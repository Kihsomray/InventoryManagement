using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models{

    public class Expense {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExpenseID { get; set; }

        public int LocationID { get; set; }

        public int ItemID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [MaxLength(50)]
        public string Method { get; set; }

        public bool Completed { get; set; }

        public Location Location { get; set; }

        public Item Item { get; set; }

    }

}
