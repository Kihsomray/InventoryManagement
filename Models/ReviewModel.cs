using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models {

    public class Review {

        public int CustomerID { get; set; }

        public int ItemID { get; set; }

        [Required]
        public int Rating { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public Customer Customer { get; set; }

        public Item Item { get; set; }
    }
}
