﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookshelfAPI.Data.Models
{
    [Index(nameof(User_Id), Name = "POSTEDBYUSER_FK")]
    [Index(nameof(Book_Id), nameof(BookIssue_Id), Name = "REVIEWOF_FK")]
    public partial class Review
    {
        public Review()
        {
            ReviewComment = new HashSet<ReviewComment>();
            ReviewReaction = new HashSet<ReviewReaction>();
        }

        [Key]
        public int Book_Id { get; set; }
        [Key]
        public int BookIssue_Id { get; set; }
        [Key]
        public string User_Id { get; set; }
        public int? Rating { get; set; }
        [Column("Review")]
        [StringLength(4000)]
        public string ReviewText { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime PostedOn { get; set; }

        [ForeignKey("Book_Id,BookIssue_Id")]
        [InverseProperty(nameof(BookIssue.Review))]
        public virtual BookIssue Book { get; set; }
        [InverseProperty("Review")]
        public virtual ICollection<ReviewComment> ReviewComment { get; set; }
        [InverseProperty("Review")]
        public virtual ICollection<ReviewReaction> ReviewReaction { get; set; }
    }
}