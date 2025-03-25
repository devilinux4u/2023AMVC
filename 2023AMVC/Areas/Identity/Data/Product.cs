using Microsoft.CodeAnalysis.Classification;
using System.ComponentModel.DataAnnotations;

namespace _2023AMVC.Areas.Identity.Data
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        public int Quantity { get; set; }
    }
}
