using System;

namespace InventoryApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryItem { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
