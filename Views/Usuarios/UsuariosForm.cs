using InventoryApp.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace InventoryApp.Views.Usuarios
{
    public partial class UsuariosForm : Form
    {
        private readonly RolManager rolManager = new RolManager();
        private readonly AccountManager accountManager = new AccountManager();

        public UsuariosForm()
        {
            InitializeComponent();
            RenderComboBoxRoles();
        }

        public void RenderComboBoxRoles()
        {
            var rolItems = new List<string>();
            foreach (DataRow categoryRow in rolManager.SelectRolAll().Rows)
            {
                object rolName = categoryRow["RolName"];
                if (rolName != DBNull.Value)
                {
                    rolItems.Add(rolName.ToString());
                }
            }
            comboBox1.Items.AddRange(rolItems.ToArray());
        }

        private void RegisterUser(Usuario usuario)
        {
            accountManager.RegisterUser(usuario);
        }

        private void ProcessLoginForm()
        {
            string rolName = comboBox1.Text;
            string username = textBox1.Text;
            string password = textBox2.Text;
            string email = textBox3.Text;

            bool isValid = true;

            if (string.IsNullOrEmpty(username))
            {
                errorProvider1.SetError(textBox1, "Please enter a username.");
                isValid = false;
            }

            if (string.IsNullOrEmpty(password))
            {
                errorProvider1.SetError(textBox2, "Please enter a password.");
                isValid = false;
            }

            if (isValid)
            {
                    RegisterUser(new Usuario
                    {
                        RolName = rolName,
                        UserName = username,
                        Password = password,
                        Email = email,
                    });
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void UsuariosForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProcessLoginForm();
        }
    }
}
