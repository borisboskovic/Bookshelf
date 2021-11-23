using BookshelfAPI.Helpers;
using System.Collections.Generic;

namespace BookshelfAPI.Services.DTOs.BookList
{
    public class BookListsDto : ActionResult
    {
        public List<CommonBookItemDto> CurrentlyReading { get; set; }
        public List<CommonBookItemDto> Read { get; set; }
        public List<CommonBookItemDto> WantToRead { get; set; }
    }
}
