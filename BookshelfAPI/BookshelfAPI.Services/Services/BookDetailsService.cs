﻿using BookshelfAPI.Data;
using BookshelfAPI.Services.DTOs.BookDetails;
using BookshelfAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BookshelfAPI.Services.Services
{
    public class BookDetailsService : IBookDetailsService
    {
        private readonly BookshelfDbContext _context;
        private readonly IUserService _userService;

        public BookDetailsService(BookshelfDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
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
                        ImageUrl = e.Publisher.ImageUrl,
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

            bookIssue.ReadingStatus = await GetBookReadingStatus(bookIssueId);

            if (bookIssue == null)
            {
                response.Errors.Add("Error", new string[] { "Book issue not found" });
            }

            response.Succeeded = true;
            response.Body = bookIssue;

            return response;
        }

        public async Task<ReadingStatusDto> GetBookReadingStatus(int bookIssueId)
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
    }
}
