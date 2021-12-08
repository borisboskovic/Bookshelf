using BookshelfAPI.Services.DTOs.Home;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookshelfAPI.Services.Interfaces
{
    public interface IHomePageService
    {
        public Task<ServiceResponse> GetPreviewItems();
        public Task<ServiceResponse> Search(string searchString);
        public List<AuthorItemDto> SearchAuthors(string[] keywords);
        public List<BookItemDto> SearchBooks(string[] keywords);
    }
}
