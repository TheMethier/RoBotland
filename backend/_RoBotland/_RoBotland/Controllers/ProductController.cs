using _RoBotland.Interfaces;
using _RoBotland.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("/all")]
        public IActionResult GetProducts()
        {
            List<Product> products = _productService.GetProducts();
            return Ok(products);
        }

        [HttpGet("/{id:int}")]
        public IActionResult GetProductById(int id)
        {
            Product product = _productService.GetProductById(id);
            return Ok(product);
        }

        [HttpPost]
        public IActionResult AddNewProduct([FromBody] Product product)
        {
            int id = _productService.AddNewProduct(product);
            return Created("$/api/v1/products/{id}",id);
        }

        [HttpDelete("/{id:int}")]
        public IActionResult RemoveProductById(int id)
        {
            bool isDeleted = _productService.DeleteProduct(id);
            if(isDeleted)
                return NoContent();
            return NotFound();
        }

        [HttpPut("/{id:int}")]
        public IActionResult UpgradeProduct(int id,[FromBody] Product product)
        {
            int productId= _productService.UpdateProduct(id,product);
            if(productId!=-1)
                return Created("$/api/v1/products/{id}",productId);
            return NotFound();
        }
    }
}
