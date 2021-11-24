using BookshelfAPI.Data;
using BookshelfAPI.Services.DTOs.BookList;
using BookshelfAPI.Services.DTOs.Review;
using BookshelfAPI.Services.Helpers;
using BookshelfAPI.Services.Interfaces;
using BookshelfAPI.Services.RequestModels.BookList;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshelfAPI.Services.Services
{
    public class BookListService : IBookListService
    {
        private readonly BookshelfDbContext _context;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public BookListService(BookshelfDbContext context, IUserService userService, IConfiguration configuration)
        {
            _context = context;
            _userService = userService;
            _configuration = configuration;
        }

        public List<CommonBookItemDto> GetWantToRead()
        {
            var list = _context.WantToRead
                .Include(e => e.Book)
                .Where(e => e.User_Id == _userService.User.Id)
                .Join
                (
                    _context.BookAuthor,
                    wantToRead => wantToRead.Book_Id,
                    bookAuthor => bookAuthor.Book_Id,
                    (wantToRead, bookAuthor) => new
                    {
                        wantToRead.Book_Id,
                        wantToRead.BookIsse_Id,
                        wantToRead.Book.Title,
                        wantToRead.Book.ImageUrl,
                        wantToRead.AddedOn,
                        Author = bookAuthor.Author
                    }
                )
                .AsEnumerable()
                .GroupBy(e => new
                {
                    e.BookIsse_Id,
                    e.Book_Id,
                    e.Title,
                    e.ImageUrl,
                    e.AddedOn,
                })
                .Select(wantToRead => new
                {
                    wantToRead.Key.Book_Id,
                    wantToRead.Key.BookIsse_Id,
                    wantToRead.Key.AddedOn,
                    wantToRead.Key.Title,
                    ImageUrl = $"{_configuration["Azure:BlobStorageUrl"]}/{wantToRead.Key.ImageUrl}",
                    Authors = wantToRead.Select(e => new AuthorBasicInfoDto
                    {
                        Id = e.Author.Id,
                        Name = $"{e.Author.Name} {e.Author.Surname}"
                    }).ToList()
                })
                .GroupJoin
                (
                    _context.Review.DefaultIfEmpty(),
                    wantToRead => wantToRead.Book_Id,
                    review => review.Book_Id,
                    (wantToRead, reviews) => new CommonBookItemDto
                    {
                        BookIssueId = wantToRead.BookIsse_Id,
                        Title = wantToRead.Title,
                        AddedOn = wantToRead.AddedOn,
                        ImageUrl = wantToRead.ImageUrl,
                        Authors = wantToRead.Authors,
                        Ratings = new RatingsSummaryDto
                        {
                            Average = reviews.Average(e => e.Rating),
                            Count = reviews.Count(),
                            YourRating = reviews.Where(e => e.User_Id == _userService.User.Id).Select(e => e.Rating).FirstOrDefault()
                        }
                    })
                .ToList();

            return list;
        }

        public List<CurrentlyReadingItemDto> GetCurrentlyReading()
        {
            var list = _context.CurrentlyReading
                .Include(e => e.Book)
                .Where(e => e.User_Id == _userService.User.Id)
                .Join
                (
                    _context.BookAuthor,
                    currentlyReading => currentlyReading.Book_Id,
                    bookAuthor => bookAuthor.Book_Id,
                    (currentlyReading, bookAuthor) => new
                    {
                        currentlyReading.BookIssue_Id,
                        currentlyReading.Book_Id,
                        currentlyReading.Book.Title,
                        currentlyReading.Book.ImageUrl,
                        currentlyReading.AddedOn,
                        currentlyReading.PagesRead,
                        currentlyReading.Book.NumberOfPages,
                        Author = bookAuthor.Author,
                    }
                )
                .AsEnumerable()
                .GroupBy(e => new
                {
                    e.BookIssue_Id,
                    e.Book_Id,
                    e.Title,
                    e.ImageUrl,
                    e.AddedOn,
                    e.PagesRead,
                    e.NumberOfPages,
                })
                .Select
                (
                    currentlyReading => new
                    {
                        currentlyReading.Key.Book_Id,
                        currentlyReading.Key.BookIssue_Id,
                        currentlyReading.Key.AddedOn,
                        currentlyReading.Key.Title,
                        currentlyReading.Key.NumberOfPages,
                        ImageUrl = $"{_configuration["Azure:BlobStorageUrl"]}/{currentlyReading.Key.ImageUrl}",
                        PagesRead = currentlyReading.Key.PagesRead ?? 0,
                        Authors = currentlyReading.Select(e => new AuthorBasicInfoDto
                        {
                            Id = e.Author.Id,
                            Name = $"{e.Author.Name} {e.Author.Surname}"
                        }).ToList()
                    })
                .GroupJoin
                (
                    _context.Review.DefaultIfEmpty(),
                    currentlyReading => currentlyReading.Book_Id,
                    review => review.Book_Id,
                    (currentlyReading, reviews) => new CurrentlyReadingItemDto
                    {
                        BookIssueId = currentlyReading.BookIssue_Id,
                        Title = currentlyReading.Title,
                        AddedOn = currentlyReading.AddedOn,
                        ImageUrl = currentlyReading.ImageUrl,
                        Authors = currentlyReading.Authors,
                        PagesRead = currentlyReading.PagesRead,
                        TotalPages = currentlyReading.NumberOfPages,
                        Ratings = new RatingsSummaryDto
                        {
                            Average = reviews.Average(e => e.Rating),
                            Count = reviews.Count(),
                            YourRating = reviews.Where(e => e.User_Id == _userService.User.Id).Select(e => e.Rating).FirstOrDefault()
                        }
                    }
                )
                .ToList();

            return list;
        }

        public List<CommonBookItemDto> GetRead()
        {
            var list = _context.Read
                .Include(e => e.Book)
                .Where(e => e.User_Id == _userService.User.Id)
                .Join
                (
                    _context.BookAuthor,
                    read => read.Book_Id,
                    bookAuthor => bookAuthor.Book_Id,
                    (read, bookAuthor) => new
                    {
                        read.BookIssue_Id,
                        read.Book_Id,
                        read.Book.Title,
                        read.Book.ImageUrl,
                        read.AddedOn,
                        Author = bookAuthor.Author
                    }
                )
                .AsEnumerable()
                .GroupBy(e => new
                {
                    e.Book_Id,
                    e.BookIssue_Id,
                    e.Title,
                    e.ImageUrl,
                    e.AddedOn
                })
                .Select(read => new
                {
                    read.Key.Book_Id,
                    read.Key.BookIssue_Id,
                    read.Key.Title,
                    ImageUrl = $"{_configuration["Azure:BlobStorageUrl"]}/{read.Key.ImageUrl}",
                    read.Key.AddedOn,
                    Authors = read.Select(e => new AuthorBasicInfoDto
                    {
                        Id = e.Author.Id,
                        Name = $"{e.Author.Name} {e.Author.Surname}"
                    }).ToList()
                })
                .GroupJoin
                (
                    _context.Review.DefaultIfEmpty(),
                    read => read.Book_Id,
                    review => review.Book_Id,
                    (read, reviews) => new CommonBookItemDto
                    {
                        BookIssueId = read.BookIssue_Id,
                        Title = read.Title,
                        AddedOn = read.AddedOn,
                        ImageUrl = read.ImageUrl,
                        Authors = read.Authors,
                        Ratings = new RatingsSummaryDto
                        {
                            Average = reviews.Average(e => e.Rating),
                            Count = reviews.Count(),
                            YourRating = reviews.Where(e => e.User_Id == _userService.User.Id).Select(e => e.Rating).FirstOrDefault()
                        },
                    }
                )
                .ToList();

            return list;
        }

        public async Task<ServiceResponse> GetDefaultLists()
        {
            var wantToRead = GetWantToRead();

            var currentlyReading = GetCurrentlyReading().Select(e => new CommonBookItemDto
            {
                AddedOn = e.AddedOn,
                Authors = e.Authors,
                BookIssueId = e.BookIssueId,
                ImageUrl = e.ImageUrl,
                Ratings = e.Ratings,
                Title = e.Title
            }).ToList();

            var read = GetRead();

            return new ServiceResponse
            {
                Succeeded = true,
                Body = new BookListsDto
                {
                    CurrentlyReading = currentlyReading,
                    Read = read,
                    WantToRead = wantToRead
                }
            };
        }

        public async Task<ServiceResponse> UpdateReadingProgress(ReadingProgress_RequestModel model)
        {
            var item = await _context.CurrentlyReading
                .Where(e => e.User_Id == _userService.User.Id)
                .Where(e => e.BookIssue_Id == model.BookIssueId)
                .FirstOrDefaultAsync();

            var response = new ServiceResponse();
            if (item == null)
            {
                response.Errors.Add("Error Updating", new string[] { "Book issue not found" });
                return response;
            }

            item.PagesRead = model.PagesRead;
            await _context.SaveChangesAsync();

            response.Succeeded = true;
            response.Body = new CurrentlyReadingItemUpdatedDto
            {
                BookIssueId = item.BookIssue_Id,
                PagesRead = item.PagesRead ?? 0
            };
            return response;
        }

        public async Task<ServiceResponse> FinishBokReading(int bookIssueId)
        {
            var book = await _context.CurrentlyReading
                .Where(e => e.User_Id == _userService.User.Id)
                .Where(e => e.BookIssue_Id == bookIssueId)
                .FirstOrDefaultAsync();

            var response = new ServiceResponse();
            if (book == null)
            {
                response.Errors.Add("Update Failed", new string[] { "Book issue not found" });
                return response;
            }

            _context.Read.Add(new Data.Models.Read
            {
                FinishedOn = DateTime.Now,
                AddedOn = DateTime.Now,
                BookIssue_Id = book.BookIssue_Id,
                Book_Id = book.Book_Id,
                User_Id = _userService.User.Id,
                StartedOn = book.AddedOn
            });
            _context.CurrentlyReading.Remove(book);
            _context.SaveChanges();

            response.Succeeded = true;
            return response;
        }

        public async Task RemoveBookFromList(RemoveBook_RequestModel model)
        {
            switch (model.List)
            {
                case BookshelfConstants.LIST_CURRENTLY_READING:
                    var currentlyReading = await _context.CurrentlyReading
                        .Where(e => e.BookIssue_Id == model.BookIssueId)
                        .Where(e => e.User_Id == _userService.User.Id)
                        .FirstOrDefaultAsync();
                    _context.CurrentlyReading.Remove(currentlyReading);
                    break;

                case BookshelfConstants.LIST_WANT_TO_READ:
                    var wantToRead = await _context.WantToRead
                        .Where(e => e.BookIsse_Id == model.BookIssueId)
                        .Where(e => e.User_Id == _userService.User.Id)
                        .FirstOrDefaultAsync();
                    _context.WantToRead.Remove(wantToRead);
                    break;

                case BookshelfConstants.LIST_READ:
                    var read = await _context.Read
                        .Where(e => e.BookIssue_Id == model.BookIssueId)
                        .Where(e => e.User_Id == _userService.User.Id)
                        .FirstOrDefaultAsync();
                    _context.Read.Remove(read);
                    break;
            }
            await _context.SaveChangesAsync();
        }

        public async Task AddBookToList(AddBook_RequestModel model)
        {
            if (model.PreviousList != null)
            {
                await RemoveBookFromList(new RemoveBook_RequestModel
                {
                    BookIssueId = model.BookIssueId,
                    List = model.PreviousList
                });
            }

            var bookIssue = _context.BookIssue
                .Where(e => e.Id == model.BookIssueId)
                .FirstOrDefault();

            switch (model.NextList)
            {
                case BookshelfConstants.LIST_CURRENTLY_READING:
                    _context.CurrentlyReading.Add(new Data.Models.CurrentlyReading
                    {
                        AddedOn = DateTime.Now,
                        BookIssue_Id = model.BookIssueId,
                        Book_Id = bookIssue.Book_Id,
                        User_Id = _userService.User.Id
                    });
                    break;

                case BookshelfConstants.LIST_WANT_TO_READ:
                    _context.WantToRead.Add(new Data.Models.WantToRead
                    {
                        AddedOn = DateTime.Now,
                        BookIsse_Id = model.BookIssueId,
                        Book_Id = bookIssue.Book_Id,
                        User_Id = _userService.User.Id,
                    });
                    break;

                case BookshelfConstants.LIST_READ:
                    _context.Read.Add(new Data.Models.Read
                    {
                        AddedOn = DateTime.Now,
                        BookIssue_Id = model.BookIssueId,
                        Book_Id = bookIssue.Book_Id,
                        User_Id = _userService.User.Id,
                        FinishedOn = DateTime.Now,
                        StartedOn = null,
                    });
                    break;
            }

            await _context.SaveChangesAsync();
        }
    }
}
