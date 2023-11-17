using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R9IOPN_HFT_2023241.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected BookDbContext context;

        public Repository(BookDbContext ctx)
        {
            this.context = ctx;
        }

        public void Create(T item)
        {
            context.Set<T>().Add(item);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.Set<T>().Remove(Read(id));
            context.SaveChanges();
        }

        public abstract T Read(int id);


        public IQueryable<T> ReadAll()
        {
            return context.Set<T>();
        }

        public abstract void Update(T item);
    }
}
