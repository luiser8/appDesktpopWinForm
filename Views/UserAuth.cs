using InventoryApp.InventoryApp;
using InventoryApp.Data;
using System.Windows.Forms;
using System;

namespace InventoryApp.Views
{
    public partial class UserAuth : Form
    {
        private readonly AccountManager accountManager;

        public UserAuth()
        {
            InitializeComponent();
            accountManager = new AccountManager();
        }

        // Validate Users Credentials
        private UsuarioResponse ValidateUserCredentials(string username, string password)
        {
            return accountManager.ValidateUserCredentials(username, password);
        }

        // Process Login Form
        private void ProcessLoginForm()
        {
            errorProvider1.Clear();

            string username = textBox1.Text;
            string password = textBox2.Text;


                    var usuarioSession = ValidateUserCredentials(username, password);
            if (usuarioSession != null)
            {
                UserSession.SessionUID = usuarioSession.Id;
                MainView mainpage = new MainView(username, usuarioSession.RolName);
                mainpage.FormClosed += (s, args) => this.Close();
                mainpage.Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Login or Register Button
        private void button1_Click(object sender, EventArgs e)
        {
            ProcessLoginForm();
        }

        // Show password checkbox
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
        }

        //TextBox key press event
        #region
        // Event handler for key press event in textBox1
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    textBox2.Focus();
                    e.Handled = true;
                }
            }
        }

        // Event handler for key press event in textBox2
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    ProcessLoginForm();
                    e.Handled = true;
                }
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    ProcessLoginForm();
                    e.Handled = true;
                }
            
        }
        #endregion
    }
}
