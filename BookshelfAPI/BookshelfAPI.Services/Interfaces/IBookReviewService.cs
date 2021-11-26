using BookshelfAPI.Services.DTOs.Review;
using BookshelfAPI.Services.RequestModels.Review;
using System.Threading.Tasks;

namespace BookshelfAPI.Services.Interfaces
{
    public interface IBookReviewService
    {
        public Task<ServiceResponse> RateBookIssue(RateBookIssue_RequestModel model);
        public Task<RatingsSummaryDto> GetBookRatings(int bookId);
        public Task<ServiceResponse> GetReviews(int bookIssueId);
        public Task<ServiceResponse> AddBookIssueReview(ReviewBookIssue_RequestModel model);
    }
}
