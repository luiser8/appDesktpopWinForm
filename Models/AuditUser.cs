using System;

namespace InventoryApp.Models
{
    public class AuditUser
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Table { get; set; }
        public string Action { get; set; }
        public string Events { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
