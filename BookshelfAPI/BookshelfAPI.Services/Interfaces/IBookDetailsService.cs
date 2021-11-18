using BookshelfAPI.Services.DTOs.BookDetails;
using System.Threading.Tasks;

namespace BookshelfAPI.Services.Interfaces
{
    public interface IBookDetailsService
    {
        public Task<ServiceResponse> GetBookIssueDetails(int bookIssueId);
        public Task<ReadingStatusDto> GetBookReadingStatus(int bookIssueId);
    }
}
