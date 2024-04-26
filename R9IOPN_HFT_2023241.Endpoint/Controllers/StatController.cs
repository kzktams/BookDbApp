using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using R9IOPN_HFT_2023241.Endpoint.Services;
using R9IOPN_HFT_2023241.Logic;
using R9IOPN_HFT_2023241.Models;

namespace R9IOPN_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IBookLogic bookLogic;
        IAuthorLogic authorLogic;
        IUserLogic userLogic;
        IHubContext<SignalRHub> hub;


        public StatController(IBookLogic bookLogic, IAuthorLogic authorLogic, IUserLogic userLogic, IHubContext<SignalRHub> hub)
        {
            this.bookLogic = bookLogic;
            this.authorLogic = authorLogic;
            this.userLogic = userLogic;
            this.hub = hub;
        }
        //booklogic
        [HttpGet("{authorId}")]
        public IEnumerable<BookDetail> BooksByAuthor(int authorId)
        {
            return this.bookLogic.GetBooksByAuthor(authorId);
            this.hub.Clients.All.SendAsync("GotBooksByAuthor", authorId);
        }
        [HttpGet]
        public IEnumerable<BookLoanCount> MostLoanedBooks(int books)
        {
            return this.bookLogic.GetMostLoanedBooks();
            this.hub.Clients.All.SendAsync("GotMostLoanedBooks", books);
        }

        [HttpGet("{genre}")]
        public IEnumerable<BookDetail> BooksByGenre(string genre)
        {
            return this.bookLogic.GetBooksByGenre(genre);
            this.hub.Clients.All.SendAsync("GotBooksByGenre", genre);

        }

        [HttpGet("{userId}")]
        public IEnumerable<UserLoanDetail> BooksLoanedByUser(int userId)
        {
            return this.bookLogic.GetBooksLoanedByUser(userId);
        }
        [HttpGet("{startDate},{endDate}")]
        public IEnumerable<BookDetail> BooksLoanedBetweenDates(DateTime startDate, DateTime endDate)
        {
            return this.bookLogic.GetBooksLoanedBetweenDates(startDate, endDate);
        }
        //authorlogic
        [HttpGet("{name}")]
        public IEnumerable<AuthorDetail> AuthorsByName(string name)
        {
            return this.authorLogic.SearchAuthorsByName(name);
        }
        [HttpGet]
        public IEnumerable<AuthorPopularity> AuthorPopularities(int id)
        {
            return this.authorLogic.GetMostPopularAuthors();
        }

        //userlogic
        [HttpGet]
        public IEnumerable<UserActivity> Activity(int id)
        {
            return this.userLogic.GetMostActiveUsers();
        }
    }
}
