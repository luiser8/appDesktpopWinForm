using System;
using System.Data;
using InventoryApp.Data;
using System.Windows.Forms;
using InventoryApp.InventoryApp.dlg;
using InventoryApp.Utility;

namespace InventoryApp
{
    public partial class ProductView : Form
    {
        private readonly ProductManager productManager;
        private readonly CategoryManager categoryManager;
        private readonly CartManager cartManager;

        public ProductView()
        {
            InitializeComponent();
            productManager = new ProductManager();
            categoryManager = new CategoryManager();
            cartManager = new CartManager();
            dataGridView1.DataSource = productManager.SelectProductsAll();
            AddToCart();
        }

        //SEARCH AND DISPLAY RESULTS
        private void PerformSearch()
        {
            DataTable dt = productManager.SearchProducts(textBox1.Text);
            dataGridView1.DataSource = dt;
        }

        //SEARCH BUTTON
        private void button6_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        //IF USER PRESS ENTER KEY
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                PerformSearch();
                e.Handled = true;
            }
        }

        //RESET DATAGRIDVIEW IF TEXTBOX IS EMPTY
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                PerformSearch();
            }
        }

        //INSERT BUTTON - Home
        private void button1_Click(object sender, EventArgs e)
        {
            ProductDialog dlg = new ProductDialog(productManager, categoryManager);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                dataGridView1.DataSource = productManager.SelectProductsAll();
            }
        }

        //UPDATE BUTTON - Home
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the data from the selected row
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                int id = (int)row.Cells["Id"].Value;
                string name = row.Cells["name"].Value.ToString();
                decimal price = (decimal)row.Cells["price"].Value;
                int stock = (int)row.Cells["stock"].Value;
                int unit = (int)row.Cells["unit"].Value;
                string category = row.Cells["category"].Value.ToString();

                // Pass the data to EditDialog
                ProductDialog dlg = new ProductDialog(productManager, categoryManager, id, name, price, stock, unit, category);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    dataGridView1.DataSource = productManager.SelectProductsAll();
                }
            }
            else
            {
                MessageBox.Show("No product is available for editing.", "Empty!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //DELETE BUTTON - Home
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;

                if (MessageBox.Show("Are you sure want to delete this item?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    productManager.DeleteProduct(id);
                    dataGridView1.DataSource = productManager.SelectProductsAll();
                }
            }
            else
            {
                MessageBox.Show("Please select a product to delete.", "Empty!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //ADD STOCKS BUTTON - Home
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string name = dataGridView1.SelectedRows[0].Cells["name"].Value.ToString();
                int productId = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;
                AddStock dlg = new AddStock(productId, name);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    dataGridView1.DataSource = productManager.SelectProductsAll();
                }
            }
            else
            {
                MessageBox.Show("No products are available for adding stock.", "Empty!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //HISTORY BUTTON - Home
        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;
                History historyForm = new History(id);
                historyForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("No product history is available.", "Empty!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //ADD_TO_CART DATAGRID BUTTON - Home
        private void AddToCart()
        {
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn
            {
                Text = "Add",
                UseColumnTextForButtonValue = true
            };
            dataGridView1.Columns.Add(buttonColumn);
            dataGridView1.CellContentClick -= dataGridView1_CellContentClick;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
        }

        //DATAGRIDVIEW BUTTON EVENT - Home
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                // Get the values from the selected row
                int productId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);
                string name = dataGridView1.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                decimal price = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["Price"].Value);
                int stock = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Stock"].Value);

                // Add item to the cart
                if (stock > 0)
                {
                    bool itemAdded = cartManager.AddItemToCart(new Models.Cart { ProductId = productId, Name = name, Price = price, Quantity = 1 });
                    if (itemAdded)
                    {
                        MessageBox.Show("Product added to cart.");
                    }
                }
                else
                {
                    MessageBox.Show("Product out of stock.");
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                ExcelGeneration.ExportAndOpen(productManager.SelectProductsAll(), "Productos");
                MessageBox.Show("Data exported successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }

        }
    }
}
