﻿using System.Data;
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
        }

        //FETCH DATA FROM TRANSACTION TABLE
        private void DisplayTransaction()
        {
            int currentUID = UserSession.SessionUID;
            
            var dt = transactionManager.SelectTransactionsAll(currentUID);
            dataGridView1.DataSource = dt;
            /*using (SqlConnection con = ConnectionManager.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Date, Subtotal, DiscountPercent, DiscountAmount, Total, Change, TransactionId FROM [Transactions] WHERE Uid = @Uid", con))
                {
                    cmd.Parameters.AddWithValue("@Uid", currentUID);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                con.Close();
            }*/
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
