using BookshelfAPI.Services.DTOs.BookDetails;
using BookshelfAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshelfAPI.Services.Services
{
    public class BookDetailsService : IBookDetailsService
    {
        public async Task<ServiceResponse> GetBookIssueDetails(int bookIssueId)
        {
            throw new NotImplementedException();
        }
    }
}
