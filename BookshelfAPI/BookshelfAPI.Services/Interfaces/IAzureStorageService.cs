using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace BookshelfAPI.Services.Interfaces
{
    public interface IAzureStorageService
    {
        Task<string> PrepareAndUploadFormFile(IFormFile formFile);
        Task<bool> UploadBlob(Stream imageStream, string fileName);
    }
}
