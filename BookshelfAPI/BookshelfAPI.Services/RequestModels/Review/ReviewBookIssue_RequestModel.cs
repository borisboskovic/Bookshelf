using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshelfAPI.Services.RequestModels.Review
{
    public class ReviewBookIssue_RequestModel
    {
        public int BookIssueId { get; set; }
        public string Content { get; set; }
    }
}
