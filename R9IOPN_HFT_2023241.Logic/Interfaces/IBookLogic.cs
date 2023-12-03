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
        IEnumerable<BookDetail> GetBooksByAuthor(int authorId);
        IEnumerable<BookDetail> GetBooksByGenre(string genre);
        IEnumerable<BookDetail> GetBooksLoanedBetweenDates(DateTime startDate, DateTime endDate);
        IEnumerable<UserLoanDetail> GetBooksLoanedByUser(int userId);
        IEnumerable<BookLoanCount> GetMostLoanedBooks();
        Book Read(int id);
        IQueryable<Book> ReadAll();
        void Update(Book item);
    }
}