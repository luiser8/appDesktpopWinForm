using System;
using System.Windows.Forms;
using InventoryApp.Utility;
using System.Collections;
using System.Data;
using InventoryApp.Models;

namespace InventoryApp.Data
{
    internal class InvoiceManager
    {
        private readonly OrdersManager _ordersManager = new OrdersManager();
        private readonly ProductManager _productManager = new ProductManager();
        private readonly AuditManager _auditManager = new AuditManager();
        private readonly CustomDataTable _dbCon = new CustomDataTable();
        private DataTable _dt = new DataTable();
        private readonly Hashtable _params = new Hashtable();

        public DataTable SelectInvoiceAll(int uid)
        {
            _params.Clear();
            _params.Add("@Uid", uid);
            _dt = _dbCon.Execute("SP_Invoice_Select_All", _params);
            return _dt;
        }

        public DataTable SelectReportsInvoice()
        {
            _params.Clear();
            _dt = _dbCon.Execute("SP_Report_Invoice", _params);
            return _dt;
        }

        // Insert Invoice Items
        public void InsertInvoiceItems(ListBox listBox, string invoiceId)
        {
            foreach (var item in listBox.Items)
            {
                string[] parts = item.ToString().Split(new string[] { " x ", " - $" }, StringSplitOptions.None);
                string name = parts[1];
                decimal price = decimal.Parse(parts[2]);
                int quantity = int.Parse(parts[0]);

                _ordersManager.InsertOrders(new Order
                {
                    InvoiceId = invoiceId,
                    Name = name,
                    Price = price.ToString(),
                    Quantity = quantity,
                });
            }
            _auditManager.InsertAudit(new AuditUser { UserId = UserSession.SessionUID, Table = "Invoice", Action = "Insertar item factura", Events = "Insertar item a la factura" });
        }

        // Saved Transaction
        public void SaveInvoiceToDatabase(Invoice transaction)
        {
            try
            {
                if (transaction == null)
                {
                    throw new ArgumentNullException(nameof(transaction), "La factura no puede ser nula");
                }

                _productManager.UpdateProductByStock(null, null);

                _params.Clear();
                _params.Add("@InvoiceId", transaction.InvoiceId);
                _params.Add("@PayMethodId", 1);
                _params.Add("@BankId", 1);
                _params.Add("@Subtotal", transaction.Subtotal);
                _params.Add("@Cash", transaction.Cash);
                _params.Add("@DiscountPercent", transaction.DiscountPercent);
                _params.Add("@DiscountAmount", transaction.DiscountAmount);
                _params.Add("@Change", transaction.Change);
                _params.Add("@Total", transaction.Total);
                _params.Add("@Date", transaction.Date);
                _params.Add("@Uid", transaction.Uid);
                _dt = _dbCon.Execute("SP_Invoice_Insert", _params);
            }
            catch (Exception ex)
            {
                {
                    throw new Exception(ex.Message);
                }
            }
            _auditManager.InsertAudit(new AuditUser { UserId = UserSession.SessionUID, Table = "Invoice", Action = "Insertar factura", Events = "Insertar factura" });
        }
    }
}
