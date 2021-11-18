using BookshelfAPI.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BookshelfAPI.Services.DTOs.BookDetails
{
    public class BookIssueDetailsDto : ActionResult
    {
        public int BookId { get; set; }
        public int BookIssueId { get; set; }
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public string Summary { get; set; }
        public string ImageUrl { get; set; }
        [JsonProperty("ISBN")]
        public string ISBN { get; set; }
        [JsonProperty("ISBN13")]
        public string ISBN13 { get; set; }
        public string Language { get; set; }
        public DateTime? PublishedOn { get; set; }
        public string OriginalPublishedOn { get; set; }
        public BookPublisherDto Publisher { get; set; }
        public BookSeriesDto Series { get; set; }
        public ReadingStatusDto ReadingStatus { get; set; }

        public virtual List<BookTagDto> Tags { get; set; }
        public virtual List<AuthorSummaryDto> Authors { get; set; }
    }
}
