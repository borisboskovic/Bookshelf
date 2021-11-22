using BookshelfAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshelfAPI.Services.DTOs.BookList
{
    public class CurrentlyReadingItemUpdatedDto : ActionResult
    {
        public int BookIssueId { get; set; }
        public int PagesRead { get; set; }
    }
}
