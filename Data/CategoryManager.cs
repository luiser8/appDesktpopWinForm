using InventoryApp.Models;
using InventoryApp.Utility;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace InventoryApp.Data
{
    public class CategoryManager
    {
        private readonly CustomDataTable _dbCon = new CustomDataTable();
        private DataTable _dt = new DataTable();
        private readonly Hashtable _params = new Hashtable();
        private readonly AuditManager _auditManager = new AuditManager();

        // Fetch data from Category
        public DataTable GetCategories()
        {
            _params.Clear();
            _dt = _dbCon.Execute("SP_Categories_Select_All", _params);
            return _dt;
        }

        // Add new Category
        public void AddCategory(Category category)
        {
            _params.Clear();
            _params.Add("@CategoryItem", category.CategoryItem);
            _dbCon.Execute("SP_Categories_Insert", _params);

            _auditManager.InsertAudit(new AuditUser { UserId = UserSession.SessionUID, Table = "Categoria", Action = "Insertar categoria" });
        }

        // Update Category
        public void UpdateCategory(Category category)
        {
            _params.Clear();
            _params.Add("@Id", category.Id);
            _params.Add("@CategoryItem", category.CategoryItem);
            _dbCon.Execute("SP_Categories_Update", _params);

            _auditManager.InsertAudit(new AuditUser { UserId = UserSession.SessionUID, Table = "Categoria", Action = "Actualizar categoria" });
        }

        // Delete Category
        public void DeleteCategory(int id)
        {
            _params.Clear();
            _params.Add("@Id", id);
            _dbCon.Execute("SP_Categories_Delete", _params);

            _auditManager.InsertAudit(new AuditUser { UserId = UserSession.SessionUID, Table = "Categoria", Action = "Eliminar categoria" });
        }
    }
}
