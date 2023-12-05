using System;
using System.Collections.Generic;
using System.Linq;
using R9IOPN_HFT_2023241.Models;

namespace R9IOPN_HFT_2023241.Logic
{
    public interface IBookLogic
    {
        void Create(Book item);
        void Delete(int id);
        IEnumerable<BookLogic.BookDetail> GetBooksByAuthor(int authorId);
        IEnumerable<BookLogic.BookDetail> GetBooksByGenre(string genre);
        IEnumerable<BookLogic.BookDetail> GetBooksLoanedBetweenDates(DateTime startDate, DateTime endDate);
        IEnumerable<BookLogic.UserLoanDetail> GetBooksLoanedByUser(int userId);
        IEnumerable<BookLogic.BookLoanCount> GetMostLoanedBooks();
        Book Read(int id);
        IQueryable<Book> ReadAll();
        void Update(Book item);
    }
}