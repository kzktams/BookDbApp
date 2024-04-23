using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using R9IOPN_HFT_2023241.Endpoint.Services;
using R9IOPN_HFT_2023241.Logic;
using R9IOPN_HFT_2023241.Models;


namespace R9IOPN_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserLogic userLogic;
        IHubContext<SignalRHub> hub;

        public UserController(IUserLogic userLogic, IHubContext<SignalRHub> hub)
        {
            this.userLogic = userLogic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<User> ReadAll()
        {
            return this.userLogic.ReadAll();
        }

        [HttpGet("{id}")]
        public User Read(int id)
        {
            return this.userLogic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] User value)
        {
            this.userLogic.Create(value);
            this.hub.Clients.All.SendAsync("UserCreated", value);
        }


        [HttpPut]
        public void Update([FromBody] User value)
        {
            this.userLogic.Update(value);
            this.hub.Clients.All.SendAsync("UserUpdated", value);

        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {

            var usertodelete = this.userLogic.Read(id);
            this.userLogic.Delete(id);
            this.hub.Clients.All.SendAsync("UserDeleted", usertodelete);
        }
    }
}
