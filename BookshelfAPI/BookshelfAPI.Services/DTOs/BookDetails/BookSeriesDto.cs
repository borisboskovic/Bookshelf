namespace BookshelfAPI.Services.DTOs.BookDetails
{
    public class BookSeriesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? OrderInSeries { get; set; }
        public string ImageUrl { get; set; }
    }
}
