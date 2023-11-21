using System;
using System.Linq;
using R9IOPN_HFT_2023241.Logic;
using R9IOPN_HFT_2023241.Models;
using R9IOPN_HFT_2023241.Repository;

namespace R9IOPN_HFT_2023241.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //IRepository<Book> rep = new BookRepository(new BookDbContext());


            //Book b = new Book()
            //{
            //    Title = "Prisoners"
            //};

            //rep.Create(b);

            //var another = rep.Read(1);
            //another.Title = "Truman";
            //rep.Update(another);

            //var items = rep.ReadAll().ToArray();

            var ctx = new BookDbContext();
            var repo = new BookRepository(ctx);
            var repo2 = new LoanRepository(ctx);
            var logic = new BookLogic(repo, repo2);

            Book book = new Book()
            {
                AuthorId = 1,
                Title = "Tehat",
                Genre = "Horror",
                PublicationYear = 2022
            };
            //logic.Create(book);

            var nc = logic.GetBooksByGenre("Mystery");
            var nc2 = logic.GetBooksLoanedByUser(1);
            var nc3 = logic.GetBooksByAuthor(1);
            var nc4 = logic.GetBooksLoanedBetweenDates(new DateTime(2020,1,1), new DateTime(2023, 1, 1));
            //var nc5 = logic.GetMostLoanedBooks();
            var item = logic.ReadAll();

            
            
        }
    }
}
