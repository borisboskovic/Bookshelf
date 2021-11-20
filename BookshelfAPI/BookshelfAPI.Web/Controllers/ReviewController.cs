using BookshelfAPI.Services.Interfaces;
using BookshelfAPI.Services.RequestModels.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookshelfAPI.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "reader")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost("RateBook")]
        public async Task<IActionResult> RateBookIssue(RateBookIssue_RequestModel model)
        {
            var result = await _reviewService.RateBookIssue(model);
            return result.Succeeded ? Ok(result.Body) : BadRequest(result);
        }
    }
}
