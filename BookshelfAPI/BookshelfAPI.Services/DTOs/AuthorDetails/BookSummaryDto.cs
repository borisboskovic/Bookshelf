using BookshelfAPI.Data.Models;
using System.Collections.Generic;

namespace BookshelfAPI.Services.DTOs.AuthorDetails
{
    public class BookSummaryDto
    {
        public int BookId { get; set; }
        public int BookIssueId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public double? Rating { get; set; }
        public int RatingsCount { get; set; }
    }
}
