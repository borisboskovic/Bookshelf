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
                    bookAuthor => bookAuthor.Book_Id,
                    bookIssue => bookIssue.Book.Id,
                    (book, bookIssue) => new
                    {
                        BookId = bookIssue.Book_Id,
                        BookIssueId = bookIssue.Id,
                        ImageUrl = bookIssue.ImageUrl,
                        Title = bookIssue.Title
                    }
                )
                .GroupJoin
                (
                    _context.Review,
                    bookIssue => bookIssue.BookId,
                    review => review.Book.Id,
                    (bookIssue, review) => new
                    {
                        BookId = bookIssue.BookId,
                        BookIssueId = bookIssue.BookIssueId,
                        ImageUrl = bookIssue.ImageUrl,
                        Title = bookIssue.Title,
                        Review = review
                    }
                )
                .SelectMany
                (
                    bookIssue => bookIssue.Review.DefaultIfEmpty(),
                    (bookIssue, review) => new
                    {
                        bookIssue.BookId,
                        bookIssue.BookIssueId,
                        bookIssue.ImageUrl,
                        bookIssue.Title,
                        Review = review
                    }
                )
                .GroupBy(e => new { e.BookId, e.BookIssueId, e.ImageUrl, e.Title })
                .Select(e => new BookSummaryDto
                {
                    BookId = e.Key.BookId,
                    BookIssueId = e.Key.BookIssueId,
                    ImageUrl = $"{_configuration["Azure:BlobStorageUrl"]}/{e.Key.ImageUrl}",
                    Title = e.Key.Title,
                    Rating = e.Average(bookReview => bookReview.Review.Rating),
                    ReviewsCount = e.Count()
                })
                .ToListAsync();

            bookIssues.ForEach(b =>
            {
                if (b.Rating == null) b.ReviewsCount = 0;
            });

            var genres = _context.BookAuthor
                .Where(e => e.Author_Id == authorId)
                .Join
                (
                    _context.BookTag,
                    bookAuthor => bookAuthor.Book_Id,
                    bookTag => bookTag.Book_Id,
                    (bookAuthor, bookTag) => new
                    {
                        bookTag.Tag.Name
                    }
                )
                .Distinct()
                .Select(e => e.Name)
                .ToList();

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
                BookIssues = bookIssues,
                Genres = genres
            };
            return response;
        }
    }
}
