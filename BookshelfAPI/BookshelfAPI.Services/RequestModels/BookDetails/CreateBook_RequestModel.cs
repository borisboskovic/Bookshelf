using System;

namespace BookshelfAPI.Services.RequestModels.BookDetails
{
    public class CreateBook_RequestModel
    {
        public string OriginalTitle { get; set; }
        public DateTime? OriginalReleaseDate { get; set; }
        public int[] AuthorIds { get; set; }
    }
}
