using Microsoft.AspNetCore.Identity;
using System;

namespace BookshelfAPI.Data.Models
{
    public class BookshelfUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime RegistrationTime { get; set; }
        public bool Active { get; set; } = true;
        public string ImageUrl { get; set; }

        public virtual Language Language { get; set; }
    }
}
