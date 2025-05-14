using Microsoft.AspNetCore.Mvc;
using PollyHttpClientDemo.Services;

namespace PollyHttpClientDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ApiService _apiService;

        public TestController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _apiService.GetDataAsync();
            return Content(result, "application/json");
        }
    }
}
