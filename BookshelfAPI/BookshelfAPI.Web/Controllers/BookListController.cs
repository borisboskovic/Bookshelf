using BookshelfAPI.Services.Interfaces;
using BookshelfAPI.Services.RequestModels.BookList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookshelfAPI.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "reader")]
    public class BookListController : ControllerBase
    {
        private readonly IBookListService _bookListService;

        public BookListController(IBookListService bookListService)
        {
            _bookListService = bookListService;
        }

        [HttpGet("CurrentlyReading")]
        public IActionResult GetCurrentlyReading()
        {
            var items = _bookListService.GetCurrentlyReading();
            return Ok(items);
        }

        [HttpPut("UpdateProgress")]
        public async Task<IActionResult> UpdateReadingprogress(ReadingProgress_RequestModel model)
        {
            var result = await _bookListService.UpdateReadingProgress(model);
            return result.Succeeded ? Ok(result.Body) : BadRequest(result);
        }

        [HttpPut("FinishBook")]
        public async Task<IActionResult> FinishBookReading([FromQuery] int bookIssueId)
        {
            var result = await _bookListService.FinishBokReading(bookIssueId);
            return result.Succeeded ? Ok() : BadRequest();
        }
    }
}
