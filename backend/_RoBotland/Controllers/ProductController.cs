using _RoBotland.Interfaces;
using _RoBotland.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;

namespace _RoBotland.Controllers
{
    [Route("/api/v1/products/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private  IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("all")]
        public IActionResult GetProducts()
        {
            List<ProductDto> products = _productService.GetProducts();
            return Ok(products);
        }

        [HttpGet("categories")]
        public IActionResult GetCategories()
        {
            List<CategoryDto> categories = _productService.GetCategories();
            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetProductById(int id)
        {
            try
            {
                ProductDto product = _productService.GetProductById(id);
                return Ok(product);
            }
            catch 
            {
                return NotFound();
            }
        }
        [HttpGet("filtred")]
        public IActionResult GetProducts([FromQuery] ProductFilterDto filterParameters)
        {
            var products = _productService.GetFilteredProducts(filterParameters);
            return Ok(products);
        }
        [HttpGet("searched")]
        public IActionResult SearchProductsByName(string productName)
        {
            var products = _productService.SearchProductsByName(productName);
            return Ok(products);
        }
     
     
        [HttpPost("addCategoryToProduct")]
        public IActionResult AddCategoryToProduct([FromBody] AddCategoryToProductDto dto)
        {
            int id = _productService.AddCategoryToProduct(dto.CategoryId, dto.ProductId);
            return Ok(id);
        }

    }
}
