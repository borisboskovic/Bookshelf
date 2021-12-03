using BookshelfAPI.Data;
using BookshelfAPI.Services.DTOs.Home;
using BookshelfAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace BookshelfAPI.Services.Services
{
    public class HomePageService : IHomePageService
    {
        private readonly BookshelfDbContext _context;
        private readonly IConfiguration _configuration;

        public HomePageService(BookshelfDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ServiceResponse> GetPreviewItems()
        {

            var recentAuthors = _context.Author
                .OrderByDescending(e => e.Id)
                .Take(10)
                .AsEnumerable()
                .Select(e => new AuthorItemDto
                {
                    Id = e.Id,
                    Name = $"{e.Name} {e.Surname}",
                    ImageUrl = $"{_configuration["Azure:BlobStorageUrl"]}/{e.ImageUrl}"
                }).ToList();


            var recentBookIssues = await _context.BookIssue
                .Include(e => e.Book)
                .OrderByDescending(e => e.Id)
                .Take(10)
                .GroupJoin
                (
                    _context.Review,
                    bookIssue => bookIssue.Book_Id,
                    review => review.Book_Id,
                    (bookIssue, review) => new
                    {
                        BookId = bookIssue.Book_Id,
                        BookIssueId = bookIssue.Id,
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
                .Select(e => new BookItemDto
                {
                    BookId = e.Key.BookId,
                    BookIssueId = e.Key.BookIssueId,
                    ImageUrl = $"{_configuration["Azure:BlobStorageUrl"]}/{e.Key.ImageUrl}",
                    Title = e.Key.Title,
                    Rating = e.Average(bookReview => bookReview.Review.Rating),
                    RatingsCount = e.Count()
                })
                .ToListAsync();

            recentBookIssues.ForEach(b =>
            {
                if (b.Rating == null) b.RatingsCount = 0;
            });

            return new ServiceResponse
            {
                Succeeded = true,
                Body = new HomePageDto
                {
                    Authors = recentAuthors,
                    BookIssues = recentBookIssues,
                }
            };
        }
    }
}
