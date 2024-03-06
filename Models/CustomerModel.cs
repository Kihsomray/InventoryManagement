using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models {

    public class Customer {

        [Key]
        public int CustomerID { get; set; }

        [Required]
        [MaxLength(255)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(255)]
        public string Address { get; set; }

        [Required]
        public DateTime DateOfCreation { get; set; }

    }
}
