using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshelfAPI.Services.RequestModels.BookList
{
    public class RemoveBook_RequestModel
    {
        [JsonProperty("id")]
        public int BookIssueId { get; set; }
        public string List { get; set; }
    }
}
