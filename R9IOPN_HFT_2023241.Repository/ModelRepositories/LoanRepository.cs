using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R9IOPN_HFT_2023241.Models;

namespace R9IOPN_HFT_2023241.Repository
{
    public class LoanRepository : Repository<Loan>, IRepository<Loan>
    {
        public LoanRepository(BookDbContext ctx) : base(ctx)
        {
        }
        
        public override Loan Read(int id)
        {
            return context.Loans.FirstOrDefault(l => l.LoanId== id);
        }

        public override void Update(Loan item)
        {
            var existingLoan = Read(item.LoanId);
            if (existingLoan == null)
            {
                throw new InvalidOperationException("The loan cannot be found in our currand database");
            }

            context.Entry(existingLoan).CurrentValues.SetValues(item);
            context.SaveChanges();
        }
    }
}
