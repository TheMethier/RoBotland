using _RoBotland.Enums;

namespace _RoBotland.Models
{
    public class ProductFilterDto
    {
        public float? MinPrice { get; set; }
        public float? MaxPrice { get; set; }
        public int? CategoryId { get; set; }
        public Availability? IsAvailable { get; set; }
        
    }

}
