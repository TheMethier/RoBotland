using Microsoft.AspNetCore.Mvc;
using _RoBotland.Interfaces;
using _RoBotland.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Query;


namespace _RoBotland.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("/api/v1/admin/products/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IProductService _productService;

        public AdminController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("/all")]
        public IActionResult GetProducts()
        {
            List<ProductDto> products = _productService.GetProducts();
            return Ok(products);
        }

        [HttpGet("/{id:int}")]
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

        [HttpPost]
        public IActionResult AddNewProduct([FromBody] ProductDto dto)
        {
            int id = _productService.AddNewProduct(dto);
            return Created("/api/v1/admin/products/{id}", id);
        }

        [HttpDelete("/{id:int}")]
        public IActionResult RemoveProductById(int id)
        {
            try
            {
                _productService.DeleteProduct(id);
            }
            catch
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("/{id:int}")]
        public IActionResult UpgradeProduct(int id, [FromBody] ProductDto dto)
        {
            try
            {
                int productId = _productService.UpdateProduct(id, dto);
                return Created("/api/v1/admin/products/{id}", productId);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}
