using InventoryApp.Models;
using InventoryApp.Utility;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace InventoryApp.Data
{
    public class CategoryManager
    {
        readonly SqlConnection con = ConnectionManager.GetConnection();

        private readonly CustomDataTable _dbCon = new CustomDataTable();
        private DataTable _dt = new DataTable();
        private readonly Hashtable _params = new Hashtable();

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
        }

        // Update Category
        public void UpdateCategory(Category category)
        {
            _params.Clear();
            _params.Add("@Id", category.Id);
            _params.Add("@CategoryItem", category.CategoryItem);
            _dbCon.Execute("SP_Categories_Update", _params);
        }

        // Delete Category
        public void DeleteCategory(int id)
        {
            _params.Clear();
            _params.Add("@Id", id);
            _dbCon.Execute("SP_Categories_Delete", _params);
        }
    }
}
