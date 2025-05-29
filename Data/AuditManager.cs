using InventoryApp.Models;
using InventoryApp.Utility;
using System.Collections;
using System.Data;

namespace InventoryApp.Data
{
    public class AuditManager
    {
        private readonly CustomDataTable _dbCon = new CustomDataTable();
        private readonly Hashtable _params = new Hashtable();

        public void InsertAudit(AuditUser audit)
        {
            _params.Clear();
            _params.Add("@UserId", audit.UserId);
            _params.Add("@Action", audit.Action);
            _params.Add("@Table", audit.Table);
            _params.Add("@Events", audit.Events);
            _dbCon.Execute("SP_Audit_Insert", _params);
        }

        public DataTable SelectAudit(int userId)
        {
            _params.Clear();
            _params.Add("@UserId", userId);
            return _dbCon.Execute("SP_Audit_Select_ByUserId", _params);
        }
    }
}
