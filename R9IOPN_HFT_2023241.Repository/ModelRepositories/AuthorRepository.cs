using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
            var old = Read(item.AuthorId);
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
