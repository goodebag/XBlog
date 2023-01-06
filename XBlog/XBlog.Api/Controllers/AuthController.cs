using Microsoft.AspNetCore.Mvc;
using XBlog.Models.Models;
using XBlog.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XBlog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IDataService _dataService;

        public AuthController(IDataService dataService)
        {
            _dataService = dataService;
        }
        // GET: api/<AuthController>
        [HttpGet]
        public async Task<IActionResult> Login(SignInModel model)
        {
            var result = await _dataService.SignInAsync(model);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            var user = await _dataService.CreateAuthur(model);
            return Ok(user);
        }

      
    }
}
