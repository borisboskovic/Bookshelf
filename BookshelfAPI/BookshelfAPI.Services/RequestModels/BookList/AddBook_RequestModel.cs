using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshelfAPI.Services.RequestModels.BookList
{
    public class AddBook_RequestModel
    {
        [JsonProperty("id")]
        public int BookIssueId { get; set; }
        public string PreviousList { get; set; }
        public string NextList { get; set; }
    }
}
