using InventoryApp.Data;
using System.Windows.Forms;

namespace InventoryApp.InventoryApp.dlg
{
    public partial class Details : Form
    {
        private readonly OrdersManager _ordersManager = new OrdersManager();
        public Details(string id)
        {
            InitializeComponent();
            DisplayTransactionItems(id);
            SetDatGridViewColumns();
        }

        private void SetDatGridViewColumns()
        {
            dataGridView1.Columns["Name"].HeaderText = "Producto";
            dataGridView1.Columns["Price"].HeaderText = "Precio";
            dataGridView1.Columns["Quantity"].HeaderText = "Cantidad";
        }

        //FETCH DATA FROM ORDERS TABLE
        private void DisplayTransactionItems(string transactionId)
        {
            if (string.IsNullOrEmpty(transactionId))
            {
                MessageBox.Show("El ID de transacción no puede ser nulo o vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            dataGridView1.DataSource = _ordersManager.GetOrdersByTransactionId(transactionId);
        }
    }
}
