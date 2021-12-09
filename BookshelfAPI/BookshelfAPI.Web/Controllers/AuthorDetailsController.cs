using BookshelfAPI.Services.Interfaces;
using BookshelfAPI.Services.RequestModels.AuthorDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace BookshelfAPI.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "reader")]
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

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateAuthor_RequestModel model)
        {
            var result = await _authorDetailsService.CreateAuthorAsync(model);
            return result.Succeeded ? Ok(result.Body) : BadRequest(result);
        }
    }
}
