using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XBlog.Models.Entities;
using XBlog.Models.Models;
using XBlog.Services.Implementations;
using XBlog.Services.Interfaces;

namespace XBlog.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly IArticleService _articleService;
        public ProductController(IDataService dataService, IArticleService articleService)
        {
            _dataService = dataService;
            _articleService = articleService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit([FromBody] ProductEditModel model)
        {
            var userId = await _dataService.GetUserId(User);
            var product = await _dataService.ModifyProductAsync(model, userId);
            return Ok(product);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Order([FromBody] IEnumerable<Guid> products)
        {
            var userId = await _dataService.GetUserId(User);
            var order = await _dataService.OrderProductAsync(products, userId);
            return Ok(order);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(Guid orderId)
        {
            var order = await _dataService.DeleteOrderAsync(orderId);
            return Ok(order);
        }
        [HttpPatch]
        public async Task<IActionResult> RemoveProductFromOrder(Guid orderId,Guid productId)
        {
            var order = await _dataService.RemoveProductFromOrderAsync(orderId,productId);
            return Ok(order);
        }
        [HttpPost]
        public async Task<IActionResult> AddProductFromOrder([FromBody] Guid orderId, Guid productId)
        {
            var order = await _dataService.AddProductToOrderAsync(orderId, productId);
            return Ok(order);
        }
    }
}

