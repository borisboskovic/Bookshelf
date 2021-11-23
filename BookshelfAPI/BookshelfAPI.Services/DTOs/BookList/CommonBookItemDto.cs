using BookshelfAPI.Services.DTOs.Review;
using System;
using System.Collections.Generic;

namespace BookshelfAPI.Services.DTOs.BookList
{
    public class CommonBookItemDto
    {
        public int BookIssueId { get; set; }
        public string Title { get; set; }
        public DateTime AddedOn { get; set; }
        public string ImageUrl { get; set; }
        public RatingsSummaryDto Ratings { get; set; }

        public List<AuthorBasicInfoDto> Authors { get; set; }
    }
}
