// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookshelfAPI.Data.Models
{
    public partial class Publisher
    {
        public Publisher()
        {
            BookEdition = new HashSet<BookEdition>();
            BookIssue = new HashSet<BookIssue>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(512)]
        public string Name { get; set; }
        [StringLength(2000)]
        public string ImageUrl { get; set; }
        [StringLength(200)]
        public string Address { get; set; }

        [InverseProperty("Publisher")]
        public virtual ICollection<BookEdition> BookEdition { get; set; }
        [InverseProperty("Publisher")]
        public virtual ICollection<BookIssue> BookIssue { get; set; }
    }
}