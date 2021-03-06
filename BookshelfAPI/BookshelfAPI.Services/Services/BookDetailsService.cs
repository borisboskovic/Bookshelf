using BookshelfAPI.Data;
using BookshelfAPI.Data.Models;
using BookshelfAPI.Services.DTOs.BookDetails;
using BookshelfAPI.Services.Helpers;
using BookshelfAPI.Services.Interfaces;
using BookshelfAPI.Services.RequestModels.BookDetails;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookshelfAPI.Services.Services
{
    public class BookDetailsService : IBookDetailsService
    {
        private readonly BookshelfDbContext _context;
        private readonly IUserService _userService;
        private readonly IBookReviewService _reviewService;
        private readonly IConfiguration _configuration;
        private readonly IAzureStorageService _azureStorageService;

        public BookDetailsService
        (
            BookshelfDbContext context,
            IUserService userService,
            IBookReviewService reviewService,
            IConfiguration configuration,
            IAzureStorageService azureStorageService
        )
        {
            _context = context;
            _userService = userService;
            _reviewService = reviewService;
            _configuration = configuration;
            _azureStorageService = azureStorageService;
        }

        public async Task<ServiceResponse> GetBookIssueDetails(int bookIssueId)
        {
            var response = new ServiceResponse();

            var bookIssue = await _context.BookIssue
                .Include(e => e.Publisher)
                .Include(e => e.Language)
                .Include(e => e.Book).ThenInclude(e => e.Series)
                .Include(e => e.Book).ThenInclude(e => e.BookTag).ThenInclude(e => e.Tag)
                .Select(e => new BookIssueDetailsDto
                {
                    BookIssueId = e.Id,
                    BookId = e.Book_Id,
                    Title = e.Title,
                    OriginalTitle = e.Book.OriginalTitle,
                    ImageUrl = e.ImageUrl,
                    Summary = e.Summary,
                    ISBN = e.ISBN,
                    ISBN13 = e.ISBN13,
                    PublishedOn = e.PublishedOn,
                    OriginalPublishedOn = e.Book.ReleaseDate != null ? e.Book.ReleaseDate.Value.ToShortDateString() : e.Book.ReleaseYearOnly.ToString(),
                    Language = e.Language.NativeName,
                    Series = e.Book.Series != null ? new BookSeriesDto
                    {
                        Id = e.Book.Series.Id,
                        Name = e.Book.Series.Name,
                        Description = e.Book.Series.Description,
                        ImageUrl = e.Book.Series.ImageUrl,
                        OrderInSeries = e.Book.OrderInSeries
                    } : null,
                    Publisher = new BookPublisherDto
                    {
                        Id = e.Publisher.Id,
                        Name = e.Publisher.Name,
                        ImageUrl = $"{_configuration["Azure:BlobStorageUrl"]}/{e.Publisher.ImageUrl}",
                        Address = e.Publisher.Address
                    },
                    Tags = e.Book.BookTag.Select(tag => new BookTagDto
                    {
                        Id = tag.Tag.Id,
                        Name = tag.Tag.Name
                    }).ToList(),
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.BookIssueId == bookIssueId);

            if (bookIssue == null)
            {
                response.Errors.Add("Error", new string[] { "Book issue not found" });
                return response;
            }

            bookIssue.ImageUrl = $"{_configuration["Azure:BlobStorageUrl"]}/{bookIssue.ImageUrl}";
            bookIssue.ReadingStatus = await GetBookIssueReadingStatus(bookIssueId);
            bookIssue.Authors = await GetBookIssueAuthors(bookIssue.BookId);
            bookIssue.Ratings = await _reviewService.GetBookRatings(bookIssue.BookId);
            bookIssue.List = await GetBookIssueCurrentList(bookIssueId);

            response.Succeeded = true;
            response.Body = bookIssue;

            return response;
        }

        public async Task<ReadingStatusDto> GetBookIssueReadingStatus(int bookIssueId)
        {
            var currentlyReading = await _context.CurrentlyReading
                .Where(e => e.BookIssue_Id == bookIssueId)
                .Where(e => e.User_Id == _userService.User.Id)
                .Select(e => new
                {
                    e.PagesRead,
                    e.AddedOn
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (currentlyReading != null)
            {
                return new ReadingStatusDto
                {
                    CurrentlyReading = true,
                    AddedOn = currentlyReading.AddedOn,
                    PagesRead = currentlyReading.PagesRead
                };
            }

            var wantToRead = await _context.WantToRead
                .Where(e => e.BookIsse_Id == bookIssueId)
                .Where(e => e.User_Id == _userService.User.Id)
                .Select(e => new
                {
                    e.AddedOn,
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (wantToRead != null)
            {
                return new ReadingStatusDto
                {
                    WantToRead = true,
                    AddedOn = wantToRead.AddedOn,
                };
            }

            var read = await _context.Read
                .Where(e => e.BookIssue_Id == bookIssueId)
                .Where(e => e.User_Id == _userService.User.Id)
                .Select(e => new
                {
                    e.AddedOn,
                    e.StartedOn,
                    e.FinishedOn,
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (read != null)
            {
                return new ReadingStatusDto
                {
                    Read = true,
                    StartedOn = read.StartedOn,
                    FinishedOn = read.FinishedOn
                };
            }

            return null;
        }

        public async Task<List<AuthorSummaryDto>> GetBookIssueAuthors(int bookId)
        {
            var authors = await _context.BookAuthor
                .Include(e => e.Author)
                .Where(e => e.Book_Id == bookId)
                .Select(e => new
                {
                    Id = e.Author_Id,
                    Name = e.Author.Name,
                    Surname = e.Author.Surname
                })
                .ToListAsync();

            return authors
                .Select(e => new AuthorSummaryDto
                {
                    Id = e.Id,
                    Name = $"{e.Name} {e.Surname}",
                })
                .ToList();
        }

        private async Task<string> GetBookIssueCurrentList(int bookIssueId)
        {
            var currentlyReading = await _context.CurrentlyReading
                .Where(e => e.BookIssue_Id == bookIssueId)
                .Where(e => e.User_Id == _userService.User.Id)
                .FirstOrDefaultAsync();
            if (currentlyReading != null)
            {
                return BookshelfConstants.LIST_CURRENTLY_READING;
            }

            var wantToRead = await _context.WantToRead
                .Where(e => e.BookIsse_Id == bookIssueId)
                .Where(e => e.User_Id == _userService.User.Id)
                .FirstOrDefaultAsync();
            if (wantToRead != null)
            {
                return BookshelfConstants.LIST_WANT_TO_READ;
            }

            var read = await _context.Read
                .Where(e => e.BookIssue_Id == bookIssueId)
                .Where(e => e.User_Id == _userService.User.Id)
                .FirstOrDefaultAsync();
            if (read != null)
            {
                return BookshelfConstants.LIST_READ;
            }

            return "";
        }

        public async Task<ServiceResponse> CreateBookAsync(CreateBook_RequestModel model)
        {
            var book = new Book
            {
                OriginalTitle = model.OriginalTitle,
                ReleaseDate = model.OriginalReleaseDate
            };

            var response = new ServiceResponse();
            try
            {
                _context.Book.Add(book);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                response.Errors.Add(e.Message, new string[] { e.StackTrace });
                return response;
            }

            model.AuthorIds.ToList().ForEach(e =>
            {
                _context.BookAuthor.Add(new BookAuthor { Book_Id = book.Id, Author_Id = e });
            });
            await _context.SaveChangesAsync();

            response.Succeeded = true;
            return response;
        }

        public async Task<List<BookSearchDto>> SearchBooks(string searchString)
        {
            var keywords = searchString.Split(" ");
            return _context.Book
                .AsEnumerable()
                .Where(e => keywords.All(keyword =>
                    e.OriginalTitle.ToLower().Contains(keyword.ToLower())
                ))
                .Take(10)
                .Select(e => new BookSearchDto
                {
                    Id = e.Id,
                    Title = e.OriginalTitle
                }).ToList();
        }

        public async Task<ServiceResponse> CreateBookIssueAsync(CreateBookIssue_RequestModel model)
        {
            string uploadedFileName = "";
            if (model.CoverPhoto != null)
            {
                uploadedFileName = await _azureStorageService.PrepareAndUploadFormFile(model.CoverPhoto);
            }


            var bookIssue = new BookIssue
            {
                Book_Id = model.BookId,
                Publisher_Id = model.PublisherId,
                Language_Id = 1,
                IsHardcover = false,
                ImageUrl = uploadedFileName,
                ISBN13 = model.ISBN,
                NumberOfPages = model.NumberOfPages,
                Tirage = model.Tirage,
                Summary = model.Summary,
                Title = model.Title
            };
            if (model.ReleaseDate != "undefined")
            {
                bookIssue.PublishedOn = DateTime.Parse(model.ReleaseDate);
            }

            var response = new ServiceResponse();
            try
            {
                _context.BookIssue.Add(bookIssue);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                response.Errors.Add(e.Message, new string[] { e.StackTrace });
                return response;
            }

            var created = await GetBookIssueDetails(bookIssue.Id);
            return created;
        }
    }
}
