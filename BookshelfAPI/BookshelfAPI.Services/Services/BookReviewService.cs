using BookshelfAPI.Data;
using BookshelfAPI.Data.Models;
using BookshelfAPI.Services.DTOs.Review;
using BookshelfAPI.Services.Interfaces;
using BookshelfAPI.Services.RequestModels.Review;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BookshelfAPI.Services.Services
{
    public class BookReviewService : IBookReviewService
    {
        private readonly BookshelfDbContext _context;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public BookReviewService(BookshelfDbContext context, IUserService userService, IConfiguration configuration)
        {
            _context = context;
            _userService = userService;
            _configuration = configuration;
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
            }
            else
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

        public async Task<ServiceResponse> AddBookIssueReview(ReviewBookIssue_RequestModel model)
        {
            var review = await _context.Review
                .Where(e => e.BookIssue_Id == model.BookIssueId)
                .Where(e => e.User_Id == _userService.User.Id)
                .FirstOrDefaultAsync();

            var bookId = _context.BookIssue.FirstOrDefault(e => e.Id == model.BookIssueId).Book_Id;

            if (review != null)
            {
                review.ReviewText = model.Content;
                _context.Review.Update(review);
            }
            else
            {
                review = new Review
                {
                    BookIssue_Id = model.BookIssueId,
                    Book_Id = bookId,
                    PostedOn = DateTime.Now,
                    ReviewText = model.Content,
                    User_Id = _userService.User.Id,
                };
                _context.Review.Add(review);
            }
            await _context.SaveChangesAsync();

            return new ServiceResponse
            {
                Succeeded = true,
                Body = new BookReviewItemDto
                {
                    AuthorId = review.User_Id,
                    Content = review.ReviewText,
                    PostedOn = review.PostedOn,
                    AuthorImage = $"{_configuration["Azure:BlobStorageUrl"]}/{_userService.User.ImageUrl}",
                    AuthorName = $"{_userService.User.FirstName} {_userService.User.LastName}",
                    LikeCount = 0,
                    DislikeCount = 0
                }
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

        public async Task<ServiceResponse> GetReviews(int bookIssueId)
        {
            var allReviews = _context.Review
                .Where(e => e.BookIssue_Id == bookIssueId)
                .Where(e => e.ReviewText != null)
                .Join
                (
                    _context.Users,
                    review => review.User_Id,
                    user => user.Id,
                    (review, user) => new
                    {
                        review.ReviewText,
                        review.PostedOn,
                        review.BookIssue_Id,
                        User_Id = user.Id,
                        user.FirstName,
                        user.LastName,
                        user.ImageUrl
                    }
                )
                .GroupJoin
                (
                    _context.ReviewReaction,
                    review => new { review.User_Id, review.BookIssue_Id },
                    reaction => new { reaction.Review.User_Id, reaction.BookIssue_Id },
                    (review, reaction) => new
                    {
                        review.User_Id,
                        review.FirstName,
                        review.LastName,
                        review.ImageUrl,
                        review.ReviewText,
                        review.PostedOn,
                        review.BookIssue_Id,
                        Reaction = reaction
                    }
                )
                .SelectMany
                (
                    review => review.Reaction.DefaultIfEmpty(),
                    (review, reaction) => new
                    {
                        review.ImageUrl,
                        review.BookIssue_Id,
                        review.FirstName,
                        review.LastName,
                        review.PostedOn,
                        review.ReviewText,
                        review.User_Id,
                        Reaction = reaction
                    }
                )
                .AsEnumerable()
                .GroupBy(review => new
                {
                    review.ImageUrl,
                    review.BookIssue_Id,
                    review.FirstName,
                    review.LastName,
                    review.PostedOn,
                    review.ReviewText,
                    review.User_Id,
                })
                .Select(e => new
                {
                    e.Key.BookIssue_Id,
                    e.Key.User_Id,
                    e.Key.ImageUrl,
                    e.Key.FirstName,
                    e.Key.LastName,
                    e.Key.ReviewText,
                    e.Key.PostedOn,
                    LikeCount = e.Where(e => e.Reaction?.Like == true).Count(),
                    DislikeCount = e.Where(e => e.Reaction?.Like == false).Count(),
                })
                .GroupJoin
                (
                    _context.ReviewComment,
                    review => new { review.User_Id, review.BookIssue_Id },
                    comment => new { User_Id = comment.Review_User_Id, comment.BookIssue_Id },
                    (review, comment) => new
                    {
                        review.BookIssue_Id,
                        review.DislikeCount,
                        review.FirstName,
                        review.LastName,
                        review.ImageUrl,
                        review.LikeCount,
                        review.PostedOn,
                        review.ReviewText,
                        review.User_Id,
                        Comment = comment
                    }
                )
                .SelectMany
                (
                    review => review.Comment
                    .Join(_context.Users, comment => comment.CommentAuthor_Id, user => user.Id,
                    (comment, user) => new
                    {
                        comment.CommentAuthor_Id,
                        comment.PostedOn,
                        comment.Content,
                        comment.Id,
                        user.FirstName,
                        user.LastName,
                        user.ImageUrl
                    }).DefaultIfEmpty(),
                    (review, comment) => new
                    {
                        review.BookIssue_Id,
                        review.DislikeCount,
                        review.FirstName,
                        review.LastName,
                        review.ImageUrl,
                        review.LikeCount,
                        review.PostedOn,
                        review.ReviewText,
                        review.User_Id,
                        Comment = comment
                    }
                )
                .AsEnumerable()
                .GroupBy(review => new
                {
                    review.BookIssue_Id,
                    review.DislikeCount,
                    review.FirstName,
                    review.LastName,
                    review.ImageUrl,
                    review.LikeCount,
                    review.PostedOn,
                    review.ReviewText,
                    review.User_Id,
                })
                .Select(e => new BookReviewItemDto
                {
                    AuthorId = e.Key.User_Id,
                    AuthorImage = $"{_configuration["Azure:BlobStorageUrl"]}/{e.Key.ImageUrl}",
                    AuthorName = $"{e.Key.FirstName} {e.Key.LastName}",
                    Content = e.Key.ReviewText,
                    LikeCount = e.Key.LikeCount,
                    DislikeCount = e.Key.DislikeCount,
                    PostedOn = e.Key.PostedOn,
                    Comments = e.Where(e => e.Comment != null).Select(e => new ReviewCommentDto
                    {
                        CommentUser_Id = e.Comment?.CommentAuthor_Id,
                        Content = e.Comment?.Content,
                        PostedOn = e.Comment?.PostedOn,
                        AuthorName = $"{e.Comment.FirstName} {e.Comment.LastName}",
                        CommentId = e.Comment.Id,
                        ImageUrl = $"{_configuration["Azure:BlobStorageUrl"]}/{e.Comment.ImageUrl}"
                    }).ToList()
                })
                .ToList();

            var myReview = allReviews.Where(e => e.AuthorId == _userService.User.Id).FirstOrDefault();
            var otherReviews = allReviews.Where(e => e.AuthorId != _userService.User.Id).ToList();

            return new ServiceResponse
            {
                Succeeded = true,
                Body = new BookReviewsDto { MyReview = myReview, OtherReviews = otherReviews }
            };
        }
    }
}
