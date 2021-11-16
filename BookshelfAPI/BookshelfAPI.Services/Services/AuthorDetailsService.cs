using BookshelfAPI.Data;
using BookshelfAPI.Services.DTOs.AuthorDetails;
using BookshelfAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshelfAPI.Services.Services
{
    public class AuthorDetailsService : IAuthorDetailsService
    {
        private readonly BookshelfDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthorDetailsService(BookshelfDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ServiceResponse> GetDetails(int authorId)
        {
            var response = new ServiceResponse();

            var author = _context.Author.FirstOrDefault(e => e.Id == authorId);
            if (author == null)
            {
                response.Errors.Add("Error", new string[] { "Author not found." });
                return response;
            }

            var bookIssues = await _context.BookAuthor
                .Where(ba => ba.Author_Id == authorId)
                .Include(ba => ba.Book)
                .Join
                (
                    _context.BookIssue,
                    book => book.Book_Id,
                    bookIssue => bookIssue.Book.Id,
                    (book, bookIssue) => new BookSummaryDto
                    {
                        BookId = bookIssue.Book_Id,
                        BookIssueId = bookIssue.Id,
                        ImageUrl = $"{_configuration["Azure:BlobStorageUrl"]}/{bookIssue.ImageUrl}",
                        Title = bookIssue.Title
                    }
                ).ToListAsync();

            response.Succeeded = true;
            response.Body = new AuthorDetailsDto
            {
                Bio = author.Bio,
                Name = author.Name,
                Surname = author.Surname,
                DateOfBirth = author.DateOfBirth,
                DateOfDeath = author.DateOfDeath,
                ImageUrl = $"{_configuration["Azure:BlobStorageUrl"]}/{author.ImageUrl}",
                PlaceOfBirth = author.PlaceOfBirth,
                BookIssues = bookIssues
            };
            return response;
        }
    }
}
