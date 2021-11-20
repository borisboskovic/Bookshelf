using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshelfAPI.Services.RequestModels.Review
{
    public class RateBookIssue_RequestModel
    {
        [Required]
        public int BookIssueId { get; set; }
        [Required]
        public int Rating { get; set; }
    }
}
