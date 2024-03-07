using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models {

    public class Payment {

        [Key]
        [ForeignKey("Order")]
        public int OrderID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(50)]
        public string Method { get; set; }

        [Required]
        public bool Completed { get; set; }

        public Order Order { get; set; }

    }
}
