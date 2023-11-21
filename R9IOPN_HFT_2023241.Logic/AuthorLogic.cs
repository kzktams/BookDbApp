using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R9IOPN_HFT_2023241.Models;
using R9IOPN_HFT_2023241.Repository;

namespace R9IOPN_HFT_2023241.Logic
{
    internal class AuthorLogic
    {
        IRepository<Author> _author;
        public void Create(Author item)
        {
            this._author.Create(item);
        }

        public void Delete(int id)
        {
            this._author.Delete(id);
        }

        public Author Read(int id)
        {
            return this._author.Read(id);
        }

        public IQueryable<Author> ReadAll()
        {
            return this._author.ReadAll();
        }

        public void Update(Author item)
        {
            this._author.Update(item);
        }
    }
}
