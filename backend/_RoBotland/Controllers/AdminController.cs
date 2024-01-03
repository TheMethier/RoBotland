using Microsoft.AspNetCore.Mvc;
using _RoBotland.Interfaces;
using _RoBotland.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Query;
using _RoBotland.Enums;

namespace _RoBotland.Controllers
{
    [Route("/api/v1/admin/products/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IProductService _productService;
        public AdminController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("/products")]
        public IActionResult GetProducts()
        {
            Console.WriteLine("Hello, World!");
            List<ProductDto> products = _productService.GetProducts();
            return Ok(products);
        }
        [HttpGet("/products/{id:int}")]
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
        //[Authorize(Roles = "ADMIN")]
        [HttpPost]
        public IActionResult AddNewProduct([FromBody] ProductDto dto)
        {
            int id = _productService.AddNewProduct(dto);
            return Created("/api/v1/admin/products/{id}", id);
        }
        //[Authorize(Roles = "ADMIN")]
        [HttpDelete("{id:int}")]
        public IActionResult RemoveProductById(int id)
        {
            try
            {
                _productService.DeleteProduct(id);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
            return NoContent();
        }
        //[Authorize(Roles = "ADMIN")]
        [HttpPut("{id:int}")]
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
        //[Authorize(Roles = "ADMIN")]
        [HttpPost("categories/products")]
        public IActionResult AddCategoryToProduct([FromBody] AddCategoryToProductDto dto)
        {
            try
            {
                int id = _productService.AddCategoryToProduct(dto.CategoryId, dto.ProductId);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Wystąpił błąd podczas przetwarzania żądania.");
            }
        }
        [HttpGet("/products/filtred")]
        public IActionResult GetProducts([FromQuery] ProductFilterDto filterParameters)
        {
            var products = _productService.GetFilteredProducts(filterParameters);
            return Ok(products);
        }
        [HttpGet("/products/searched")]
        public IActionResult SearchProductsByName(string productName)
        {
            var products = _productService.SearchProductsByName(productName);
            return Ok(products);
        }
        [Authorize(Roles = "ADMIN")]
        [HttpPut("/products/{id:int}/ChangeProductAvailability")]
        public IActionResult ChangeProductAvailability(Availability availability, [FromBody] ProductDto dto)
        {
            try
            {
                var productId = _productService.ChangeProductAvailability(availability, dto);
                return Created("/api/v1/admin/products/{id}", productId);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}
