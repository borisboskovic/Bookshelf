using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshelfAPI.Services.DTOs.Review
{
    public class ReviewCommentDto
    {
        public string CommentUser_Id { get; set; }
        public string Content { get; set; }
        public DateTime? PostedOn { get; set; }
    }
}
