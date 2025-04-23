using InventoryApp.Models;
using InventoryApp.Utility;
using System.Collections;

namespace InventoryApp.Data
{
    public class HistoryManager
    {
        private readonly CustomDataTable _dbCon = new CustomDataTable();
        private readonly Hashtable _params = new Hashtable();

        public void InsertHistory(History history)
        {
            _params.Clear();
            _params.Add("@ProductId", history.ProductID);
            _params.Add("@AddedStocks", history.AddedStocks);
            _dbCon.Execute("SP_History_Insert", _params);
        }
    }
}
