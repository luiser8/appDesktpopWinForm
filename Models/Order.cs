using System;

namespace InventoryApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public int Quantity { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
