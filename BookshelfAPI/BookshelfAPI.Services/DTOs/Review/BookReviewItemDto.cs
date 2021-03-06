using BookshelfAPI.Helpers;
using System;
using System.Collections.Generic;

namespace BookshelfAPI.Services.DTOs.Review
{
    public class BookReviewItemDto : ActionResult
    {
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorImage { get; set; }
        public string Content { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
        public DateTime PostedOn { get; set; }
        public List<ReviewCommentDto> Comments { get; set; }
    }
}
