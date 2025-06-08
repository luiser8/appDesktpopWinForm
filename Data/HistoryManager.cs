using InventoryApp.Models;
using InventoryApp.Utility;
using System.Collections;
using System.Data;

namespace InventoryApp.Data
{
    public class HistoryManager
    {
        private readonly CustomDataTable _dbCon = new CustomDataTable();
        private readonly Hashtable _params = new Hashtable();
        private readonly AuditManager _auditManager = new AuditManager();

        public void InsertHistory(History history)
        {
            _params.Clear();
            _params.Add("@ProductId", history.ProductID);
            _params.Add("@AddedStocks", history.AddedStocks);
            _dbCon.Execute("SP_History_Insert", _params);

            _auditManager.InsertAudit(new AuditUser { UserId = UserSession.SessionUID, Table = "History", Action = "Insertar history", Events = "Insertar History" });
        }

        public DataTable SelectHistory(int productId)
        {
            _params.Clear();
            _params.Add("@ProductId", productId);
            return _dbCon.Execute("SP_History_Select_ByProductId", _params);
        }
    }
}
