using System;
using System.Data;
using System.Windows.Forms;
using InventoryApp.Utility;
using System.Collections;
using InventoryApp.Models;

namespace InventoryApp.Data
{
    public class CartManager
    {
        private readonly CustomDataTable _dbCon = new CustomDataTable();
        private DataTable _dt = new DataTable();
        private readonly Hashtable _params = new Hashtable();
        private readonly AuditManager _auditManager = new AuditManager();

        // Fetch data from Cart
        public DataTable GetCartItems()
        {
            int currentUID = UserSession.SessionUID;
            _params.Clear();
            _params.Add("@Uid", currentUID);
            _dt = _dbCon.Execute("SP_Cart_Select_ByUid", _params);
            return _dt;
        }

        // Update Quantity
        public void UpdateQuantityInCart(Cart cart)
        {
            _params.Clear();
            _params.Add("@ProductId", cart.ProductId);
            _params.Add("@Quantity", cart.Quantity);
            _dt = _dbCon.Execute("SP_Cart_Update", _params);

            _auditManager.InsertAudit(new AuditUser { UserId = UserSession.SessionUID, Table = "Cart", Action = "Actualizar cantidad en cart", Events = "Actualizacion de la cantidad de Cart" });
        }

        public decimal GetTotalPrice()
        {
            decimal totalPrice = 0;

            _params.Clear();
            _dt = _dbCon.Execute("SP_Cart_Select_All", _params);

            foreach (DataRow row in _dt.Rows)
            {
                object price = row["Price"];
                object quantity = row["Quantity"];
                if (price != DBNull.Value && quantity != DBNull.Value)
                {
                    totalPrice = Convert.ToDecimal(price) * Convert.ToInt32(quantity);
                }
            }

            return totalPrice;
        }

        // Remove product from Cart
        public void RemoveCartItem(int? id, string typeDeleting)
        {
            _params.Clear();
            _params.Add(typeDeleting, id);
            _dbCon.Execute("SP_Cart_Delete", _params);

            _auditManager.InsertAudit(new AuditUser { UserId = UserSession.SessionUID, Table = "Cart", Action = "Remover cart", Events = "Remover Cart" });
        }

        //Count items on Cart
        public int GetCartItemCount()
        {
            return GetCartItems().Rows.Count;
        }

        // Load Cart items to ListBox
        public void LoadCartItems(ListBox listBox)
        {
            try
            {
                _params.Clear();
                _dt = _dbCon.Execute("SP_Cart_Select_All", _params);

                foreach (DataRow row in _dt.Rows)
                {
                    listBox.Items.Clear();
                    object name = row["name"];
                    object price = row["Price"];
                    object quantity = row["Quantity"];
                    if (price != DBNull.Value && quantity != DBNull.Value && name != DBNull.Value)
                    {
                        string item = $"{Convert.ToInt32(quantity)} x {Convert.ToString(name)} - ${Convert.ToDecimal(price)}";
                        listBox.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading cart items: " + ex.Message);
            }
        }

        public bool AddItemToCart(Cart cart)
        {
            _params.Clear();
            _params.Add("@Name", cart.Name);
            _dt = _dbCon.Execute("SP_Cart_Select_ByName", _params);

            if (_dt.Rows.Count > 0)
            {
                foreach (DataRow row in _dt.Rows)
                {
                    int existingQuantity = Convert.ToInt32(row["Quantity"]);
                    decimal existingPrice = Convert.ToDecimal(row["Price"]);
                    int newQuantity = existingQuantity + 1;
                    decimal newPrice = existingPrice;

                    _params.Clear();
                    _params.Add("@ProductId", Convert.ToInt32(row["Id"]));
                    _params.Add("@Quantity", newQuantity);
                    _params.Add("@Price", newPrice);
                    _params.Add("@Name", cart.Name);
                    _dt = _dbCon.Execute("SP_Cart_Update", _params);
                }
            } else
            {
                _params.Clear();
                _params.Add("@ProductId", cart.ProductId);
                _params.Add("@Quantity", cart.Quantity);
                _params.Add("@Price", cart.Price);
                _params.Add("@Name", cart.Name);
                _params.Add("@Uid", UserSession.SessionUID);
                _dt = _dbCon.Execute("SP_Cart_Insert", _params);
            }

            _auditManager.InsertAudit(new AuditUser { UserId = UserSession.SessionUID, Table = "Cart", Action = "Insercion de cart", Events = "Insertar Cart" });
            return true;
        }
    }
}
