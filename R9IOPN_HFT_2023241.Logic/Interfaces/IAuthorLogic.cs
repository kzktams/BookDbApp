using System.Collections.Generic;
using System.Linq;
using R9IOPN_HFT_2023241.Models;

namespace R9IOPN_HFT_2023241.Logic
{
    public interface IAuthorLogic
    {
        void Create(Author item);
        void Delete(int id);
        IEnumerable<AuthorLogic.AuthorPopularity> GetMostPopularAuthors();
        Author Read(int id);
        IQueryable<Author> ReadAll();
        IEnumerable<AuthorLogic.AuthorDetail> SearchAuthorsByName(string name);
        void Update(Author item);
    }
}