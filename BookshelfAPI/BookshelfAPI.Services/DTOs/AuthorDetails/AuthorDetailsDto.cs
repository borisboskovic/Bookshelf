using BookshelfAPI.Helpers;
using System;
using System.Collections.Generic;

namespace BookshelfAPI.Services.DTOs.AuthorDetails
{
    public class AuthorDetailsDto : ActionResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public string ImageUrl { get; set; }
        public string Bio { get; set; }
        public string PlaceOfBirth { get; set; }

        public virtual List<string> Genres { get; set; }
        public virtual List<BookSummaryDto> BookIssues { get; set; }
    }
}
