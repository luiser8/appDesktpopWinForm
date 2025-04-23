using System;
using InventoryApp.Data;
using System.Windows.Forms;
using InventoryApp.Models;
using System.Collections.Generic;
using System.Data;

namespace InventoryApp
{
    public partial class ProductDialog : Form
    {
        private readonly ProductManager _productManager;
        private readonly CategoryManager _categoryManager;
        private readonly int itemId; // Used for Edit mode

        public ProductDialog(ProductManager productManager, CategoryManager categoryManager)
        {
            InitializeComponent();
            _productManager = productManager;
            _categoryManager = categoryManager;

            // ComboBox Item
            RenderComboBoxCategory();

            // Set form title for Create mode
            Text = "Add New Product";
        }

        public void RenderComboBoxCategory()
        {
            var categoryItems = new List<string>();
            foreach (DataRow categoryRow in _categoryManager.GetCategories().Rows)
            {
                object value = categoryRow["CategoryItem"];
                if (value != DBNull.Value)
                {
                    categoryItems.Add(value.ToString());
                }
            }
            comboBox1.Items.AddRange(categoryItems.ToArray());
        }

        // Constructor for Edit mode
        public ProductDialog(ProductManager productManager, CategoryManager categoryManager, int id, string name, decimal price, int stock, int unit, string category)
        {
            InitializeComponent();
            _productManager = productManager;
            _categoryManager = categoryManager;
            itemId = id;

            textBox1.Text = name;
            textBox2.Text = price.ToString();
            textBox3.Text = stock.ToString();
            textBox4.Text = unit.ToString();
            comboBox1.Text = category;

            // ComboBox Items
            RenderComboBoxCategory();

            // Set form title for Edit mode
            Text = "Edit Product";
        }

        // Save Product
        private void SaveProduct()
        {
            if (itemId == 0) // Create mode
            {
                var newProduct = new Product
                {
                    Name = textBox1.Text,
                    Price = Convert.ToDecimal(textBox2.Text),
                    Stock = Convert.ToInt32(textBox3.Text),
                    Unit = Convert.ToInt32(textBox4.Text),
                    Category = comboBox1.Text
                };
                _productManager.InsertProduct(newProduct);
            }
            else // Edit mode
            {
                _productManager.UpdateProduct(new Product
                {
                    Id = itemId,
                    Name = textBox1.Text,
                    Price = Convert.ToDecimal(textBox2.Text),
                    Stock = Convert.ToInt32(textBox3.Text),
                    Unit = Convert.ToInt32(textBox4.Text),
                    Category = comboBox1.Text
                });
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        // SAVE or UPDATE BUTTON
        private void button1_Click(object sender, EventArgs e)
        {
            SaveProduct();
        }

        // CANCEL BUTTON
        private void button2_Click(object sender, EventArgs e)
        {
            errorProvider1.SetError(textBox1, "");
            errorProvider1.SetError(textBox2, "");
            errorProvider1.SetError(textBox3, "");
            errorProvider1.SetError(textBox4, "");
            errorProvider1.SetError(comboBox1, "");
            errorProvider1.Clear();
            Close();
        }

        //Textbox key press event
        #region
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    textBox3.Focus();
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
                    textBox4.Focus();
                    e.Handled = true;
                }
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox4.Text))
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    comboBox1.Focus();
                    e.Handled = true;
                }
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBox1.Text))
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    SaveProduct();
                    e.Handled = true;
                }
            }
        }
        #endregion

        //Texbox validations
        #region
        private void textBox1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "Product name is required.");
            }
            else
            {
                errorProvider1.SetError(textBox1, "");
                //errorProvider1.Clear();
            }
        }

        private void textBox3_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                errorProvider1.SetError(textBox3, "Price is required.");
            }
            else
            {
                errorProvider1.SetError(textBox3, "");
                //errorProvider1.Clear();
            }
        }

        private void textBox2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "Stock is required.");
            }
            else
            {
                errorProvider1.SetError(textBox2, "");
                //errorProvider1.Clear();
            }
        }

        private void textBox4_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox4.Text))
            {
                errorProvider1.SetError(textBox4, "Unit is required.");
            }
            else
            {
                errorProvider1.SetError(textBox4, "");
                //errorProvider1.Clear();
            }
        }

        private void comboBox1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                errorProvider1.SetError(comboBox1, "Category is required.");
            }
            else
            {
                errorProvider1.SetError(comboBox1, "");
                //errorProvider1.Clear();
            }
        }
        #endregion
    }
}
