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
            var existingBook = Read(item.BookId);
            if (existingBook == null)
            {
                throw new InvalidOperationException("The book cannot be found in our currand database");
            }

            context.Entry(existingBook).CurrentValues.SetValues(item);
            context.SaveChanges();
        }

    }
}
