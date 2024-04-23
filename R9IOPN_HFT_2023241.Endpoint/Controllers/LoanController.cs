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
    public class LoanController : ControllerBase
    {
        ILoanLogic loanLogic;
        IHubContext<SignalRHub> hub;
        public LoanController(ILoanLogic logic, IHubContext<SignalRHub> hub)
        {
            this.loanLogic = logic;
            this.hub = hub;
        }
        
        [HttpGet]
        public IEnumerable<Loan> ReadAll()
        {
            return this.loanLogic.ReadAll();
        }

        [HttpGet("{id}")]
        public Loan Read(int id)
        {
            return this.loanLogic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Loan value)
        {
            this.loanLogic.Create(value);
            this.hub.Clients.All.SendAsync("LoanCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Loan value)
        {
            this.loanLogic.Update(value);
            this.hub.Clients.All.SendAsync("LoanUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var loantodelete = this.loanLogic.Read(id);
            this.loanLogic.Delete(id);
            this.hub.Clients.All.SendAsync("LoanDeleted", loantodelete);
        }
    }
}
