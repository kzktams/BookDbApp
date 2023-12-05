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
            var old = Read(item.LoanId);
            if (old == null)
            {
                throw new NullReferenceException();
            }
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            context.SaveChanges();
        }
    }
}
