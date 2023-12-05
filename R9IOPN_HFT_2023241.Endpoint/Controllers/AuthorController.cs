using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using R9IOPN_HFT_2023241.Logic;
using R9IOPN_HFT_2023241.Models;


namespace R9IOPN_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        IAuthorLogic authorLogic;

        public AuthorController(IAuthorLogic authorLogic)
        {
            this.authorLogic = authorLogic;
        }


        [HttpGet]
        public IEnumerable<Author> ReadAll()
        {
            return this.authorLogic.ReadAll();
        }

        [HttpGet("{id}")]
        public Author Read(int id)
        {
            return this.authorLogic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Author value)
        {
            this.authorLogic.Create(value);
        }

        [HttpPut]
        public void Update( [FromBody] Author value)
        {
            this.authorLogic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.authorLogic.Delete(id);
        }
    }
}
