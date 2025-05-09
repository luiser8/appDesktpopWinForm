﻿using InventoryApp.InventoryApp;
using InventoryApp.Data;
using System.Windows.Forms;
using System;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Collections.Generic;
using System.Data;

namespace InventoryApp.Views
{
    public partial class UserAuth : Form
    {
        private bool isRegisterMode = false;
        private readonly AccountManager accountManager;
        private readonly RolManager rolManager;

        public UserAuth()
        {
            InitializeComponent();
            accountManager = new AccountManager();
            rolManager = new RolManager();
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

        // Validate Users Credentials
        private UsuarioResponse ValidateUserCredentials(string username, string password)
        {
            return accountManager.ValidateUserCredentials(username, password);
        }

        // Process Login Form
        private void ProcessLoginForm()
        {
            errorProvider1.Clear();

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
                if (isRegisterMode)
                {
                    RegisterUser(new Usuario
                    {
                        RolName = rolName,
                        UserName = username,
                        Password = password,
                        Email = email,
                    });
                }
                else
                {
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
            }
        }

        // Toggle label mode
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            isRegisterMode = !isRegisterMode; // Toggle the mode

            if (isRegisterMode)
            {
                // Switch to register mode
                RenderComboBoxRoles();
                errorProvider1.Clear();
                textBox3.Visible = true;
                textBox1.Text = string.Empty;
                textBox2.Text = string.Empty;
                textBox3.Text = string.Empty;
                comboBox1.Visible = true;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                linkLabel1.Text = "LOGIN";
                label3.Visible = true;
                label4.Visible = true;
                button1.Text = "REGISTER";
            }
            else
            {
                // Switch to login mode
                errorProvider1.Clear();
                textBox3.Visible = false;
                textBox1.Text = string.Empty;
                textBox2.Text = string.Empty;
                textBox3.Text = string.Empty;
                comboBox1.Visible = false;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                label3.Visible = false;
                label4.Visible = false;
                linkLabel1.Text = "REGISTER";
                button1.Text = "LOGIN";
            }
        }

        // Login or Register Button
        private void button1_Click(object sender, EventArgs e)
        {
            ProcessLoginForm();
        }

        // Register new user
        private void RegisterUser(Usuario usuario)
        {
            accountManager.RegisterUser(usuario);
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
            if (!string.IsNullOrEmpty(textBox3.Text))
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    ProcessLoginForm();
                    e.Handled = true;
                }
            }
        }
        #endregion
    }
}
