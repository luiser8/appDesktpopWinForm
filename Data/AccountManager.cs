using InventoryApp.Models;
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
        private readonly AuditManager _auditManager = new AuditManager();

        public DataTable GetAllUsuarios(int userId)
        {
            _params.Clear();
            _params.Add("@Id", userId);
            _dt = _dbCon.Execute("SP_Users_Select_All", _params);
            return _dt;
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
                        RolName = row["RolName"]?.ToString(),
                        FirstName = row["FirstName"]?.ToString(),
                        LastName = row["LastName"]?.ToString(),
                        Email = row["Email"]?.ToString(),
                        Status = row["Status"] != DBNull.Value && Convert.ToInt32(row["Status"]) == 1,
                    };
                }
                if (usuarioResponse != null && usuarioResponse.Id > 0 && usuarioResponse.Status)
                    _auditManager.InsertAudit(new AuditUser { UserId = usuarioResponse.Id, Table = "Account", Action = "Inicio de sesion", Events = "Inicio de sesion satisfactorio" });
                if (usuarioResponse != null && !usuarioResponse.Status)
                { 
                    string messageError = usuarioResponse.Status ? "satisfactorio" : "fallido, usuario inactivo";
                    _auditManager.InsertAudit(new AuditUser { UserId = usuarioResponse.Id, Table = "Account", Action = "Inicio de sesion", Events = $"Inicio de sesion {messageError}" });
                }
            }

            return usuarioResponse;
        }

        // Register new user
        public void RegisterUser(Usuario usuario)
        {
            bool checkExistsUser = IsUsernameExists(usuario.UserName);
            if (checkExistsUser)
            {
                MessageBox.Show("El nombre de usuario ya existe. Por favor, elija un nombre de usuario diferente.", "Error de registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int UserId = 0;
                _params.Clear();
                _params.Add("@FirstName", usuario.FirstName);
                _params.Add("@LastName", usuario.LastName);
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
                        MessageBox.Show("¡Registro exitoso!", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo registrar. Por favor, inténtelo de nuevo.", "Error de registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                _auditManager.InsertAudit(new AuditUser { UserId = UserSession.SessionUID, Table = "Account", Action = "Registro de usuario", Events = $"Se ha registrado un nuevo usuario: {usuario.UserName}" });
            }
        }

        // Update password
        public void UpdatePassword(int userId, string newPassword)
        {
            _params.Clear();
            _params.Add("@Id", userId);
            _params.Add("@Password", MD5.GetMD5(newPassword));
            _dt = _dbCon.Execute("SP_Users_Update_Password", _params);
            if (_dt != null || _dt.Rows.Count != 0)
            {
                MessageBox.Show("¡Contraseña actualizada correctamente!", "Actualizar contraseña", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se pudo actualizar la contraseña. Por favor, inténtelo de nuevo.", "Error al actualizar contraseña", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            _auditManager.InsertAudit(new AuditUser { UserId = UserSession.SessionUID, Table = "Account", Action = "Actualizacion de contraseña", Events = $"Se ha actualizado la contraseña del usuario: {userId}" });
        }

        // Udpate User Status
        public void UpdateUserStatus(string username, bool status)
        {
            _params.Clear();
            _params.Add("@UserName", username);
            _params.Add("@Status", status ? 1 : 0);
            _dt = _dbCon.Execute("SP_Users_Update_Status", _params);

            _auditManager.InsertAudit(new AuditUser { UserId = UserSession.SessionUID, Table = "Account", Action = "Actualizacion de estado de usuario", Events = $"Se ha actualizado el estado del usuario: {username}" });
        }

        //Update User
        public void UpdateUser(Usuario usuario)
        {
            _params.Clear();
            _params.Add("@Id", usuario.Id);
            _params.Add("@FirstName", usuario.FirstName);
            _params.Add("@LastName", usuario.LastName);
            _params.Add("@RolName", usuario.RolName);
            _params.Add("@UserName", usuario.UserName);
            _params.Add("@Email", usuario.Email);
            _params.Add("@Status", usuario.Status ? 1 : 0);
            _dt = _dbCon.Execute("SP_Users_Update", _params);

            if (_dt != null || _dt.Rows.Count != 0)
            {
                MessageBox.Show("¡Actualización exitosa!", "Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se pudo actualizar. Por favor, inténtelo de nuevo.", "Error de actualización", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            _auditManager.InsertAudit(new AuditUser { UserId = UserSession.SessionUID, Table = "Account", Action = "Actualizacion de usuario", Events = $"Se ha actualizado datos del usuario: {usuario.Id}" });
        }

        //Delete user
        public void DeleteUser(int id)
        {
            _params.Clear();
            _params.Add("@Id", id);
            _dt = _dbCon.Execute("SP_Users_Delete", _params);
            if (_dt != null || _dt.Rows.Count != 0)
            {
                MessageBox.Show("Usuario eliminado exitosamente!", "Eliminar usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error al eliminar el usuario. Inténtelo de nuevo.", "Error al eliminar usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            _auditManager.InsertAudit(new AuditUser { UserId = UserSession.SessionUID, Table = "Account", Action = "Eliminacion de usuario", Events = $"Se ha eliminado el usuario: {id}" });
        }

        // Check if user name already exists
        private bool IsUsernameExists(string username)
        {
            _params.Clear();
            _params.Add("@UserName", username);
            _dt = _dbCon.Execute("SP_Users_Select_ByName", _params);
            bool exists = false;

            if (_dt != null && _dt.Rows.Count >= 0)
            {
                foreach (DataRow row in _dt.Rows)
                {
                    exists = Convert.ToInt16(row["ValueExists"]) == 1;
                }
            }

            return exists;
        }
    }
}
