using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models {
    public class Feedback {

        [Key]
        [ForeignKey("Order")]
        public int OrderID { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public Order Order { get; set; }
    }
}
