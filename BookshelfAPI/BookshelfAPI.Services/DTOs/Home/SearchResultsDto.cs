using BookshelfAPI.Helpers;
using System.Collections.Generic;

namespace BookshelfAPI.Services.DTOs.Home
{
    public class SearchResultsDto : ActionResult
    {
        public List<AuthorItemDto> Authors { get; set; }
        public List<BookItemDto> Books { get; set; }
    }
}
