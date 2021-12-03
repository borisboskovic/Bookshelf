using System.Threading.Tasks;

namespace BookshelfAPI.Services.Interfaces
{
    public interface IHomePageService
    {
        public Task<ServiceResponse> GetPreviewItems();
    }
}
