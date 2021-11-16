using BookshelfAPI.Services.DTOs.BookDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshelfAPI.Services.Interfaces
{
    public interface IBookDetailsService
    {
        public Task<ServiceResponse> GetBookIssueDetails(int bookIssueId);
    }
}
