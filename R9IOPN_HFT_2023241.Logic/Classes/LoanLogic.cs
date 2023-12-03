using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R9IOPN_HFT_2023241.Models;
using R9IOPN_HFT_2023241.Repository;

namespace R9IOPN_HFT_2023241.Logic
{
    public class LoanLogic : ILoanLogic
    {

        IRepository<Loan> loanRepo;

        public LoanLogic(IRepository<Loan> loanRepo)
        {
            this.loanRepo = loanRepo;
        }

        public void Create(Loan item)
        {
            loanRepo.Create(item);
        }

        public void Delete(int id)
        {
            loanRepo.Delete(id);
        }

        public Loan Read(int id)
        {
            return loanRepo.Read(id);
        }

        public IQueryable<Loan> ReadAll()
        {
            return loanRepo.ReadAll();
        }

        public void Update(Loan item)
        {
            loanRepo.Update(item);
        }
    }
}
