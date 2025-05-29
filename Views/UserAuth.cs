using InventoryApp.Data;
using InventoryApp.InventoryApp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;

namespace InventoryApp.Views
{
    public partial class UserAuth : Form
    {
        private readonly AccountManager accountManager;
        private readonly int attempsValue = Convert.ToInt16(ConfigurationManager.AppSettings["attempts"].ToString());
        private static readonly Dictionary<string, int> attempsDic = new Dictionary<string, int>();
        private bool isBlocked = false;

        public UserAuth()
        {
            InitializeComponent();
            accountManager = new AccountManager();
            label3.Text = ConfigurationManager.AppSettings["companyName"].ToString();
            label4.Text = $"Rif: {ConfigurationManager.AppSettings["companyRif"].ToString()}";
            label5.Text = $"Localidad: {ConfigurationManager.AppSettings["companyAddress"].ToString()}";
            label6.Text = $"Version: {ConfigurationManager.AppSettings["AppVersion"].ToString()}";
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

            if (usuarioSession != null && usuarioSession.Status)
            {
                UserSession.SessionUID = usuarioSession.Id;
                if (attempsDic.ContainsKey(username))
                    attempsDic.Remove(username);
                MainView mainpage = new MainView(usuarioSession);
                mainpage.FormClosed += (s, args) => Close();
                mainpage.Show();
                Hide();
            }
            else if (usuarioSession != null && !usuarioSession.Status)
            {
                if (attempsDic.ContainsKey(username))
                    attempsDic[username]++;
                else
                    attempsDic[username] = 1;
                MessageBox.Show($"Usuario se encuentra inhabilitado. Comuniquese con su Administrador..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (attempsDic.ContainsKey(username))
                    attempsDic[username]++;
                else
                    attempsDic[username] = 1;
                MessageBox.Show($"Nombre de usuario o contraseña no válidos. Inténtalo de nuevo, Intentos: {attempsDic[username]}..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (attempsDic.ContainsKey(username) && attempsDic[username] >= attempsValue)
            {
                if (!isBlocked)
                {
                    accountManager.UpdateUserStatus(username, false);
                    isBlocked = true;
                }
                if (attempsDic.ContainsKey(username))
                {
                    attempsDic.Remove(username);
                    isBlocked = false;
                }
                MessageBox.Show($"Usuario bloqueado por intentos({attempsValue}) fallidos..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void UserAuth_Load(object sender, EventArgs e)
        {
            Text = $"Inicio de sesiòn en {ConfigurationManager.AppSettings["companyName"].ToString()}";
        }
    }
}
