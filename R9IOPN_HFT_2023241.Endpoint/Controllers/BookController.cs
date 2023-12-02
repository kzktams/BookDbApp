using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using R9IOPN_HFT_2023241.Logic;
using R9IOPN_HFT_2023241.Models;

namespace R9IOPN_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        IBookLogic bookLogic;

        public BookController(IBookLogic bookLogic)
        {
            this.bookLogic = bookLogic;
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
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public void Update([FromBody] Book value)
        {
            this.bookLogic.Update(value);
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.bookLogic.Delete(id);
        }
    }
}
