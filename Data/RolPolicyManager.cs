using System.Data;
using InventoryApp.Utility;
using System.Collections;
using System.Collections.Generic;
using InventoryApp.Models;
using System;

namespace InventoryApp.Data
{
    public class RolPolicyManager
    {
        private readonly CustomDataTable _dbCon = new CustomDataTable();
        private DataTable _dt = new DataTable();
        private readonly Hashtable _params = new Hashtable();

        public List<RolPolicy> SelectRolPolicyByUserl(int userId)
        {
            var policies = new List<RolPolicy>();
            _params.Clear();
            _params.Add("@UserId", userId);
            _dt = _dbCon.Execute("SP_RolPolicy_Select_ByUser", _params);

           if (_dt != null || _dt.Rows.Count >= 0)
            {
                foreach (DataRow row in _dt.Rows)
                {
                    policies.Add(new RolPolicy
                    {
                        UserId = userId,
                        RolId = row["RolId"] != DBNull.Value ? Convert.ToInt32(row["RolId"]) : 0,
                        RolName = row["RolName"].ToString(),
                        PolicyId = row["PolicyId"] != DBNull.Value ? Convert.ToInt32(row["PolicyId"]) : 0,
                        Policy = row["Policy"].ToString()
                    });
                }
                return policies;
            }

            return policies;
        }
    }
}
