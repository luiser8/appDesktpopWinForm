using System;

namespace InventoryApp.Models
{
    public class Rol
    {
        public int Id { get; set; }
        public string RolName { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
