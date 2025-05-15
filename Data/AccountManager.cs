using InventoryApp.Utility;
using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace InventoryApp.Data
{
    class AccountManager
    {
        private readonly CustomDataTable _dbCon = new CustomDataTable();
        private DataTable _dt = new DataTable();
        private readonly Hashtable _params = new Hashtable();

        public DataTable GetAllUsuarios()
        {
            _params.Clear();
            _dt = _dbCon.Execute("SP_Users_Select_All", _params);
            return _dt;

            //Usuario usuario = null;

            //if (_dt != null || _dt.Rows.Count <= 0)
            //{
            //    foreach (DataRow row in _dt.Rows)
            //    {
            //        usuario = new Usuario
            //        {
            //            Id = Convert.ToInt32(row["Id"]),
            //            RolId = Convert.ToInt32(row["RolId"]),
            //            UserName = row["UserName"].ToString(),
            //            RolName = row["RolName"].ToString(),
            //            Email = row["Email"]?.ToString(),
            //            Status = Convert.ToBoolean(row["Status"]),
            //            CreatedAt = Convert.ToDateTime(row["Status"])
            //        };
            //    }
            //}

            //return usuario;
        }

        public UsuarioResponse ValidateUserCredentials(string username, string password)
        {
            _params.Clear();
            _params.Add("@UserName", username);
            _params.Add("@Password", MD5.GetMD5(password));
            _dt = _dbCon.Execute("SP_Users_Select_ByLogin", _params);

            UsuarioResponse usuarioResponse = null;

            if (_dt != null || _dt.Rows.Count != 0)
            {
                foreach (DataRow row in _dt.Rows)
                {
                    usuarioResponse = new UsuarioResponse
                    {
                        Id = row["Id"] != DBNull.Value ? Convert.ToInt32(row["Id"]) : 0,
                        RolName = row["RolName"]?.ToString()
                    };
                }
            }

            return usuarioResponse;
        }

        // Register new user
        public void RegisterUser(Usuario usuario)
        {
            // Check if the username already exists in the database
            if (IsUsernameExists(usuario.UserName))
            {
                MessageBox.Show("Username already exists. Please choose a different username.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int UserId = 0;
            _params.Clear();
            _params.Add("@RolName", usuario.RolName);
            _params.Add("@UserName", usuario.UserName);
            _params.Add("@Password", MD5.GetMD5(usuario.Password));
            _params.Add("@Email", usuario.Email);
            _dt = _dbCon.Execute("SP_Users_Insert", _params);

            if (_dt != null || _dt.Rows.Count != 0)
            {
                foreach (DataRow row in _dt.Rows)
                {
                    UserId = Convert.ToInt32(row["Id"]);
                }

                if (UserId > 0)
                {
                    MessageBox.Show("Registration successful!", "Registration", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to register. Please try again.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //Update User
        public void UpdateUser(Usuario usuario)
        {
            _params.Clear();
            _params.Add("@Id", usuario.Id);
            _params.Add("@RolName", usuario.RolName);
            _params.Add("@UserName", usuario.UserName);
            _params.Add("@Password", MD5.GetMD5(usuario.Password));
            _params.Add("@Email", usuario.Email);
            _dt = _dbCon.Execute("SP_Users_Update", _params);

            if (_dt != null || _dt.Rows.Count != 0)
            {
                MessageBox.Show("Update successful!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to update. Please try again.", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Delete user
        public void DeleteUser(int id)
        {
            _params.Clear();
            _params.Add("@Id", id);
            _dt = _dbCon.Execute("SP_Users_Delete", _params);
            if (_dt != null || _dt.Rows.Count != 0)
            {
                MessageBox.Show("User deleted successfully!", "Delete User", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to delete user. Please try again.", "Delete User Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Check if user name already exists
        private bool IsUsernameExists(string username)
        {
            _params.Clear();
            _params.Add("@UserName", username);
            _dt = _dbCon.Execute("SP_Users_Select_ByName", _params);

            if (_dt != null || _dt.Rows.Count <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
