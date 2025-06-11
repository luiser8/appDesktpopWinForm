using InventoryApp.Data;
using InventoryApp.InventoryApp.dlg;
using InventoryApp.InventoryApp.Views;
using InventoryApp.Views;
using InventoryApp.Views.Dashboard;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace InventoryApp.InventoryApp
{
    public partial class MainView : Form
    {
        private Form currentForm;
        private readonly RolPolicyManager _rolPolicyManager = new RolPolicyManager();

        public MainView(UsuarioResponse usuario)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            ActivateViewRolPolicy(usuario);

            SwitchForm(new Dashboard());
            DateTimeOffset nowWithOffset = DateTimeOffset.Now;

            statusStrip1.ShowItemToolTips = true;
            statusStrip1.Items.Clear();
            statusStrip1.Items.Add($"Bienvenido {usuario.FirstName} {usuario.LastName} | Rol: {usuario.RolName} | Version: {ConfigurationManager.AppSettings["AppVersion"].ToString()} | {ConfigurationManager.AppSettings["companyAddress"].ToString()} {nowWithOffset.ToString("yyyy-MM-dd")}");

            itemCountTimer = new Timer
            {
                Interval = 1000
            };
            itemCountTimer.Tick += itemCountTimer_Tick;
            itemCountTimer.Start();
        }

        private void ActivateViewRolPolicy(UsuarioResponse usuario)
        {
            var rolPolicies = _rolPolicyManager.SelectRolPolicyByUserl(usuario.Id);
            foreach (var policy in rolPolicies)
            {
                switch (policy.Policy)
                {
                    case "Productos":
                        radioButton1.Visible = true;
                        break;
                    case "Categorias":
                        radioButton2.Visible = true;
                        break;
                    case "Ventas":
                        radioButton3.Visible = true;
                        break;
                    case "Transacciones":
                        radioButton4.Visible = true;
                        break;
                    case "Inicio":
                        radioButton5.Visible = true;
                        break;
                    case "Usuarios":
                        radioButton6.Visible = true;
                        break;
                }
            }
        }

        //NAVIGATION CONTROL
        private void SwitchForm(Form newForm)
        {
            if (currentForm != null && !currentForm.IsDisposed)
            {
                currentForm.Hide();
            }

            newForm.TopLevel = false;
            newForm.FormBorderStyle = FormBorderStyle.None;
            newForm.Dock = DockStyle.Fill;

            panel2.Controls.Clear();
            panel2.Controls.Add(newForm);
            panel2.Refresh();

            newForm.Show();
            currentForm = newForm;
        }

        // DASHBOARD
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                SwitchForm(new Dashboard());
            }
        }

        //HOME TAB
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                SwitchForm(new ProductView());
            }
        }

        //CATEGORY TAB
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                SwitchForm(new CategoryView());
            }
        }

        //CART TAB
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                SwitchForm(new Sale());
            }
        }

        //TRANSACTION TAB
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                SwitchForm(new Transaction());
            }
        }

        // LOGOUT BUTTON
        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea cerrar sesion?", "Advertencia!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                UserAuth userauth = new UserAuth();
                userauth.FormClosed += (s, args) => Close();
                userauth.Show();
                Hide();
            }
        }

        // CART COUNTER
        private void itemCountTimer_Tick(object sender, EventArgs e)
        {
            CartManager cartManager = new CartManager();
            int cartItemCount = cartManager.GetCartItemCount();
            radioButton3.Text = "Ventas (" + cartItemCount.ToString() + ")";
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked)
            {
                SwitchForm(new UsuariosView());
            }
        }

        private void MainView_Load(object sender, EventArgs e)
        {
            Text = $"{ConfigurationManager.AppSettings["AppType"].ToString()} - {ConfigurationManager.AppSettings["companyName"].ToString()}, {ConfigurationManager.AppSettings["companyRif"].ToString()}";
        }
    }
}
