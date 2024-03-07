using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models {
    public class Cart {
        [Display(Name = "Customer ID")]
        [Required(ErrorMessage = "Customer ID is required")]
        public int CustomerID { get; set; }

        [Display(Name = "Item ID")]
        [Required(ErrorMessage = "Item ID is required")]
        public int ItemID { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }

        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; }

        [ForeignKey("ItemID")]
        public Item Item { get; set; }
    }
}
