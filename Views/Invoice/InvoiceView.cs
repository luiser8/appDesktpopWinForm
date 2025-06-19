using InventoryApp.Data;
using InventoryApp.Utility;
using System;
using System.Windows.Forms;

namespace InventoryApp.InventoryApp.dlg
{
    public partial class InvoiceView : Form
    {
        private readonly InvoiceManager invoiceManager = new InvoiceManager();
        public InvoiceView()
        {
            InitializeComponent();
            DisplayInvoices();
            SetDatGridViewColumns();
        }

        //FETCH DATA FROM TRANSACTION TABLE
        private void DisplayInvoices()
        {
            int currentUID = UserSession.SessionUID;
            
            var dt = invoiceManager.SelectInvoiceAll(currentUID);
            dataGridView1.DataSource = dt;
        }

        private void SetDatGridViewColumns()
        {
            dataGridView1.Columns["InvoiceId"].HeaderText = "Id";
            dataGridView1.Columns["PayMethod"].HeaderText = "Metodo de pago";
            dataGridView1.Columns["Bank"].HeaderText = "Banco";
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
                string id = (string)dataGridView1.SelectedRows[0].Cells["InvoiceId"].Value;
                Details dlg = new Details(id);
                dlg.ShowDialog();
            }
        }

        private void Transaction_Load(object sender, System.EventArgs e)
        {

        }

        private void button8_Click(object sender, System.EventArgs e)
        {
            try
            {
                ExcelGeneration.ExportAndOpen(invoiceManager.SelectReportsInvoice(), "Facturas");
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
