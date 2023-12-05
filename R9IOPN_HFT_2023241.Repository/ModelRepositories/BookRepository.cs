using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R9IOPN_HFT_2023241.Models;

namespace R9IOPN_HFT_2023241.Repository
{
    public class BookRepository : Repository<Book>, IRepository<Book>
    {
        public BookRepository(BookDbContext ctx) : base(ctx)
        {
        }

        public override Book Read(int id)
        {
            return context.Books.FirstOrDefault(b => b.BookId == id);
        }

        public override void Update(Book item)
        {
            var old = Read(item.BookId);
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
