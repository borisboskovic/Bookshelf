using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BookshelfAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshelfAPI.Services.Services
{
    public class AzureStorageService : IAzureStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public AzureStorageService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<string> PrepareAndUploadFormFile(IFormFile formFile)
        {
            var readStream = formFile.OpenReadStream();
            string extension;
            try
            {
                extension = Path.GetExtension(formFile.FileName);
            }
            catch (ArgumentException) {
                return "";
            }
            var fileName = $"{Guid.NewGuid()}{extension}";
            return await UploadBlob(readStream, fileName) ? fileName : "";
        }

        public async Task<bool> UploadBlob(Stream imageStream, string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("bookshelf");
            var blobClient = containerClient.GetBlobClient(fileName);
            try
            {
                var response = await blobClient.UploadAsync(imageStream);
                return true;
            }
            catch (RequestFailedException e)
            {
                Log.Error(e.Message + "\r\n" + e.StackTrace);
                return false;
            }

        }
    }
}
