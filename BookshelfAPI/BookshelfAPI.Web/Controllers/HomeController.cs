using BookshelfAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookshelfAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHomePageService _homePageService;

        public HomeController(IHomePageService homePageService)
        {
            _homePageService = homePageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var result = await _homePageService.GetPreviewItems();
            return result.Succeeded ? Ok(result.Body): BadRequest(result);
        }
    }
}
