using System;
using System.ComponentModel.DataAnnotations;

namespace BookshelfAPI.Services.DTOs
{
    public class UserRegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
