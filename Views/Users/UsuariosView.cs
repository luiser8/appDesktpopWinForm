using InventoryApp.Data;
using InventoryApp.Models;
using InventoryApp.Views.Audit;
using InventoryApp.Views.Usuarios;
using System;
using System.Data;
using System.Windows.Forms;

namespace InventoryApp.InventoryApp.Views
{
    public partial class UsuariosView : Form
    {
        private readonly AccountManager accountManager = new AccountManager();

        public UsuariosView()
        {
            InitializeComponent();
            SetDatGridViewColumns(accountManager.GetAllUsuarios(UserSession.SessionUID));
        }

        private void SetDatGridViewColumns(DataTable dataTable)
        {
            dataGridView1.DataSource = dataTable;
            dataGridView1.Columns["RolId"].Visible = false;
            dataGridView1.Columns["Password"].Visible = false;
            dataGridView1.Columns["Status"].Visible = false;

            dataGridView1.Columns["FirstName"].HeaderText = "Nombres";
            dataGridView1.Columns["LastName"].HeaderText = "Apellidos";
            dataGridView1.Columns["Username"].HeaderText = "Usuario";
            dataGridView1.Columns["CreatedAt"].HeaderText = "Creacion";
            dataGridView1.Columns["Email"].HeaderText = "Correo";
            dataGridView1.Columns["RolName"].HeaderText = "Rol";
            dataGridView1.Columns["StatusString"].HeaderText = "Estado";
        }

        //ADD BUTTON - Usuario
        private void button1_Click(object sender, System.EventArgs e)
        {
            UsuariosForm dlg = new UsuariosForm();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                SetDatGridViewColumns(accountManager.GetAllUsuarios(UserSession.SessionUID));
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
                    FirstName = row.Cells["FirstName"].Value.ToString(),
                    LastName = row.Cells["LastName"].Value.ToString(),
                    RolName = row.Cells["RolName"].Value.ToString(),
                    UserName = row.Cells["UserName"].Value.ToString(),
                    Password = row.Cells["Password"].Value.ToString(),
                    Email = row.Cells["Email"].Value.ToString(),
                    Status = Convert.ToInt32(row.Cells["Status"].Value) == 1,
                });
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    SetDatGridViewColumns(accountManager.GetAllUsuarios(UserSession.SessionUID));
                }
            }
            else
            {
                MessageBox.Show("No hay usuario disponible para editar.", "Usuario vacío",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //DELETE BUTTON - User
        private void button3_Click(object sender, System.EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;

                if (MessageBox.Show("¿Está seguro que desea eliminar este usuario?", "¡Advertencia!",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    accountManager.DeleteUser(id);
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione un usuario para eliminar.", "Usuario vacío",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UsuariosView_Load(object sender, System.EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;
                var viewAudit = new AuditView(new AuditUser
                {
                    Id = id
                });

                viewAudit.ShowDialog();
            }
            else
            {
                MessageBox.Show("Por favor seleccione un usuario para ver la auditoría.", "Usuario vacío",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var formNewPassword = new UsuariosFormNewPassword((int)dataGridView1.SelectedRows[0].Cells["Id"].Value);
                formNewPassword.ShowDialog();
            }
            else
            {
                MessageBox.Show("Por favor seleccione un usuario para cambiar la contraseña.", "Usuario vacío",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
