using BookshelfAPI.Services.RequestModels.AuthorDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshelfAPI.Services.Interfaces
{
    public interface IAuthorDetailsService
    {
        public Task<ServiceResponse> GetDetails(int authorId);
        public Task<ServiceResponse> CreateAuthorAsync(CreateAuthor_RequestModel model);
    }
}
