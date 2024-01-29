using Microsoft.AspNetCore.Mvc;
using _RoBotland.Interfaces;
using _RoBotland.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Query;
using _RoBotland.Enums;
using Newtonsoft.Json.Linq;


namespace _RoBotland.Controllers
{
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IProductService _productService;
        private IOrderService _orderService;
        public AdminController(IProductService productService, IOrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;
        }
        [Authorize(Roles = "ADMIN")]
        [HttpGet("products/all")]
        public IActionResult GetProducts()
        {
            List<ProductDto> products = _productService.GetProducts();
            return Ok(products);
        }
        [Authorize(Roles = "ADMIN")]
        [HttpGet("products/{id:int}")]
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
        [Authorize(Roles = "ADMIN")]
        [HttpPost("products/addProduct")]
        public IActionResult AddNewProduct([FromBody] ProductDto dto)
        {
            int id = _productService.AddNewProduct(dto);
            return Created("$/api/v1/products/{id}", id);
        }
        [Authorize(Roles = "ADMIN")]
        [HttpDelete("products/{id:int}")]
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
            return Ok(_productService.GetProducts());
        }
        [Authorize(Roles ="ADMIN")]
        [HttpPut("products/{id:int}")]
        public IActionResult UpgradeProduct(int id, [FromBody] ProductDto dto)
        {
            try
            {
                int productId = _productService.UpdateProduct(id, dto);
                return Created("$/api/v1/products/{id}", productId);
            }
            catch
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "ADMIN")]
        [HttpPost("categories/products")]
        public IActionResult AddCategoryToProduct([FromBody] AddCategoryToProductDto dto)
        {
            try
            {
                ICollection<Category> categories = _productService.AddCategoryToProduct(dto.CategoryNames, dto.ProductId);
                return Ok(categories);
            }
            catch
            {
                return StatusCode(500, "Wystąpił błąd podczas przetwarzania żądania.");
            }
        }
        [Authorize(Roles = "ADMIN")]
        [HttpGet("products/filtred")]
        public IActionResult GetProducts([FromQuery] ProductFilterDto filterParameters)
        {
            var products = _productService.GetFilteredProducts(filterParameters);
            return Ok(products);
        }        
        [Authorize(Roles = "ADMIN")]
        [HttpPut("products/changeProductAvailability/{id:int}")]
        public IActionResult ChangeProductAvailability(Availability availability, int id)
        {
            try
            {
                var productId = _productService.ChangeProductAvailability(availability, id);
                return Created("/api/v1/admin/products/{id}", productId);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
        [Authorize(Roles = "ADMIN")]
        [HttpGet("products/searched")]
        public IActionResult SearchProductsByName(string productName)
        {
            var products = _productService.SearchProductsByName(productName);
            return Ok(products);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPut("changeOrderStatus/{id:Guid}")]
        public IActionResult ChangeOrderStatus(Guid id,[FromBody] OrderStatus orderStatus)
        {
            try
            { 
                var order = _orderService.ChangeOrderStatus(id,orderStatus);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Authorize(Roles = "ADMIN")]
        [HttpGet("categories/getProductCategories/{productId:int}")]
        public IActionResult GetProductCategories(int productId)
        {
            try
            {
                List<Category> categories = _productService.GetProductCategories(productId);
                return Ok(categories);
            }
            catch
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "ADMIN")]
        [HttpGet("getOrders")]
        public IActionResult GetOrders()
        {
            List<OrderDto> orders = _orderService.GetOrders();
            return Ok(orders);
        }
    }
}

