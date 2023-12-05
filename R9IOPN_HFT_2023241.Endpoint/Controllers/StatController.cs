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
        

        public StatController(IBookLogic bookLogic, IAuthorLogic authorLogic, IUserLogic userLogic)
        {
            this.bookLogic = bookLogic;
            this.authorLogic = authorLogic;
            this.userLogic = userLogic;
            
        }
        //booklogic
        [HttpGet("{authorId}")]
        public IEnumerable<BookLogic.BookDetail> BooksByAuthor(int authorId)
        {
            return this.bookLogic.GetBooksByAuthor(authorId);
        }
        [HttpGet]
        public IEnumerable<BookLogic.BookLoanCount> MostLoanedBooks(int books)
        {
            return this.bookLogic.GetMostLoanedBooks();
        }

        [HttpGet("{genre}")]
        public IEnumerable<BookLogic.BookDetail> BooksByGenre(string genre)
        {
            return this.bookLogic.GetBooksByGenre(genre);
        }

        [HttpGet("{userId}")]
        public IEnumerable<BookLogic.UserLoanDetail> BooksLoanedByUser(int userId)
        {
            return this.bookLogic.GetBooksLoanedByUser(userId);
        }
        [HttpGet("{startDate},{endDate}")]
        public IEnumerable<BookLogic.BookDetail> BooksLoanedBetweenDates(DateTime startDate, DateTime endDate)
        {
            return this.bookLogic.GetBooksLoanedBetweenDates(startDate, endDate);
        }
        //authorlogic
        [HttpGet("{name}")]
        public IEnumerable<AuthorLogic.AuthorDetail> AuthorsByName(string name)
        {
            return this.authorLogic.SearchAuthorsByName(name);
        }
        [HttpGet]
        public IEnumerable<AuthorLogic.AuthorPopularity> AuthorPopularities(int id)
        {
            return this.authorLogic.GetMostPopularAuthors();
        }

        //userlogic
        [HttpGet]
        public IEnumerable<UserLogic.UserActivity> Activity(int id)
        {
            return this.userLogic.GetMostActiveUsers();
        }
    }
}
