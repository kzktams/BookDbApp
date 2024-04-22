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
    public class AuthorController : ControllerBase
    {
        IAuthorLogic authorLogic;
        IHubContext<SignalRHub> hub;
        public AuthorController(IAuthorLogic authorLogic, IHubContext<SignalRHub> hub)
        {
            this.authorLogic = authorLogic;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("AuthorCreated", value);
        }

        [HttpPut]
        public void Update( [FromBody] Author value)
        {
            this.authorLogic.Update(value);
            this.hub.Clients.All.SendAsync("AuthorUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var authortodelete = this.authorLogic.Read(id);
            this.authorLogic.Delete(id);
            this.hub.Clients.All.SendAsync("AuthorDeleted", authortodelete);
        }
    }
}
