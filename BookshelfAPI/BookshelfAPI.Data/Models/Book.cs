using System;
using System.Collections.Generic;

#nullable disable

namespace BookshelfAPI.Data.Models
{
    public partial class Book
    {
        public int Id { get; set; }
        public int AutId { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public string Isbn13 { get; set; }

        public virtual Author Aut { get; set; }
    }
}
