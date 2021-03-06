// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookshelfAPI.Data.Models
{
    [Index(nameof(User_Id), Name = "REACTEDONREVIEWBY_FK")]
    [Index(nameof(Book_Id), nameof(BookIssue_Id), nameof(Review_User_Id), Name = "REACTEDONREVIEW_FK")]
    public partial class ReviewReaction
    {
        [Key]
        public string User_Id { get; set; }
        [Key]
        public int Book_Id { get; set; }
        [Key]
        public int BookIssue_Id { get; set; }
        [Key]
        public string Review_User_Id { get; set; }
        public bool Like { get; set; }

        [ForeignKey("Book_Id,BookIssue_Id,Review_User_Id")]
        [InverseProperty("ReviewReaction")]
        public virtual Review Review { get; set; }
    }
}