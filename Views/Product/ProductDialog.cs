﻿using DocumentFormat.OpenXml.Bibliography;
using InventoryApp.Data;
using InventoryApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

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
                object statusValue = categoryRow["StatusString"];
                if (value != DBNull.Value && statusValue.ToString() != "Inactivo")
                {
                    categoryItems.Add(value.ToString());
                }
            }
            comboBox1.Items.AddRange(categoryItems.ToArray());
        }

        // Constructor for Edit mode
        public ProductDialog(ProductManager productManager, CategoryManager categoryManager, Product product)
        {
            InitializeComponent();
            _productManager = productManager;
            _categoryManager = categoryManager;
            itemId = product.Id;

            textBox1.Text = product.Name;
            textBox2.Text = product.Price.ToString();
            textBox3.Text = product.Stock.ToString();
            textBox4.Text = product.Unit.ToString();
            comboBox1.Text = product.Category;

            radioButton1.Checked = product.Status;
            radioButton2.Checked = !product.Status;

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
                    Category = comboBox1.Text,
                    Status = radioButton1.Checked
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
                    Category = comboBox1.Text,
                    Status = radioButton1.Checked
                });
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        // SAVE or UPDATE BUTTON
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && comboBox1.Text != "" && radioButton1.Checked || radioButton2.Checked)
            {
                SaveProduct();
            } else
            {
                MessageBox.Show("Error! debes llenar todos los campos");
            }
            
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
                errorProvider1.SetError(textBox1, "El nombre del producto es obligatorio.");
            }
            else
            {
                errorProvider1.SetError(textBox1, "");
            }
        }

        private void textBox3_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                errorProvider1.SetError(textBox3, "Se requiere precio.");
            }
            else
            {
                errorProvider1.SetError(textBox3, "");
            }
        }

        private void textBox2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "Se requiere stock.");
            }
            else
            {
                errorProvider1.SetError(textBox2, "");
            }
        }

        private void textBox4_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox4.Text))
            {
                errorProvider1.SetError(textBox4, "Se requiere unidad.");
            }
            else
            {
                errorProvider1.SetError(textBox4, "");
            }
        }

        private void comboBox1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                errorProvider1.SetError(comboBox1, "Se requiere categoría.");
            }
            else
            {
                errorProvider1.SetError(comboBox1, "");
            }
        }
        #endregion
    }
}
