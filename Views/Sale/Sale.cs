using System;
using System.Data;
using System.Windows.Forms;
using InventoryApp.Data;

namespace InventoryApp.InventoryApp.Views
{
    public partial class Sale : Form
    {
        private readonly CartManager cartManager;
        public Sale()
        {
            InitializeComponent();
            cartManager = new CartManager();
            DisplayCartItem();
            SetDatGridViewColumns();
        }

        private void SetDatGridViewColumns()
        {
            dataGridView1.Columns["ProductId"].HeaderText = "Id";
            dataGridView1.Columns["Name"].HeaderText = "Producto";
            dataGridView1.Columns["Price"].HeaderText = "Precio";
            dataGridView1.Columns["Quantity"].HeaderText = "Cantidad";
        }

        //FETCH DATA FROM CATEGORY DATABASE
        private void DisplayCartItem()
        {
            DataTable dt = cartManager.GetCartItems();
            dataGridView1.DataSource = dt;
        }

        //CHECKOUT BUTTON - Cart
        private void button1_Click(object sender, EventArgs e)
        {
            decimal totalPrice = cartManager.GetTotalPrice();
            if (totalPrice > 0)
            {
                Checkout dlg = new Checkout(totalPrice);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    DisplayCartItem();
                }
            }
            else
            {
                MessageBox.Show("El carrito está vacío.", "Carro vacío", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //ADD QUANTITY BYTTON - Cart
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0 && dataGridView1.SelectedRows.Count > 0)
            {
                int quantity = (int)dataGridView1.SelectedRows[0].Cells["Quantity"].Value;
                int productId = (int)dataGridView1.SelectedRows[0].Cells["ProductId"].Value;

                Quantity dlg = new Quantity(quantity, productId);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    DisplayCartItem();
                }
            }
            else
            {
                MessageBox.Show("El carrito está vacío.", "Carro vacío", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //REMOVE BUTTON - Cart
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int productId = (int)dataGridView1.SelectedRows[0].Cells["ProductId"].Value;

                if (MessageBox.Show("¿Estás seguro de que deseas eliminar este artículo de tu carrito?", "Advertencia!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    cartManager.RemoveCartItem(productId, "@ProductId");
                    DisplayCartItem();
                }
            }
            else
            {
                MessageBox.Show("El carrito está vacío.", "Carro vacío", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
