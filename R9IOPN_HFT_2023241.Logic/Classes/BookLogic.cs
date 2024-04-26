using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using R9IOPN_HFT_2023241.Models;
using R9IOPN_HFT_2023241.Repository;

namespace R9IOPN_HFT_2023241.Logic
{
    public class BookLogic : IBookLogic
    {
        IRepository<Book> _bookRepository;
        IRepository<Loan> _loanRepository;
        IRepository<User> _userRepository;

        public BookLogic(IRepository<Book> repo, IRepository<Loan> loan, IRepository<User> userRepository)
        {
            this._bookRepository = repo;
            this._loanRepository = loan;
            this._userRepository = userRepository;
        }

        public void Create(Book item)
        {
            if (item.PublicationYear > DateTime.Now.Year)
            {
                throw new ArgumentException("Publication cannot be in the future");
            }
            //var validGenres = new List<string> { "Mystery", "Science Fiction", "Fantasy", "Adventure", "Horror", "Drama", "Thriller",
            //"mystery", "science fiction", "fantasy", "adventure", "horror", "drama", "thriller"
            //};
            //if (!validGenres.Contains(item.Genre))
            //{
            //    throw new ArgumentException("Genre doesn't exist");
            //}
            this._bookRepository.Create(item);
        }

        public void Delete(int id)
        {
            if (_loanRepository.ReadAll().Any(l => l.BookId == id && l.ReturnDate > DateTime.Now))
            {
                throw new InvalidOperationException("The book is currently under loan, cannot be deleted");
            }

            this._bookRepository.Delete(id);
        }

        public Book Read(int id)
        {
            return this._bookRepository.Read(id);
        }

        public IQueryable<Book> ReadAll()
        {
            return this._bookRepository.ReadAll();
        }

        public void Update(Book item)
        {
            if (item.PublicationYear > DateTime.Now.Year)
            {
                throw new ArgumentException("Publication cannot be in the future");
            }

            this._bookRepository.Update(item);
        }

        //nonCRUD

        //Listing the  boooks of a certain author by authorId
        public IEnumerable<BookDetail> GetBooksByAuthor(int authorId)
        {
            return _bookRepository.ReadAll()
                                 .Where(b => b.AuthorId == authorId)
                                 .Select(b => new BookDetail
                                 {
                                     BookId = b.BookId,
                                     Title = b.Title,
                                     PublicationYear = b.PublicationYear,
                                     Genre = b.Genre
                                 })
                                 ;
        }

        // Listing the books that were loaned the most times, along with their titles and loan counts
        public IEnumerable<BookLoanCount> GetMostLoanedBooks()
        {
            var loanCounts = _loanRepository.ReadAll()
                .GroupBy(loan => loan.BookId)
                .Select(group => new
                {
                    BookId = group.Key,
                    LoanCount = group.Count()
                })
                .OrderByDescending(x => x.LoanCount)
                .Take(5)
                .ToList();

            // Lekérdezzük az összes szükséges könyvet egyszerre
            var bookIds = loanCounts.Select(l => l.BookId).ToList();
            var books = _bookRepository.ReadAll()
                .Where(book => bookIds.Contains(book.BookId))
                .ToDictionary(book => book.BookId, book => book.Title);

            // Csak a létező könyvekkel térünk vissza
            var mostLoanedBooks = loanCounts.Where(l => books.ContainsKey(l.BookId))
                .Select(loanCount => new BookLoanCount
                {
                    BookId = loanCount.BookId,
                    Title = books[loanCount.BookId],
                    LoanCount = loanCount.LoanCount
                });

            return mostLoanedBooks;
        }


        //Listing the books by the given genre
        public IEnumerable<BookDetail> GetBooksByGenre(string genre)
        {
            return _bookRepository.ReadAll()
                                 .Where(b => b.Genre == genre)
                                 .Select(b => new BookDetail
                                 {
                                     BookId = b.BookId,
                                     Title = b.Title,
                                     PublicationYear = b.PublicationYear,
                                     Genre = b.Genre
                                 })
                                 ;
        }

        //Listing the books that a certain person loaned out
        public IEnumerable<UserLoanDetail> GetBooksLoanedByUser(int userId)
        {
            var userLoans = _loanRepository.ReadAll()
                                           .Where(l => l.UserId == userId)
                                           .Select(l => new UserLoanDetail
                                           {
                                               BookId = l.Book.BookId,
                                               BookTitle = l.Book.Title,
                                               UserName = l.User.Name,
                                               LoanDate = l.LoanDate,
                                               ReturnDate = l.ReturnDate
                                           })
                                           ;

            return userLoans;
            
        }

        //listing books between dates
        public IEnumerable<BookDetail> GetBooksLoanedBetweenDates(DateTime startDate, DateTime endDate)
        {
            return _loanRepository.ReadAll()
                                 .Where(l => l.LoanDate >= startDate && l.ReturnDate <= endDate)
                                 .Select(l => new BookDetail
                                 {
                                     BookId = l.Book.BookId,
                                     Title = l.Book.Title,
                                     PublicationYear = l.Book.PublicationYear,
                                     Genre = l.Book.Genre
                                 })
                                 ;
        }

        

        

        
    }

    
}
