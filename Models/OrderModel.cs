using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models {

    public class Order {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        public int CustomerID { get; set; }

        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        public Customer Customer { get; set; }
    }
}
