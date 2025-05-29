using InventoryApp.Data;
using System.Windows.Forms;

namespace InventoryApp.Views.Usuarios
{
    public partial class UsuariosFormNewPassword : Form
    {
        private readonly AccountManager accountManager = new AccountManager();
        private readonly int _userId = 0;

        public UsuariosFormNewPassword(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (_userId > 0 && !string.IsNullOrEmpty(textBox2.Text))
            {
                accountManager.UpdatePassword(_userId, textBox2.Text);
            }
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
