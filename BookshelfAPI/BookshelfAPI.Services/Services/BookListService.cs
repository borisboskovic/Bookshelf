using BookshelfAPI.Data;
using BookshelfAPI.Services.DTOs.BookList;
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
                    e.Title,
                    e.ImageUrl,
                    e.AddedOn,
                    e.PagesRead,
                    e.NumberOfPages
                })
                .Select
                (
                    currentlyReading => new CurrentlyReadingItemDto
                    {
                        BookIssueId = currentlyReading.Key.BookIssue_Id,
                        AddedOn = currentlyReading.Key.AddedOn,
                        ImageUrl = $"{_configuration["Azure:BlobStorageUrl"]}/{currentlyReading.Key.ImageUrl}",
                        PagesRead = currentlyReading.Key.PagesRead ?? 0,
                        Title = currentlyReading.Key.Title,
                        TotalPages = currentlyReading.Key.NumberOfPages,
                        Authors = currentlyReading.Select(e => new AuthorBasicInfoDto
                        {
                            AuthorId = e.Author.Id,
                            Name = $"{e.Author.Name} {e.Author.Surname}"
                        }).ToList()
                    })
                .ToList();

            return list;
        }

        public async Task<ServiceResponse> GetDefaultLists()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse> UpdateReadingProgress(ReadingProgress_RequestModel model)
        {
            var item = _context.CurrentlyReading
                .Where(e => e.User_Id == _userService.User.Id)
                .Where(e => e.BookIssue_Id == model.BookIssueId)
                .FirstOrDefault();

            var response = new ServiceResponse();
            if (item == null)
            {
                response.Errors.Add("Error Updating", new string[] { "Book issue not found" });
                return response;
            }

            item.PagesRead = model.PagesRead;
            _context.SaveChanges();

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
                response.Errors.Add("Update Failed", new string[] { "Book issue not found"});
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
    }
}
