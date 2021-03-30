using System;

namespace BookshelfAPI.Data.Models
{
    public class CredentialsHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime ChangedOn { get; set; }

        public virtual BookshelfUser User { get; set; }
    }
}
