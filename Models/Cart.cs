using System;

namespace InventoryApp.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Uid { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool Status { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
