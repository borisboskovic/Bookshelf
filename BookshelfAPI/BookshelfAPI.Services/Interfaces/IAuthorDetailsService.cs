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
    }
}
