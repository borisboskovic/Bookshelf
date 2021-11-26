using BookshelfAPI.Helpers;
using System.Collections.Generic;

namespace BookshelfAPI.Services.DTOs.Review
{
    public class BookReviewsDto : ActionResult
    {
        public BookReviewItemDto MyReview { get; set; }
        public List<BookReviewItemDto> OtherReviews { get; set; }
    }
}
