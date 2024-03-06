using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models {

    public class Inventory {

        [Key]
        [Column(Order = 0)]
        public int LocationID { get; set; }

        [Key]
        [Column(Order = 1)]
        public int ItemID { get; set; }

        [Required]
        public int ReorderQuantity { get; set; }

        [Required]
        public int ReorderLevel { get; set; }

        public Location Location { get; set; }

        public Item Item { get; set; }

    }

}
