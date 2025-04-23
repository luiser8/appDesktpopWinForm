using System;

namespace InventoryApp.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public string Subtotal { get; set; }
        public string Cash { get; set; }
        public string DiscountPercent { get; set; }
        public string DiscountAmount { get; set; }
        public string Change { get; set; }
        public string Total { get; set; }
        public string Date { get; set; }
        public int Uid { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
