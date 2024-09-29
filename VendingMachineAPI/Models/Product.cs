using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendingMachineAPI.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Ensures this is auto-incremented by the database
        public int ProductNumber { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]  // Specify precision and scale
        public decimal Price { get; set; }
        [Required]
        public int Count { get; set; }
    }
}


//
