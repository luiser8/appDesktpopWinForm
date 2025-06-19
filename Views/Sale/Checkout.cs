﻿using InventoryApp.Data;
using InventoryApp.Utility;
using System;
using System.Windows.Forms;

namespace InventoryApp
{
    public partial class Checkout : Form
    {
        private readonly PointOfSale pointOfSale = new PointOfSale();
        private readonly CartManager cartManager = new CartManager();
        private readonly InvoiceManager invoiceManager = new InvoiceManager();
        private readonly InvoiceIdGenerator invoiceIdGenerator = new InvoiceIdGenerator();

        public Checkout(decimal totalPrice)
        {
            InitializeComponent();

            label3.Text = totalPrice.ToString();

            pointOfSale.InitializeComboBox(comboBox1);
            pointOfSale.CalculateDiscount(label3.Text, comboBox1.SelectedItem, label7, label8);
            cartManager.LoadCartItems(listBox1);
        }

        // ON TEXT CHANGED
        public void ChangeEventHandler()
        {
            if (decimal.TryParse(textBox2.Text, out _))
            {
                pointOfSale.CalculateChange(label8, textBox2, label10);
            }
        }

        // COMBOBOX EVENT
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pointOfSale.CalculateDiscount(label3.Text, comboBox1.SelectedItem, label7, label8);
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            // TODO: not working
            pointOfSale.CalculateDiscount(label3.Text, comboBox1.SelectedItem, label7, label8);
        }

        // TEXTBOX EVENT
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ChangeEventHandler();
        }

        // LABEL8 EVENT
        private void label8_TextChanged(object sender, EventArgs e)
        {
            ChangeEventHandler();
        }

        // INSERT STOCK BUTTON
        private void button1_Click(object sender, EventArgs e)
        {
            string invoiceId = invoiceIdGenerator.GenerateInvoiceId();

            if (pointOfSale.ProcessInvoice(label3.Text, textBox2.Text, comboBox1.SelectedItem, invoiceId))
            {
                invoiceManager.InsertInvoiceItems(listBox1, invoiceId);
                DialogResult = DialogResult.OK;
            }
        }

        // CANCEL BUTTON
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
