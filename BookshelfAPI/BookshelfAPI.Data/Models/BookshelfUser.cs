using Microsoft.AspNetCore.Identity;
using System;

namespace BookshelfAPI.Data.Models
{
    public class BookshelfUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime RegistrationTime { get; set; }
        public bool Active { get; set; } = true;
    }
}
