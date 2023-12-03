using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using R9IOPN_HFT_2023241.Logic;
using R9IOPN_HFT_2023241.Models;


namespace R9IOPN_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserLogic userLogic;
        public UserController(IUserLogic userLogic)
        {
            this.userLogic = userLogic;
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
        }

        
        [HttpPut("{id}")]
        public void Update([FromBody] User value)
        {
            this.userLogic.Update(value);
        }

        
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.userLogic.Delete(id);
        }
    }
}
