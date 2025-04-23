using System.Data;
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

        public void InsertOrders(Order order)
        {
            _params.Clear();
            _params.Add("@TransactionId", order.TransactionId);
            _params.Add("@Name", order.Name);
            _params.Add("@Price", order.Price);
            _params.Add("@Quantity", order.Quantity);
            _dt = _dbCon.Execute("SP_Orders_Insert", _params);
        }
    }
}
