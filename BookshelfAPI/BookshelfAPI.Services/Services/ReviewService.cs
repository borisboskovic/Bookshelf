using BookshelfAPI.Data;
using BookshelfAPI.Data.Models;
using BookshelfAPI.Services.DTOs.Review;
using BookshelfAPI.Services.Interfaces;
using BookshelfAPI.Services.RequestModels.Review;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BookshelfAPI.Services.Services
{
    public class ReviewService : IReviewService
    {
        private readonly BookshelfDbContext _context;
        private readonly IUserService _userService;

        public ReviewService(BookshelfDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<ServiceResponse> RateBookIssue(RateBookIssue_RequestModel model)
        {
            var previousRating = await _context.Review
                .Where(e => e.BookIssue_Id == model.BookIssueId)
                .Where(e => e.User_Id == _userService.User.Id)
                .FirstOrDefaultAsync();

            var bookId = _context.BookIssue.FirstOrDefault(e => e.Id == model.BookIssueId).Book_Id;

            if (previousRating != null)
            {
                previousRating.Rating = model.Rating;
            }else
            {
                _context.Review.Add(new Review
                {
                    Book_Id = bookId,
                    PostedOn = DateTime.Now,
                    Rating = model.Rating,
                    BookIssue_Id = model.BookIssueId,
                    User_Id = _userService.User.Id
                });
            }
            _context.SaveChanges();

            return new ServiceResponse
            {
                Succeeded = true,
                Body = await GetBookRatings(bookId)
            };
        }

        public async Task<RatingsSummaryDto> GetBookRatings(int bookId)
        {
            var ratings = await _context.Review.Where(e => e.Book_Id == bookId)
                .GroupBy(e => new
                {
                    e.Book_Id
                })
                .Select(e => new
                {
                    Count = e.Count(),
                    Average = e.Average(e => e.Rating)
                }).FirstOrDefaultAsync();

            var userReview = await _context.Review
                .Where(e => e.Book_Id == bookId && e.User_Id == _userService.User.Id)
                .FirstOrDefaultAsync();

            return new RatingsSummaryDto
            {
                Average = ratings?.Average,
                Count = ratings?.Count ?? 0,
                YourRating = userReview?.Rating,
            };
        }
    }
}
