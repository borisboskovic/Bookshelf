using BookshelfAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace BookshelfAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorDetailsController : ControllerBase
    {
        private readonly IAuthorDetailsService _authorDetailsService;

        public AuthorDetailsController(IAuthorDetailsService authorDetailsService)
        {
            _authorDetailsService = authorDetailsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDetails([FromQuery, Required] int? authorId)
        {
            var result = await _authorDetailsService.GetDetails((int)authorId);
            return result.Succeeded ? Ok(result.Body) : BadRequest(result);
        }
    }
}
