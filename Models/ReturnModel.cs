using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models {

    public class Return {

        [Key]
        [ForeignKey("Order")]
        public int OrderID { get; set; }

        public DateTime ReturnDate { get; set; }

        [MaxLength(255)]
        public string ReturnReason { get; set; }

        public Order Order { get; set; }
    }
}
