﻿using Microsoft.AspNetCore.Mvc;
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
        private IOrderService _orderService;
        public AdminController(IProductService productService, IOrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;
        }
        [Authorize(Roles = "ADMIN")]
        [HttpGet("/all")]
        public IActionResult GetProducts()
        {
            List<ProductDto> products = _productService.GetProducts();
            return Ok(products);
        }
        [Authorize(Roles = "ADMIN")]
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
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public IActionResult AddNewProduct([FromBody] ProductDto dto)
        {
            int id = _productService.AddNewProduct(dto);
            return Created("/api/v1/admin/products/{id}", id);
        }
        [Authorize(Roles = "ADMIN")]
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
        [Authorize(Roles = "ADMIN")]
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
        [Authorize(Roles = "ADMIN")]
        [HttpGet("/getOrders")]
        public IActionResult GetOrders()
        {
            try
            {
                var orders = _orderService.GetOrders();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Authorize(Roles = "ADMIN")]
        [HttpPut("/finalize/{id:Guid}")]
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
    }
}
