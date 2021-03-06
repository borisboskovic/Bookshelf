// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookshelfAPI.Data.Models
{
    [Index(nameof(Publisher_Id), Name = "BYPUBLISHER_FK")]
    public partial class BookEdition
    {
        public BookEdition()
        {
            BookIssueInEdition = new HashSet<BookIssueInEdition>();
        }

        [Key]
        public int Id { get; set; }
        public int Publisher_Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(4000)]
        public string Description { get; set; }
        [StringLength(200)]
        public string ImageUrl { get; set; }

        [ForeignKey(nameof(Publisher_Id))]
        [InverseProperty("BookEdition")]
        public virtual Publisher Publisher { get; set; }
        [InverseProperty("Edition")]
        public virtual ICollection<BookIssueInEdition> BookIssueInEdition { get; set; }
    }
}