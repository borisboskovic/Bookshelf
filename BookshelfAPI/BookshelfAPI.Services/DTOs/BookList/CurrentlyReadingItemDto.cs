using BookshelfAPI.Helpers;
using System;
using System.Collections.Generic;

namespace BookshelfAPI.Services.DTOs.BookList
{
    public class CurrentlyReadingItemDto
    {
        public int BookIssueId { get; set; }
        public string Title { get; set; }
        public DateTime AddedOn { get; set; }
        public int PagesRead { get; set; }
        public int TotalPages { get; set; }
        public string ImageUrl { get; set; }

        public List<AuthorBasicInfoDto> Authors { get; set; }
    }
}
