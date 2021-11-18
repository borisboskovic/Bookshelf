using System;

namespace BookshelfAPI.Services.DTOs.BookDetails
{
    public class ReadingStatusDto
    {
        public bool CurrentlyReading { get; set; } = false;
        public bool WantToRead { get; set; } = false;
        public bool Read { get; set; } = false;
        public DateTime? AddedOn { get; set; }
        public DateTime? StartedOn { get; set; }
        public DateTime? FinishedOn { get; set; }
        public int? PagesRead { get; set; }
    }
}
