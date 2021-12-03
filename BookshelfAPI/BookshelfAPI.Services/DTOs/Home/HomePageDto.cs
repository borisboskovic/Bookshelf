using BookshelfAPI.Helpers;
using System.Collections.Generic;

namespace BookshelfAPI.Services.DTOs.Home
{
    public class HomePageDto : ActionResult
    {
        public List<AuthorItemDto> Authors { get; set; }
        public List<BookItemDto> BookIssues { get; set; }
    }
}
