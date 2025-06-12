using System;
using InventoryApp.Data;
using System.Windows.Forms;
using InventoryApp.Models;

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
        public CartDialog(CategoryManager manager, Category category)
        {
            InitializeComponent();
            categoryManager = manager;
            itemId = category.Id;
            textBox2.Text = category.CategoryItem;

            radioButton1.Checked = category.Status;
            radioButton2.Checked = !category.Status;

            Text = "Editar categoría";
        }

        // Save Category and Validate
        private void SaveCategory()
        {
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                if (itemId == 0) // Create mode
                {
                    categoryManager.AddCategory(new Category { CategoryItem = textBox2.Text, Status = radioButton1.Checked });
                }
                else // Edit mode
                {
                    categoryManager.UpdateCategory(new Category { Id = itemId, CategoryItem = textBox2.Text, Status = radioButton1.Checked });
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
