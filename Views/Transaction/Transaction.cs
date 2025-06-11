using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using InventoryApp.Data;

namespace InventoryApp.InventoryApp.dlg
{
    public partial class Transaction : Form
    {
        private readonly TransactionManager transactionManager;
        public Transaction()
        {
            InitializeComponent();
            transactionManager = new TransactionManager();
            DisplayTransaction();
            SetDatGridViewColumns();
        }

        //FETCH DATA FROM TRANSACTION TABLE
        private void DisplayTransaction()
        {
            int currentUID = UserSession.SessionUID;
            
            var dt = transactionManager.SelectTransactionsAll(currentUID);
            dataGridView1.DataSource = dt;
        }

        private void SetDatGridViewColumns()
        {
            dataGridView1.Columns["TransactionId"].HeaderText = "Id";
            dataGridView1.Columns["Date"].HeaderText = "Fecha";
            dataGridView1.Columns["Subtotal"].HeaderText = "Sub Total";
            dataGridView1.Columns["DiscountPercent"].HeaderText = "Porcentaje descuento";
            dataGridView1.Columns["DiscountAmount"].HeaderText = "Porcentaje Cantidad";
            dataGridView1.Columns["Change"].HeaderText = "Cambio";
        }

        //CELL DOUBLE CLICK EVENT FOR OPENING DETAILS
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string id = (string)dataGridView1.SelectedRows[0].Cells["TransactionId"].Value;
                Details dlg = new Details(id);
                dlg.ShowDialog();
            }
        }

        private void Transaction_Load(object sender, System.EventArgs e)
        {

        }
    }
}
