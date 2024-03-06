using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models {
    public class Discount {

        [Key]
        [ForeignKey("Item")]
        public int ItemID { get; set; }

        [Required]
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Percentage { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        public int QuantityUsed { get; set; }

        [Required]
        public int UsageLimit { get; set; }

        public Item Item { get; set; }

    }

}
