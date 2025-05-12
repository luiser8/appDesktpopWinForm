using InventoryApp.Data;
using System;
using System.Windows.Forms;

namespace InventoryApp.Views.Usuarios
{
    public partial class UsuariosView : Form
    {
        private readonly AccountManager accountManager = new AccountManager();

        public UsuariosView()
        {
            InitializeComponent(); 
            var data = accountManager.GetAllUsuarios();
            dataGridView1.DataSource = data;
            dataGridView1.Dock = DockStyle.Fill;

            // O alternativamente, usar Anchor (para márgenes fijos)
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                                 | AnchorStyles.Left | AnchorStyles.Right;
        }

        private void UsuariosView_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            UsuariosForm usuariosForm = new UsuariosForm();
            if(usuariosForm.ShowDialog() == DialogResult.OK)
                usuariosForm.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
