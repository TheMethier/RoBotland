﻿using _RoBotland.Enums;

namespace _RoBotland.Models
{
    public class ProductFilterDto
    {
        public float? MinPrice { get; set; }
        public float? MaxPrice { get; set; }
        public Availability? IsAvailable { get; set; }
        public string? CategoryName { get; set; }
        
    }

}
