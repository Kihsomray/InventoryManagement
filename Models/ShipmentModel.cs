using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models {
    public class Shipment {
        [Key]
        [ForeignKey("Order")]
        public int OrderID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; }

        [Required]
        [MaxLength(20)]
        public string ShippingNumber { get; set; }

        public Order Order { get; set; }
    }
}
