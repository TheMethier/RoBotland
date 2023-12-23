using _RoBotland.Enums;
using System.ComponentModel.DataAnnotations;

namespace _RoBotland.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
    
        public string Name { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public Availability IsAvailable { get; set; }
    }
}
