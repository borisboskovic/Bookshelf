using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshelfAPI.Services.RequestModels.BookDetails
{
    public class CreateBookIssue_RequestModel
    {
        public int BookId { get; set; }
        public int PublisherId { get; set; }
        public string Title { get; set; }
        public string ReleaseDate { get; set; }
        public int NumberOfPages { get; set; }
        public int Tirage { get; set; }
        public string IsHardcover { get; set; }
        public string Summary { get; set; }
        public IFormFile CoverPhoto { get; set; }
        public string ISBN { get; set; }
    }
}
