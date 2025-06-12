using System;
using System.Data;
using InventoryApp.Data;
using System.Windows.Forms;
using InventoryApp.InventoryApp.dlg;
using InventoryApp.Utility;
using InventoryApp.Models;

namespace InventoryApp
{
    public partial class ProductView : Form
    {
        private readonly ProductManager productManager = new ProductManager();
        private readonly CategoryManager categoryManager = new CategoryManager();
        private readonly CartManager cartManager = new CartManager();

        public ProductView()
        {
            InitializeComponent();
            dataGridView1.DataSource = productManager.SelectProductsAll();
            AddToCart();
            SetDatGridViewColumns();
        }

        private void SetDatGridViewColumns()
        {
            dataGridView1.Columns["Name"].HeaderText = "Nombre";
            dataGridView1.Columns["Price"].HeaderText = "Precio";
            dataGridView1.Columns["Unit"].HeaderText = "Unidad";
            dataGridView1.Columns["Category"].HeaderText = "Categoria";
            dataGridView1.Columns["StatusString"].HeaderText = "Estado";
            dataGridView1.Columns["CreatedAt"].HeaderText = "Creacion";
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

                var product = new Product
                {
                    Id = (int)row.Cells["Id"].Value,
                    Name = row.Cells["name"].Value.ToString(),
                    Price = (decimal)row.Cells["price"].Value,
                    Stock = (int)row.Cells["stock"].Value,
                    Unit = (int)row.Cells["unit"].Value,
                    Category = row.Cells["category"].Value.ToString(),
                    Status = Convert.ToString(row.Cells["StatusString"].Value) == "Activo"
                };

                // Pass the data to EditDialog
                ProductDialog dlg = new ProductDialog(productManager, categoryManager, product);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    dataGridView1.DataSource = productManager.SelectProductsAll();
                }
            }
            else
            {
                MessageBox.Show("No hay ningún producto disponible para editar.", "Vacio!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //DELETE BUTTON - Home
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;

                if (MessageBox.Show("¿Estás segura de que quieres eliminar este artículo?", "Advertencia!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    productManager.DeleteProduct(id);
                    dataGridView1.DataSource = productManager.SelectProductsAll();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un producto para eliminar.", "Vacio!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("No hay productos disponibles para agregar stock.", "Vacio!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //HISTORY BUTTON - Home
        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;
                HistoryView historyForm = new HistoryView(id);
                historyForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("No hay ningún historial de producto disponible.", "Vacio!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //ADD_TO_CART DATAGRID BUTTON - Home
        private void AddToCart()
        {
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn
            {
                Text = "Agregar",
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
                    bool itemAdded = cartManager.AddItemToCart(new Cart { ProductId = productId, Name = name, Price = price, Quantity = 1 });
                    if (itemAdded)
                    {
                        MessageBox.Show("Producto añadido al carrito.");
                    }
                }
                else
                {
                    MessageBox.Show("Producto agotado.");
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                ExcelGeneration.ExportAndOpen(productManager.SelectProductsAll(), "Productos");
                MessageBox.Show("Datos exportados exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }

        }
    }
}
