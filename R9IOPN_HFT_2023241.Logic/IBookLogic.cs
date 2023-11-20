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
        IEnumerable<Book> GetBooksByAuthor(int authorId);
        IEnumerable<Book> GetBooksByGenre(string genre);
        IEnumerable<Book> GetBooksLoanedBetweenDates(DateTime startDate, DateTime endDate);
        IEnumerable<Book> GetBooksLoanedByUser(int userId);
        IEnumerable<Book> GetMostLoanedBooks();
        Book Read(int id);
        IQueryable<Book> ReadAll();
        void Update(Book item);
    }
}