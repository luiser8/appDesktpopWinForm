using System;

namespace InventoryApp.Models
{
    public class History
    {
        public int Id { get; set; }
        public int ProductID { get; set; }
        public int AddedStocks { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
