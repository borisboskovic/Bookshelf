using BookshelfAPI.Services.Interfaces;
using BookshelfAPI.Services.RequestModels.BookDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace BookshelfAPI.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "reader")]
    public class BookDetailsController : ControllerBase
    {
        private readonly IBookDetailsService _bookDetailsService;

        public BookDetailsController(IBookDetailsService bookDetailsService)
        {
            _bookDetailsService = bookDetailsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDetails([FromQuery, Required] int? bookIssueId)
        {
            var result = await _bookDetailsService.GetBookIssueDetails((int)bookIssueId);
            return result.Succeeded ? Ok(result.Body) : BadRequest(result);
        }

        [HttpPost("CreateBook")]
        public async Task<IActionResult> CreateBook(CreateBook_RequestModel model)
        {
            var result = await _bookDetailsService.CreateBookAsync(model);
            return result.Succeeded ? Ok(result.Body) : BadRequest(result);
        }
    }
}
