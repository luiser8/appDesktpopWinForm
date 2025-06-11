using System;
using InventoryApp.Data;
using System.Windows.Forms;

namespace InventoryApp
{
    public partial class CartDialog : Form
    {
        private readonly CategoryManager categoryManager;
        private readonly int itemId;

        public CartDialog(CategoryManager manager)
        {
            InitializeComponent();
            categoryManager = manager;

            Text = "Agregar nueva categoría";
        }

        // Constructor for Edit mode - Cat
        public CartDialog(CategoryManager manager, int id, string categoryItem)
        {
            InitializeComponent();
            categoryManager = manager;
            itemId = id;
            textBox2.Text = categoryItem;

            Text = "Editar categoría";
        }

        // Save Category and Validate
        private void SaveCategory()
        {
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                if (itemId == 0) // Create mode
                {
                    categoryManager.AddCategory(new Models.Category { CategoryItem = textBox2.Text });
                }
                else // Edit mode
                {
                    categoryManager.UpdateCategory(new Models.Category { Id = itemId, CategoryItem = textBox2.Text });
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                errorProvider1.SetError(textBox2, "El nombre de la categoría es obligatorio.");
            }
        }

        // SAVE or UPDATE BUTTON - Cat
        private void button1_Click(object sender, EventArgs e)
        {
            SaveCategory();
        }

        // [ ENTER ] Keypress to save
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SaveCategory();
                e.Handled = true;
            }
        }

        // CANCEL BUTTON - Cat
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
