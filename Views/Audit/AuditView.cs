using InventoryApp.Data;
using InventoryApp.Models;
using System;
using System.Data;
using System.Windows.Forms;

namespace InventoryApp.Views.Audit
{
    public partial class AuditView : Form
    {
        private readonly AuditManager auditManager = new AuditManager();

        public AuditView(AuditUser audit)
        {
            InitializeComponent();
            SetDatGridViewColumns(auditManager.SelectAudit(audit.Id));
        }

        private void AuditView_Load(object sender, EventArgs e)
        {

        }

        private void SetDatGridViewColumns(DataTable dataTable)
        {
            dataGridView1.DataSource = dataTable;
            dataGridView1.Columns["UserId"].Visible = false;
        }
    }
}
