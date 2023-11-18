using System;
using System.Collections.Generic;
using System.Linq;
using R9IOPN_HFT_2023241.Models;
using R9IOPN_HFT_2023241.Repository;

namespace R9IOPN_HFT_2023241.Logic
{
    public class BookLogic
    {
        IRepository<Book> _bookRepository;
        IRepository<Loan> _loanRepository;

        public BookLogic(IRepository<Book> repo, IRepository<Loan> loan)
        {
            this._bookRepository = repo;
            this._loanRepository = loan;
        }

        public void Create(Book item)
        {
            this._bookRepository.Create(item);
        }

        public void Delete(int id)
        {
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
            this._bookRepository.Update(item);
        }

        //nonCRUD

        //Listing the  boooks of a certain author by authorId
        public IEnumerable<Book> GetBooksByAuthor(int authorId)
        {
            return _bookRepository.ReadAll().Where(b => b.AuthorId == authorId);
        }

        //listing the books that were loaned the most times
        public IEnumerable<Book> GetMostLoanedBooks()
        {
            // Loan namount
            var loanCounts = _bookRepository.ReadAll()
                                            .GroupBy(loan => loan.BookId)
                                            .Select(group => new
                                            {
                                                BookId = group.Key,
                                                LoanCount = group.Count()
                                            })
                                            .OrderByDescending(x => x.LoanCount)
                                            .Take(10)
                                            .ToList();

            // Books that were loaned the most amount of times
            var mostLoanedBooks = new List<Book>();
            foreach (var loanCount in loanCounts)
            {
                var book = _bookRepository.ReadAll().FirstOrDefault(b => b.BookId == loanCount.BookId);
                if (book != null)
                {
                    mostLoanedBooks.Add(book);
                }
            }

            return mostLoanedBooks;
        }

        //Listing the books by the given genre
        public IEnumerable<Book> GetBooksByGenre(string genre)
        {
            return _bookRepository.ReadAll().Where(b => b.Genre == genre);
        }

        //Listing the books that a certain person loaned out
        public IEnumerable<Book> GetBooksLoanedByUser(int userId)
        {
            return _loanRepository.ReadAll()
                    .Where(l => l.UserId == userId)
                    .Select(l => l.Book);
        }

        //listing books between dates
        public IEnumerable<Book> GetBooksLoanedBetweenDates(DateTime startDate, DateTime endDate)
        {
            return _loanRepository.ReadAll()
                    .Where(l => l.LoanDate >= startDate && l.ReturnDate <= endDate)
                    .Select(l => l.Book);
        }
    }
}
