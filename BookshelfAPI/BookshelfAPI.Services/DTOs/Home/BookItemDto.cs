namespace BookshelfAPI.Services.DTOs.Home
{
    public class BookItemDto
    {
        public int BookId { get; set; }
        public int BookIssueId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public double? Rating { get; set; }
        public int RatingsCount { get; set; }
    }
}
