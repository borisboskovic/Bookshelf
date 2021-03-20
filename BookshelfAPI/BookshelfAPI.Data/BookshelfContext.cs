using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BookshelfAPI.Data.Models;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace BookshelfAPI.Data
{
    public partial class BookshelfContext : DbContext
    {
        public IConfiguration Configuration { get; }

        public BookshelfContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public BookshelfContext(DbContextOptions<BookshelfContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;Database=Bookshelf;Trusted_Connection=True;");
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("Bookshelf"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("Author");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Book");

                entity.HasIndex(e => e.AutId, "Writen By_FK");

                entity.Property(e => e.AutId).HasColumnName("Aut_Id");

                entity.Property(e => e.Isbn)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ISBN");

                entity.Property(e => e.Isbn13)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("ISBN13");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.Aut)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AutId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BOOK_WRITEN BY_AUTHOR");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
