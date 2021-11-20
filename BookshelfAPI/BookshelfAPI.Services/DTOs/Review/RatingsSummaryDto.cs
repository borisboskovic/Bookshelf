using BookshelfAPI.Helpers;

namespace BookshelfAPI.Services.DTOs.Review
{
    public class RatingsSummaryDto:ActionResult
    {
        public double? Average { get; set; }
        public int Count { get; set; }
        public int? YourRating { get; set; }
    }
}
