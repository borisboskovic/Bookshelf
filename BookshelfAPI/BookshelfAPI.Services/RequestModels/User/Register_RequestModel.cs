using Microsoft.AspNetCore.Http;
using System;

namespace BookshelfAPI.Services.RequestModels.User
{
    public class Register_RequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Password { get; set; }
        public IFormFile ProfilePhoto { get; set; }
    }
}
