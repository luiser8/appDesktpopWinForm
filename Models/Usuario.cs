using System;

namespace InventoryApp
{
    public class Usuario
    {
        public int Id { get; set; }
        public int RolId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string RolName { get; set; }
        public bool Status { get; set; }
        public string StatusString { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class UsuarioResponse
    {
        public int Id { get; set; }
        public int RolId { get; set; }
        public string RolName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
    }
}
