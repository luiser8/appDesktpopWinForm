using System;

namespace InventoryApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int Unit { get; set; }
        public string Category { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
