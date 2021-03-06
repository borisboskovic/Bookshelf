// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookshelfAPI.Data.Models
{
    [Index(nameof(User_Id), Name = "BOOKSHELFBY_FK")]
    public partial class UserBookShelf
    {
        public UserBookShelf()
        {
            BookOnBookshelf = new HashSet<BookOnBookshelf>();
        }

        [Key]
        public int Id { get; set; }
        [Key]
        public string User_Id { get; set; }
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        [StringLength(4000)]
        public string Descripton { get; set; }
        public bool Public { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }

        [InverseProperty("UserBookShelf")]
        public virtual ICollection<BookOnBookshelf> BookOnBookshelf { get; set; }
    }
}