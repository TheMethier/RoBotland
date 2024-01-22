using _RoBotland.Enums;
using System.ComponentModel.DataAnnotations;

namespace _RoBotland.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public Availability IsAvailable { get; set; }
    }
}
