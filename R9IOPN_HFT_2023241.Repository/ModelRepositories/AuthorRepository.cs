using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using R9IOPN_HFT_2023241.Models;

namespace R9IOPN_HFT_2023241.Repository
{
    public class AuthorRepository : Repository<Author>, IRepository<Author>
    {
        public AuthorRepository(BookDbContext ctx) : base(ctx)
        {
        }

        public override Author Read(int id)
        {
            return context.Authors.FirstOrDefault(a => a.AuthorId == id);
        }

        public override void Update(Author item)
        {
            var existingAuthor = Read(item.AuthorId);
            if (existingAuthor == null)
            {
                throw new InvalidOperationException("The author cannot be found in our currand database");
            }

            context.Entry(existingAuthor).CurrentValues.SetValues(item);
            context.SaveChanges();
        }
    }
}
