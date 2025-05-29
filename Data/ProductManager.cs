using System.Data;
using InventoryApp.Utility;
using System.Collections;
using InventoryApp.Models;

namespace InventoryApp.Data
{
    public class ProductManager
    {
        private readonly CustomDataTable _dbCon = new CustomDataTable();
        private DataTable _dt = new DataTable();
        private readonly Hashtable _params = new Hashtable();
        private readonly AuditManager _auditManager = new AuditManager();

        public DataTable SelectProductsAll()
        {
            _params.Clear();
            _dt = _dbCon.Execute("SP_Products_Select_All", _params);
            return _dt;
        }

        public DataTable SelectProductsById(int id)
        {
            _params.Clear();
            _params.Add("@ProductId", id);
            _dt = _dbCon.Execute("SP_Products_Select_ByStock", _params);
            return _dt;
        }

        public DataTable SelectProductsByName(string name)
        {
            _params.Clear();
            _params.Add("@Name", name);
            _dt = _dbCon.Execute("SP_Products_Select_ByName", _params);
            return _dt;
        }

        public bool InsertProduct(Product product)
        {
            _params.Clear();
            _params.Add("@Name", product.Name);
            _params.Add("@Price", product.Price);
            _params.Add("@Stock", product.Stock);
            _params.Add("@Unit", product.Unit);
            _params.Add("@Category", product.Category);
            _dt = _dbCon.Execute("SP_Products_Insert", _params);

            _auditManager.InsertAudit(new AuditUser { UserId = UserSession.SessionUID, Table = "Product", Action = "Insertar producto" });
            return true;
        }

        // Search Product
        public DataTable SearchProducts(string searchTerm)
        {
            _params.Clear();
            _params.Add("@Value", searchTerm);
            _dt = _dbCon.Execute("SP_Products_Select_Search", _params);
            return _dt;
        }

        // Update Product
        public void UpdateProduct(Product product)
        {
            _params.Clear();
            _params.Add("@Id", product.Id);
            _params.Add("@Name", product.Name);
            _params.Add("@Price", product.Price);
            _params.Add("@Stock", product.Stock);
            _params.Add("@Unit", product.Unit);
            _params.Add("@Category", product.Category);
            _dbCon.Execute("SP_Products_Update", _params);

            _auditManager.InsertAudit(new AuditUser { UserId = UserSession.SessionUID, Table = "Product", Action = "Actualizar producto" });
        }

        public void UpdateProductByStock(int? productId, int? stock)
        {
            _params.Clear();
            _params.Add("@ProductId", productId);
            _params.Add("@Stock", stock);
            _dbCon.Execute("SP_Products_Update_ByStock", _params);

            _auditManager.InsertAudit(new AuditUser { UserId = UserSession.SessionUID, Table = "Product", Action = "Actualizar stock producto" });
        }

        // Delete Product
        public void DeleteProduct(int id)
        {
            _params.Clear();
            _params.Add("@Id", id);
            _dbCon.Execute("SP_Products_Delete", _params);

            _auditManager.InsertAudit(new AuditUser { UserId = UserSession.SessionUID, Table = "Product", Action = "Eliminar producto" });
        }
    }
}
