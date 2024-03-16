using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using R9IOPN_HFT_2023241.Endpoint.Services;
using R9IOPN_HFT_2023241.Logic;
using R9IOPN_HFT_2023241.Models;

namespace R9IOPN_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        IBookLogic bookLogic;
        IHubContext<SignalRHub> hub;

        public BookController(IBookLogic bookLogic, IHubContext<SignalRHub> hub)
        {
            this.bookLogic = bookLogic;
            this.hub = hub;
        }

        // GET: api/<BookController>
        [HttpGet]
        public IEnumerable<Book> ReadAll()
        {
            return this.bookLogic.ReadAll();
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public Book Read(int id)
        {
            return this.bookLogic.Read(id);
        }

        // POST api/<BookController>
        [HttpPost]
        public void Create([FromBody] Book value)
        {
            this.bookLogic.Create(value);
            this.hub.Clients.All.SendAsync("BookCreated", value);
        }

        // PUT api/<BookController>/5
        [HttpPut]
        public void Update([FromBody] Book value)
        {
            this.bookLogic.Update(value);
            this.hub.Clients.All.SendAsync("BookUpdated", value);
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var bookToDelete = this.bookLogic.Read(id);
            this.bookLogic.Delete(id);
            this.hub.Clients.All.SendAsync("BookDeleted", bookToDelete);
        }
    }
}
