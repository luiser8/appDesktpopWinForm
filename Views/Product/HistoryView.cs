﻿using InventoryApp.Data;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace InventoryApp.InventoryApp.dlg
{
    public partial class HistoryView : Form
    {
        private readonly HistoryManager _historyManager = new HistoryManager();
        private readonly int _productId;
        public HistoryView(int id)
        {
            InitializeComponent();
            _productId = id;
            DisplayHistory();
        }

        private void SetDatGridViewColumns(DataTable dataTable)
        {
            dataGridView1.DataSource = dataTable;
            dataGridView1.Columns["ProductId"].Visible = false;
            dataGridView1.Columns["Status"].Visible = false;
        }

        //FETCH DATA FROM HISTORY TABLE
        private void DisplayHistory()
        {
            if (_productId <= 0)
            {
                MessageBox.Show("ID de producto no válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SetDatGridViewColumns(_historyManager.SelectHistory(_productId));
        }
    }
}
