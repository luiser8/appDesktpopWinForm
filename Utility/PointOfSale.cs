﻿using System;
using System.Windows.Forms;
using InventoryApp.Data;
using InventoryApp.Models;

namespace InventoryApp.Utility
{
    public class PointOfSale
    {
        private readonly CartManager cartManager = new CartManager();
        private readonly InvoiceManager invoiceManager = new InvoiceManager();

        public void InitializeComboBox(ComboBox comboBox)
        {
            comboBox.Items.Add(new ComboBoxItem { Value = 10, Description = "10% off" });
            comboBox.Items.Add(new ComboBoxItem { Value = 15, Description = "15% off" });
            comboBox.Items.Add(new ComboBoxItem { Value = 30, Description = "30% off" });
            comboBox.Items.Add(new ComboBoxItem { Value = 50, Description = "50% off" });
        }

        // Calculate Discount in real time
        public void CalculateDiscount(string totalText, object selectedItem, Label labelDiscount, Label labelTotalAfterDiscount)
        {
            int total = Convert.ToInt32(totalText);
            double discountAmount = 0;

            if (selectedItem is ComboBoxItem selectedComboBoxItem)
            {
                double discountPercent = selectedComboBoxItem.Value;
                discountAmount = total * (discountPercent / 100);
            }

            double totalAfterDiscount = total - discountAmount;
            labelDiscount.Text = (0 - discountAmount).ToString();
            labelTotalAfterDiscount.Text = totalAfterDiscount.ToString();
        }

        // Calculate Change in real time
        public void CalculateChange(Label totalLabel, TextBox paidTextBox, Label changeLabel)
        {
            decimal totalAmount = decimal.Parse(totalLabel.Text);

            if (decimal.TryParse(paidTextBox.Text, out decimal paidAmount))
            {
                decimal change = paidAmount - totalAmount;
                if (change < 0)
                {
                    change = 0;
                    changeLabel.Text = "0";
                }
                changeLabel.Text = change.ToString();
            }
            else
            {
                changeLabel.Text = string.Empty;
            }
        }

        // Process Transaction then save to database
        public bool ProcessInvoice(string totalText, string cashText, object selectedItem, string invoiceId)
        {
            int subtotal = Convert.ToInt32(totalText);
            int cash = string.IsNullOrWhiteSpace(cashText) ? 0 : Convert.ToInt32(cashText);
            double discountPercent = 0;
            if (selectedItem is ComboBoxItem selectedComboBoxItem)
            {
                discountPercent = selectedComboBoxItem.Value;
            }

            // Calculate the discount amount
            double discountAmount = subtotal * (discountPercent / 100);

            // Calculate the total after discount
            double total = subtotal - discountAmount;

            double totalAfterDiscount;
            // Validate if there is enough cash
            if (cash < total)
            {
                MessageBox.Show("No hay suficiente efectivo para generar la facturacion.");
                return false;
            }

            double change = cash - total;
            DateTime currentDate = DateTime.Now;

            try
            {
                invoiceManager.SaveInvoiceToDatabase(new Invoice
                {
                    InvoiceId = invoiceId,
                    BankId = 1,
                    PayMethodId = 1,
                    Subtotal = subtotal.ToString(),
                    Cash = cash.ToString(),
                    DiscountPercent = Math.Round(discountPercent, 0) + "%",
                    DiscountAmount = "$" + discountAmount.ToString(),
                    Change = "$" + change.ToString(),
                    Total = "$" + total.ToString(),
                    Date = currentDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    Uid = UserSession.SessionUID,
                });
                cartManager.RemoveCartItem(UserSession.SessionUID, "@Uid");

                totalAfterDiscount = total;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se produjo un error al guardar la factura: " + ex.Message);
                return false;
            }
        }
    }
}
