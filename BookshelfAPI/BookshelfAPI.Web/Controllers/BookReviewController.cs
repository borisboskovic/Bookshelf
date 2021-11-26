using BookshelfAPI.Services.Interfaces;
using BookshelfAPI.Services.RequestModels.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookshelfAPI.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "reader")]
    public class BookReviewController : ControllerBase
    {
        private readonly IBookReviewService _reviewService;

        public BookReviewController(IBookReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost("RateBook")]
        public async Task<IActionResult> RateBookIssue(RateBookIssue_RequestModel model)
        {
            var result = await _reviewService.RateBookIssue(model);
            return result.Succeeded ? Ok(result.Body) : BadRequest(result);
        }

        [HttpPost("AddReview")]
        public async Task<IActionResult> AddBookIssueReview(ReviewBookIssue_RequestModel model)
        {
            var result = await _reviewService.AddBookIssueReview(model);
            return result.Succeeded ? Ok(result.Body) : BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetReviews([FromQuery, Required] int? bookIssueId)
        {
            var response = await _reviewService.GetReviews((int)bookIssueId);
            return response.Succeeded ? Ok(response.Body) : BadRequest(response);
        }
    }
}
