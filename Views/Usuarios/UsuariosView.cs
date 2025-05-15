using System.Windows.Forms;
using InventoryApp.Data;
using InventoryApp.Views.Usuarios;

namespace InventoryApp.InventoryApp.Views
{
    public partial class UsuariosView : Form
    {
        private readonly AccountManager accountManager = new AccountManager();
        public UsuariosView()
        {
            InitializeComponent();
            var data = accountManager.GetAllUsuarios();
            dataGridView1.DataSource = data;
        }

        //ADD BUTTON - Usuario
        private void button1_Click(object sender, System.EventArgs e)
        {
            UsuariosForm dlg = new UsuariosForm();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                dataGridView1.DataSource = accountManager.GetAllUsuarios();
            }
        }

        //UPDATE BUTTON - Usuarios
        private void button2_Click(object sender, System.EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the data from the selected row
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                int id = (int)row.Cells["Id"].Value;

                // Pass the data to EditUser
                UsuariosForm dlg = new UsuariosForm(new Usuario
                {
                    Id = id,
                    RolName = row.Cells["RolName"].Value.ToString(),
                    UserName = row.Cells["UserName"].Value.ToString(),
                    Password = row.Cells["Password"].Value.ToString(),
                    Email = row.Cells["Email"].Value.ToString()
                });
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    dataGridView1.DataSource = accountManager.GetAllUsuarios();
                }
            }
            else
            {
                MessageBox.Show("No user is available for editing.", "Empty User",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //DELETE BUTTON - User
        private void button3_Click(object sender, System.EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = (int)dataGridView1.SelectedRows[0].Cells["ID"].Value;

                if (MessageBox.Show("Are you sure want to delete this user?", "Warning!",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    accountManager.DeleteUser(id);
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                }
            }
            else
            {
                MessageBox.Show("Please select a user to delete.", "Empty User",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UsuariosView_Load(object sender, System.EventArgs e)
        {

        }
    }
}
