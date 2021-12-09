using BookshelfAPI.Services.DTOs.BookDetails;
using BookshelfAPI.Services.RequestModels.BookDetails;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookshelfAPI.Services.Interfaces
{
    public interface IBookDetailsService
    {
        public Task<ServiceResponse> GetBookIssueDetails(int bookIssueId);
        public Task<ReadingStatusDto> GetBookIssueReadingStatus(int bookIssueId);
        public Task<List<AuthorSummaryDto>> GetBookIssueAuthors(int bookId);
        public Task<ServiceResponse> CreateBookAsync(CreateBook_RequestModel model);
    }
}
