using Microsoft.AspNetCore.Http;
using System;

namespace BookshelfAPI.Services.RequestModels.AuthorDetails
{
    public class CreateAuthor_RequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; } = null;
        public string DateOfDeath { get; set; } = null;
        public IFormFile AuthorPhoto { get; set; }
        public string Biography { get; set; }
        public string PlaceOfBirth { get; set; }
    }
}
