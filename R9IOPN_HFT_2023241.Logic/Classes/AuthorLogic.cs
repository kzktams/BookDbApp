﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R9IOPN_HFT_2023241.Models;
using R9IOPN_HFT_2023241.Repository;

namespace R9IOPN_HFT_2023241.Logic
{
    public class AuthorLogic : IAuthorLogic
    {
        IRepository<Author> _authorRepository;
        IRepository<Book> _bookRepository;
        IRepository<Loan> _loanRepository;
        public AuthorLogic(IRepository<Author> authorRepository, IRepository<Book> bookRepository, IRepository<Loan> loanRepository)
        {
            this._authorRepository = authorRepository;
            this._bookRepository = bookRepository;
            this._loanRepository = loanRepository;
        }


        public void Create(Author item)
        {
            if (string.IsNullOrWhiteSpace(item.Name))
            {
                throw new ArgumentException("Author name cannot be empty, if Author not known please type: Anonymus");
            }
            if (item.BirthDate > DateTime.Now)
            {
                throw new ArgumentException("Author cannot born in the future");
            }
            this._authorRepository.Create(item);
        }

        public void Delete(int id)
        {
            var author = _authorRepository.Read(id);
            if (author == null)
            {
                throw new InvalidOperationException("Author isn't found");
            }
            this._authorRepository.Delete(id);
        }

        public Author Read(int id)
        {
            return this._authorRepository.Read(id);
        }

        public IQueryable<Author> ReadAll()
        {
            return this._authorRepository.ReadAll();
        }

        public void Update(Author item)
        {
            if (string.IsNullOrWhiteSpace(item.Name))
            {
                throw new ArgumentException("Author name cannot be empty, if Author not known please type Anonymus");
            }
            if (item.BirthDate > DateTime.Now)
            {
                throw new ArgumentException("Author cannot born in the future");
            }
            this._authorRepository.Update(item);
        }
        //nonCRUD
        public IEnumerable<AuthorDetail> SearchAuthorsByName(string name)
        {
            var authors = _authorRepository.ReadAll()
                                           .Where(a => a.Name.Contains(name)).ToList();

            var authorDetails = authors.Select(a => new AuthorDetail
            {
                AuthorId = a.AuthorId,
                Name = a.Name,
                BirthDate = a.BirthDate,
                Country = a.Country,
                BookCount = _bookRepository.ReadAll().Count(b => b.AuthorId == a.AuthorId)
            });

            return authorDetails;
        }

        public IEnumerable<AuthorPopularity> GetMostPopularAuthors()
        {
            var authorLoanCounts = _loanRepository.ReadAll()
                                         .GroupBy(l => l.Book.AuthorId)
                                         .Select(group => new
                                         {
                                             AuthorId = group.Key,
                                             LoanCount = group.Count()
                                         })
                                         .OrderByDescending(x => x.LoanCount)
                                         .ToList();

            // Gyűjtsük össze az összes szerző azonosítót
            var authorIds = authorLoanCounts.Select(a => a.AuthorId).Distinct().ToList();

            // Lekérjük egyszerre az összes releváns szerzőt
            var authors = _authorRepository.ReadAll()
                                           .Where(author => authorIds.Contains(author.AuthorId))
                                           .ToDictionary(author => author.AuthorId, author => author.Name);

            // Csak a létező szerzőkkel térünk vissza
            var popularAuthors = authorLoanCounts.Where(lc => authors.ContainsKey(lc.AuthorId))
                                                 .Select(lc => new AuthorPopularity
                                                 {
                                                     AuthorId = lc.AuthorId,
                                                     AuthorName = authors[lc.AuthorId],
                                                     LoanCount = lc.LoanCount
                                                 });

            return popularAuthors;
        }

        
        
    }

    
}
