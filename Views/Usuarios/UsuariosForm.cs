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
        private readonly int userId = 0;

        public UsuariosForm()
        {
            InitializeComponent();
            RenderComboBoxRoles();
        }

        public UsuariosForm(Usuario usuario)
        {
            InitializeComponent();
            RenderComboBoxRoles();
            userId = usuario.Id;
            textBox4.Text = usuario.FirstName;
            textBox5.Text = usuario.LastName;
            textBox1.Text = usuario.UserName;
            textBox2.Text = usuario.Password;
            textBox3.Text = usuario.Email;
            comboBox1.Text = usuario.RolName;
            radioButton1.Checked = usuario.Status;
            radioButton2.Checked = !usuario.Status;
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

        private void SaveUser(Usuario usuario)
        {
            if(usuario.Id == 0) // Create mode
            {
                accountManager.RegisterUser(usuario);
            }
            else // Edit mode
            {
                accountManager.UpdateUser(usuario);
            }
        }

        private void ProcessLoginForm()
        {
            string firstName = textBox4.Text;
            string lastName = textBox5.Text;
            string rolName = comboBox1.Text;
            string username = textBox1.Text;
            string password = textBox2.Text;
            string email = textBox3.Text;
            bool status = radioButton1.Checked;

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
                    SaveUser(new Usuario
                    {
                        Id = userId,
                        FirstName = firstName,
                        LastName = lastName,
                        RolName = rolName,
                        UserName = username,
                        Password = password,
                        Email = email,
                        Status = status
                    });
            }

            DialogResult = DialogResult.OK;
            Close();
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

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
