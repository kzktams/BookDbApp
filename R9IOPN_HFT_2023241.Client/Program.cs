using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleTools;
using R9IOPN_HFT_2023241.Logic;
using R9IOPN_HFT_2023241.Models;
using R9IOPN_HFT_2023241.Repository;

namespace R9IOPN_HFT_2023241.Client
{
    internal class Program
    {
        static void Create(string entity)
        {
            Console.WriteLine(entity + " create");
            Console.ReadLine();
        }
        static void List(string entity)
        {
            Console.WriteLine(entity + " list");
            Console.ReadLine();
        }
        static void Update(string entity)
        {
            Console.WriteLine(entity + " update");
            Console.ReadLine();
        }
        static void Delete(string entity)
        {
            Console.WriteLine(entity + " delete");
            Console.ReadLine();
        }
        static void Main(string[] args)
        {
            var ctx = new BookDbContext();

            var bookRepo = new BookRepository(ctx);
            var authorRepo = new AuthorRepository(ctx);
            var loanRepo = new LoanRepository(ctx);
            var userRepo = new UserRepository(ctx);

            var bookLogic = new BookLogic(bookRepo, loanRepo, userRepo);
            var authorLogic = new AuthorLogic(authorRepo, bookRepo, loanRepo);
            var userLogic = new UserLogic(userRepo, loanRepo);
            var loanLogic = new LoanLogic(loanRepo);

            var loanSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Loan"))
                .Add("Create", () => Create("Loan"))
                .Add("Delete", () => Delete("Loan"))
                .Add("Update", () => Update("Loan"))
                .Add("Exit", ConsoleMenu.Close);

            var userSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("User"))
                .Add("Create", () => Create("User"))
                .Add("Delete", () => Delete("User"))
                .Add("Update", () => Update("User"))
                .Add("Exit", ConsoleMenu.Close);


            var authorSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Author"))
                .Add("Create", () => Create("Author"))
                .Add("Delete", () => Delete("Author"))
                .Add("Update", () => Update("Author"))
                .Add("Exit", ConsoleMenu.Close);

            var bookSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Book"))
                .Add("Create", () => Create("Book"))
                .Add("Delete", () => Delete("Book"))
                .Add("Update", () => Update("Book"))
                .Add("Exit", ConsoleMenu.Close);

            var menu = new ConsoleMenu(args, level: 0)
                .Add("Books", () => bookSubMenu.Show())
                .Add("Authors", () => authorSubMenu.Show())
                .Add("Users", () => userSubMenu.Show())
                .Add("Loans", () => loanSubMenu.Show());

            menu.Show();

            IRepository<Book> rep = new BookRepository(new BookDbContext());


            //Book b = new Book()
            //{
            //    Title = "Prisoners"
            //};

            //rep.Create(b);

            //var another = rep.Read(1);
            //another.Title = "Truman";
            //rep.Update(another);

            //var items = rep.ReadAll().ToArray();

        //    var ctx = new BookDbContext();
        //var repo = new BookRepository(ctx);
        //var repo2 = new LoanRepository(ctx);
        //var repo3 = new UserRepository(ctx);
        //var repo4 = new AuthorRepository(ctx);
        //var logic = new BookLogic(repo, repo2, repo3);
        //var logicA = new AuthorLogic(repo4, repo, repo2);
        //Book book = new Book()
        //{
        //    AuthorId = 1,
        //    Title = "Tehat",
        //    Genre = "Horror",
        //    PublicationYear = 2022
        //};
        //logic.Create(book);

        //var nc = logic.GetBooksByGenre("Mystery");
        //var nc2 = logic.GetBooksLoanedByUser(1);
            
            //var nc3 = logic.GetBooksByAuthor(1);
            //var nc4 = logic.GetBooksLoanedBetweenDates(new DateTime(2020,1,1), new DateTime(2023, 1, 1));
            //var nc5 = logic.GetMostLoanedBooks();

            //var nc6 = logicA.SearchAuthorsByName("John Smith");
            //var nc7 = logicA.GetMostPopularAuthors();
            //var item = logic.ReadAll();



        }
    }
}
