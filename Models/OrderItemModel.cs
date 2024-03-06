using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models {
    public class OrderItem {

        public int OrderID { get; set; }

        public int ItemID { get; set; }

        [Required]
        public int Quantity { get; set; }

        public Order Order { get; set; }

        public Item Item { get; set; }
    }
}
