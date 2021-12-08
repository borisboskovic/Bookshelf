using System;

namespace BookshelfAPI.Services.DTOs.Review
{
    public class ReviewCommentDto
    {
        public int CommentId { get; set; }
        public string CommentUser_Id { get; set; }
        public string AuthorName { get; set; }
        public DateTime? PostedOn { get; set; }
        public string ImageUrl { get; set; }
        public string Content { get; set; }
    }
}
