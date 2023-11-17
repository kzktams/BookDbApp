using System;
using Microsoft.EntityFrameworkCore;
using R9IOPN_HFT_2023241.Models;

namespace R9IOPN_HFT_2023241.Repository
{
    public class BookDbContext : DbContext
    {
        public BookDbContext()

        {
            this.Database.EnsureCreated();
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<User> Users { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseInMemoryDatabase("book");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(t => t.Author)
                .WithMany(t => t.Books)
                .HasForeignKey(t => t.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Loan>()
                .HasOne(t => t.Book)
                .WithMany(t => t.Loans)
                .HasForeignKey(t => t.BookId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Loan>()
                .HasOne(t => t.User)
                .WithMany(t => t.Loans)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId = 1, Name = "John Smith", BirthDate = new DateTime(1965, 7, 21), Country = "USA" },
                new Author { AuthorId = 2, Name = "Emily Johnson", BirthDate = new DateTime(1974, 1, 15), Country = "UK" },
                new Author { AuthorId = 3, Name = "Alex Brown", BirthDate = new DateTime(1980, 11, 3), Country = "Canada" },
                new Author { AuthorId = 4, Name = "Isabella Davis", BirthDate = new DateTime(1992, 3, 8), Country = "Australia" },
                new Author { AuthorId = 5, Name = "Matthew Taylor", BirthDate = new DateTime(1955, 5, 17), Country = "Ireland" },
                new Author { AuthorId = 6, Name = "Sophia Wilson", BirthDate = new DateTime(1985, 8, 24), Country = "New Zealand" },
                new Author { AuthorId = 7, Name = "Ethan Anderson", BirthDate = new DateTime(1970, 12, 13), Country = "South Africa" },
                new Author { AuthorId = 8, Name = "Olivia Thomas", BirthDate = new DateTime(1991, 4, 2), Country = "USA" }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, Title = "Mountain Mysteries", PublicationYear = 2001, Genre = "Mystery", AuthorId = 1 },
                new Book { BookId = 2, Title = "The Future of Stars", PublicationYear = 1999, Genre = "Science Fiction", AuthorId = 1 },
                new Book { BookId = 3, Title = "Rivers of Time", PublicationYear = 2003, Genre = "Fantasy", AuthorId = 2 },
                new Book { BookId = 4, Title = "Silent Oceans", PublicationYear = 2005, Genre = "Adventure", AuthorId = 3 },
                new Book { BookId = 5, Title = "Echoes in the Dark", PublicationYear = 2000, Genre = "Horror", AuthorId = 3 },
                new Book { BookId = 6, Title = "Lost in the City", PublicationYear = 2004, Genre = "Drama", AuthorId = 3 },
                new Book { BookId = 7, Title = "Sky's Edge", PublicationYear = 1998, Genre = "Science Fiction", AuthorId = 4 },
                new Book { BookId = 8, Title = "Beyond the Horizon", PublicationYear = 2002, Genre = "Adventure", AuthorId = 5 },
                new Book { BookId = 9, Title = "Ancient Secrets", PublicationYear = 1997, Genre = "Mystery", AuthorId = 5 },
                new Book { BookId = 10, Title = "Sands of Time", PublicationYear = 2006, Genre = "Historical", AuthorId = 6 },
                new Book { BookId = 11, Title = "Through the Storm", PublicationYear = 1995, Genre = "Thriller", AuthorId = 7 },
                new Book { BookId = 12, Title = "The Last Quest", PublicationYear = 2007, Genre = "Fantasy", AuthorId = 7 },
                new Book { BookId = 13, Title = "Voices from the Past", PublicationYear = 2008, Genre = "Historical", AuthorId = 8 },
                new Book { BookId = 14, Title = "Shadows in the Snow", PublicationYear = 2009, Genre = "Thriller", AuthorId = 8 },
                new Book { BookId = 15, Title = "Dreams of the Desert", PublicationYear = 2010, Genre = "Romance", AuthorId = 8 }
            );

            modelBuilder.Entity<Loan>().HasData(
                new Loan { LoanId = 1, UserId = 1, BookId = 1, LoanDate = new DateTime(2022, 1, 10), ReturnDate = new DateTime(2022, 1, 20) },
                new Loan { LoanId = 2, UserId = 1, BookId = 2, LoanDate = new DateTime(2022, 1, 15), ReturnDate = new DateTime(2022, 1, 25) },
                new Loan { LoanId = 3, UserId = 3, BookId = 5, LoanDate = new DateTime(2022, 2, 1), ReturnDate = new DateTime(2022, 2, 11) },
                new Loan { LoanId = 4, UserId = 4, BookId = 8, LoanDate = new DateTime(2022, 2, 5), ReturnDate = new DateTime(2022, 2, 15) },
                new Loan { LoanId = 5, UserId = 5, BookId = 11, LoanDate = new DateTime(2022, 3, 10), ReturnDate = new DateTime(2022, 3, 20) },
                new Loan { LoanId = 6, UserId = 6, BookId = 6, LoanDate = new DateTime(2022, 3, 15), ReturnDate = new DateTime(2022, 3, 25) },
                new Loan { LoanId = 7, UserId = 7, BookId = 2, LoanDate = new DateTime(2022, 4, 1), ReturnDate = new DateTime(2022, 4, 11) },
                new Loan { LoanId = 8, UserId = 13, BookId = 8, LoanDate = new DateTime(2022, 4, 5), ReturnDate = new DateTime(2022, 4, 15) },
                new Loan { LoanId = 9, UserId = 15, BookId = 10, LoanDate = new DateTime(2022, 5, 10), ReturnDate = new DateTime(2022, 5, 20) },
                new Loan { LoanId = 10, UserId = 10, BookId = 10, LoanDate = new DateTime(2022, 5, 15), ReturnDate = new DateTime(2023, 5, 25) }
            );

            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Name = "Alice Johnson", Email = "alice.johnson@example.com", Phone = "+36262073389" },
                new User { UserId = 2, Name = "Bob Smith", Email = "bob.smith@example.com", Phone = "+36802834423" },
                new User { UserId = 3, Name = "Carol White", Email = "carol.white@example.com", Phone = "+36917195844" },
                new User { UserId = 4, Name = "David Brown", Email = "david.brown@example.com", Phone = "+36665244504" },
                new User { UserId = 5, Name = "Eve Black", Email = "eve.black@example.com", Phone = "+36365943802" },
                new User { UserId = 6, Name = "Frank Green", Email = "frank.green@example.com", Phone = "+36862082856" },
                new User { UserId = 7, Name = "Grace Hall", Email = "grace.hall@example.com", Phone = "+36222187435" },
                new User { UserId = 8, Name = "Henry Lee", Email = "henry.lee@example.com", Phone = "+36396070232" },
                new User { UserId = 9, Name = "Ivy Wilson", Email = "ivy.wilson@example.com", Phone = "+36685561811" },
                new User { UserId = 10, Name = "Jack Taylor", Email = "jack.taylor@example.com", Phone = "+36627744951" },
                new User { UserId = 11, Name = "Karen King", Email = "karen.king@example.com", Phone = "+36528051529" },
                new User { UserId = 12, Name = "Louis Adams", Email = "louis.adams@example.com", Phone = "+36998593158" },
                new User { UserId = 13, Name = "Mia Thompson", Email = "mia.thompson@example.com", Phone = "+36911933245" },
                new User { UserId = 14, Name = "Noah Parker", Email = "noah.parker@example.com", Phone = "+36863419178" },
                new User { UserId = 15, Name = "Olivia Turner", Email = "olivia.turner@example.com", Phone = "+36792267144" }
            );

        }

    }
}
