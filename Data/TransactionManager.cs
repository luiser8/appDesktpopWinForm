using System;
using System.Windows.Forms;
using InventoryApp.Utility;
using System.Collections;
using System.Data;
using InventoryApp.Models;

namespace InventoryApp.Data
{
    internal class TransactionManager
    {
        private readonly OrdersManager _ordersManager = new OrdersManager();
        private readonly ProductManager _productManager = new ProductManager();
        private readonly AuditManager _auditManager = new AuditManager();
        private readonly CustomDataTable _dbCon = new CustomDataTable();
        private DataTable _dt = new DataTable();
        private readonly Hashtable _params = new Hashtable();

        public DataTable SelectTransactionsAll(int uid)
        {
            _params.Clear();
            _params.Add("@Uid", uid);
            _dt = _dbCon.Execute("SP_Transactions_Select_All", _params);
            return _dt;
        }

        // Insert Transaction Items
        public void InsertTransactionItems(ListBox listBox, string transactionId)
        {
            foreach (var item in listBox.Items)
            {
                string[] parts = item.ToString().Split(new string[] { " x ", " - $" }, StringSplitOptions.None);
                string name = parts[1];
                decimal price = decimal.Parse(parts[2]);
                int quantity = int.Parse(parts[0]);

                _ordersManager.InsertOrders(new Order
                {
                    TransactionId = transactionId,
                    Name = name,
                    Price = price.ToString(),
                    Quantity = quantity,
                });
            }
            _auditManager.InsertAudit(new AuditUser { UserId = UserSession.SessionUID, Table = "Transaction", Action = "Insertar item transaccion", Events = "Insertar item a la transaccion" });
        }

        // Saved Transaction
        public void SaveTransactionToDatabase(Transaction transaction)
        {
            try
            {
                if (transaction == null)
                {
                    throw new ArgumentNullException(nameof(transaction), "Transaction cannot be null");
                }

                _productManager.UpdateProductByStock(null, null);

                _params.Clear();
                _params.Add("@TransactionId", transaction.TransactionId);
                _params.Add("@Subtotal", transaction.Subtotal);
                _params.Add("@Cash", transaction.Cash);
                _params.Add("@DiscountPercent", transaction.DiscountPercent);
                _params.Add("@DiscountAmount", transaction.DiscountAmount);
                _params.Add("@Change", transaction.Change);
                _params.Add("@Total", transaction.Total);
                _params.Add("@Date", transaction.Date);
                _params.Add("@Uid", transaction.Uid);
                _dt = _dbCon.Execute("SP_Transactions_Insert", _params);
            }
            catch (Exception ex)
            {
                {
                    throw new Exception(ex.Message);
                }
            }
            _auditManager.InsertAudit(new AuditUser { UserId = UserSession.SessionUID, Table = "Transaction", Action = "Insertar transaccion", Events = "Insertar Transaccion" });
        }
    }
}
