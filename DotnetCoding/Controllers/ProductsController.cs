using Microsoft.AspNetCore.Mvc;
using DotnetCoding.Core.Models;
using DotnetCoding.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace DotnetCoding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get the list of product
        /// </summary>
        /// <returns></returns>
        [HttpGet("all-products")]
        public async Task<IActionResult> GetProductList(string productName, DateTime createdDate, decimal productPrice)
        {
            var productDetailsList = await _productService.GetAllProducts(productName, createdDate, productPrice);
            
            if(productDetailsList == null)
            {
                return NotFound();
            }
            return Ok(productDetailsList.ToList());
        }

        [HttpGet("pending-approval-queue")]
        public async Task<IActionResult> GetPendingApprovalProducts()
        {
            var productDetailsList = await _productService.GetPendingApprovalProducts();
            //productDetailsList.Where()
            if (productDetailsList == null)
            {
                return NotFound();
            }
            return Ok(productDetailsList);
        }
        [HttpPost("create-product")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDetails productDetails)
        {
            var productDetailsList = await _productService.CreateProduct(productDetails);
            return Ok(productDetailsList);
        }
        [HttpPut("update-product")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDetails productDetails)
        {
            var productDetailsList = await _productService.UpdateProduct(productDetails);
            return Ok(productDetailsList);
        }
        [HttpDelete("delete-product")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var productDetailsList = await _productService.DeleteProduct(productId);
            return Ok(productDetailsList);
        }

    }
}
