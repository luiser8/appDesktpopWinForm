﻿using System.Data;
using InventoryApp.Utility;
using System.Collections;
using InventoryApp.Models;

namespace InventoryApp.Data
{
    public class OrdersManager
    {
        private readonly CustomDataTable _dbCon = new CustomDataTable();
        private DataTable _dt = new DataTable();
        private readonly Hashtable _params = new Hashtable();
        private readonly AuditManager _auditManager = new AuditManager();

        public void InsertOrders(Order order)
        {
            _params.Clear();
            _params.Add("@InvoiceId", order.InvoiceId);
            _params.Add("@Name", order.Name);
            _params.Add("@Price", order.Price);
            _params.Add("@Quantity", order.Quantity);
            _dt = _dbCon.Execute("SP_Orders_Insert", _params);

            _auditManager.InsertAudit(new AuditUser { UserId = UserSession.SessionUID, Table = "Order", Action = "Insertar order", Events = "Insertar Order" });
        }

        public DataTable GetOrdersByInvoiceId(string invoiceId)
        {
            _params.Clear();
            _params.Add("@InvoiceId", invoiceId);
            _dt = _dbCon.Execute("SP_Orders_Select_ByInvoiceId", _params);
            return _dt;
        }
    }
}
