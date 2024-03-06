using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeID { get; set; }

        public int LocationID { get; set; }

        [Required]
        [MaxLength(255)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Position { get; set; }

        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        // Navigation property for the location
        public Location Location { get; set; }
    }
}
