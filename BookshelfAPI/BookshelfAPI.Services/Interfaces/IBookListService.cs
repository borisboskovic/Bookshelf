using BookshelfAPI.Services.DTOs.BookList;
using BookshelfAPI.Services.RequestModels.BookList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookshelfAPI.Services.Interfaces
{
    public interface IBookListService
    {
        public List<CurrentlyReadingItemDto> GetCurrentlyReading();
        public Task<ServiceResponse> GetDefaultLists();
        public Task<ServiceResponse> UpdateReadingProgress(ReadingProgress_RequestModel model);
        public Task<ServiceResponse> FinishBokReading(int bookIssueId);
        public Task RemoveBookFromList(RemoveBook_RequestModel model);
        public Task AddBookToList(AddBook_RequestModel model);
    }
}
