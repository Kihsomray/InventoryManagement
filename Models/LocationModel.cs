using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Location {
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LocationID { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(255)]
    public string Address { get; set; }
}