using System.Data;
using InventoryApp.Utility;
using System.Collections;

namespace InventoryApp.Data
{
    public class RolManager
    {
        private readonly CustomDataTable _dbCon = new CustomDataTable();
        private DataTable _dt = new DataTable();
        private readonly Hashtable _params = new Hashtable();

        public DataTable SelectRolAll()
        {
            _params.Clear();
            _dt = _dbCon.Execute("SP_Roles_Select_All", _params);
            return _dt;
        }
    }
}
