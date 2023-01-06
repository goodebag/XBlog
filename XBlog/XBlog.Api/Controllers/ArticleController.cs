using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XBlog.Models.Entities;
using XBlog.Models.Models;
using XBlog.Services.Implementations;
using XBlog.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XBlog.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly IArticleService _articleService;

        public ArticleController(IDataService dataService, IArticleService articleService)
        {
            _dataService = dataService;
            _articleService = articleService;
        }
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
          var article = await _articleService.GetArticleByIdAsync(id);
          await  _dataService.TrackArticle(id);
          return Ok(article);
        }
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await _dataService.GetCategoriesAsync());
        }
        [HttpGet]
        public async Task<IActionResult> GetByCategoryId(Guid categoryId)
        {
            var articles = await _articleService.GetArticlesByCategoryAsync(categoryId);
            return Ok(articles);
        }
        [HttpGet]
        public async Task<IActionResult> GetByHeadline(string headline)
        {
            var articles = await _articleService.GetArticlesByHeadlineAsync(headline);
            return Ok(articles);
        }
        [HttpGet]
        public async Task<IActionResult> GetByAuthur(string authur)
        {
            var articles = await _articleService.GetArticlesByAuthurAsync(authur);
            return Ok(articles);
        }
        [HttpGet]
        public async Task<IActionResult> GetByAuthurId(Guid authurId)
        {
            var articles = await _articleService.GetAllArticleAsync(authurId);
            return Ok(articles);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] Article model)
        {
            var articles = await _articleService.UpdateArticleAsync(model);
            return Ok(articles);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] ArticleCreationModel model)
        {
           var authurId = await  _dataService.GetAuthurId(User);
            var articles = await _articleService.CreateArticleAsync(model,authurId);
            return Ok(articles);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddOrModifyReation([FromBody] ReationModel model)
        {
            var userId = await _dataService.GetUserId(User);
            var articles = await _dataService.AddOrModifyReationAsync(model, userId);
            return Ok(articles);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddComent([FromBody] CommentModel model)
        {
            var userId = await _dataService.GetUserId(User);
            var articles = await _dataService.AddCommentsAsync(model, userId);
            return Ok(articles);
        }
    }
}
