using System.ComponentModel.DataAnnotations;

namespace VendingMachineAPI.Models
{
    public struct ProductStruct
    {
        [Key]
        public int ProductNumber { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Count { get; set; }
    }
}
