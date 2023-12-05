using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using R9IOPN_HFT_2023241.Logic;
using R9IOPN_HFT_2023241.Models;

namespace R9IOPN_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        ILoanLogic loanLogic;

        public LoanController(ILoanLogic logic)
        {
            this.loanLogic = logic;
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
        }

        [HttpPut]
        public void Update([FromBody] Loan value)
        {
            this.loanLogic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.loanLogic.Delete(id);
        }
    }
}
