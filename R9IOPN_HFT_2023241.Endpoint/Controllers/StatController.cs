using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using R9IOPN_HFT_2023241.Logic;

namespace R9IOPN_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IBookLogic bookLogic;
        IAuthorLogic authorLogic;
        IUserLogic userLogic;
        ILoanLogic loanLogic;

        public StatController(IBookLogic bookLogic, IAuthorLogic authorLogic, IUserLogic userLogic, ILoanLogic loanLogic)
        {
            this.bookLogic = bookLogic;
            this.authorLogic = authorLogic;
            this.userLogic = userLogic;
            this.loanLogic = loanLogic;
        }
        //booklogic
        [HttpGet]
        public  IEnumerable<BookLogic.BookDetail> BooksByAuthor(int authorId)
        {
            return this.bookLogic.GetBooksByAuthor(authorId);
        }
        [HttpGet]
        public IEnumerable<BookLogic.BookLoanCount> MostLoanedBooks()
        {
            return this.bookLogic.GetMostLoanedBooks();
        }

        [HttpGet]
        public IEnumerable<BookLogic.BookDetail> BooksByGenre(string genre)
        {
            return this.bookLogic.GetBooksByGenre(genre);
        }

        [HttpGet]
        public IEnumerable<BookLogic.UserLoanDetail> BooksLoanedByUser(int userId)
        {
            return this.bookLogic.GetBooksLoanedByUser(userId);
        }
        [HttpGet]
        public IEnumerable<BookLogic.BookDetail> BooksLoanedBetweenDates(DateTime startDate, DateTime endDate)
        {
            return this.bookLogic.GetBooksLoanedBetweenDates(startDate,endDate);
        }
        //authorlogic
        [HttpGet]
        public IEnumerable<AuthorLogic.AuthorDetail> AuthorsByName(string name)
        {
            return this.authorLogic.SearchAuthorsByName(name);
        }

        public IEnumerable<AuthorLogic.AuthorPopularity> AuthorPopularities()
        {
            return this.authorLogic.GetMostPopularAuthors();
        }

        //userlogic
        [HttpGet]
        public IEnumerable<UserLogic.UserActivity> Activity()
        {
            return this.userLogic.GetMostActiveUsers();
        }
    }
}
