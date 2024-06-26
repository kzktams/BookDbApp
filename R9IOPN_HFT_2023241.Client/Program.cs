﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ConsoleTools;
using R9IOPN_HFT_2023241.Models;


namespace R9IOPN_HFT_2023241.Client
{
    internal class Program
    {

        static RestService rest;
        static void Create(string entity)
        {
            if (entity == "Book")
            {
                Console.Write("Enter the title: ");
                string title = Console.ReadLine();
                Console.WriteLine("Enter the genre from the list (Mystery, Science Fiction, Fantasy, Adventure, Horror, Drama, Thriller): ");
                string genre = Console.ReadLine();
                Console.WriteLine("Enter publication year: ");
                int year = int.Parse(Console.ReadLine());

                List<Author> authors = rest.Get<Author>("author");

                Console.WriteLine("Enter author name:");
                string authorName = Console.ReadLine();

                var author = authors.FirstOrDefault(a => a.Name.Equals(authorName, StringComparison.OrdinalIgnoreCase));

                if (author == null)
                {
                    Console.WriteLine("Author not found. Do you want to add this new author? (yes/no)");
                    string response = Console.ReadLine();

                    if (response.Equals("yes", StringComparison.OrdinalIgnoreCase))
                    {
                        var newAuthor = new Author { Name = authorName };
                        rest.Post(newAuthor, "author");

                        author = rest.Get<Author>("author").LastOrDefault();
                    }
                    else
                    {
                        Console.WriteLine("Book creation cancelled.");
                        return;
                    }
                }

                rest.Post(new Book { Title = title, Genre = genre, PublicationYear = year, AuthorId = author.AuthorId }, "book");
                Console.WriteLine("Book created successfully.");
            }
            if (entity == "Author")
            {
                
                Console.Write("Enter the author's name: ");
                string name = Console.ReadLine();

                //correct form
                Console.Write("Enter the author's birth date (yyyy.MM.dd): ");
                string birthDateString = Console.ReadLine();
                DateTime birthDate;
                while (!DateTime.TryParse(birthDateString, out birthDate))
                {
                    Console.Write("Invalid date format. Please enter the date in yyyy-MM-dd format: ");
                    birthDateString = Console.ReadLine();
                }

                
                Console.Write("Enter the author's country: ");
                string country = Console.ReadLine();

                
                var newAuthor = new Author
                {
                    Name = name,
                    BirthDate = birthDate,
                    Country = country
                };

                // API
                rest.Post(newAuthor, "author");
                Console.WriteLine("Author created successfully.");
            }
            if (entity == "User")
            {
                Console.Write("Enter the user's name: ");
                string name = Console.ReadLine();

                Console.Write("Enter the user's email: ");
                string email = Console.ReadLine();

                Console.Write("Enter the user's phone number: ");
                string phone = Console.ReadLine();

                var newUser = new User
                {
                    Name = name,
                    Email = email,
                    Phone = phone
                };

                // API
                rest.Post(newUser, "user");
                Console.WriteLine("User created successfully.");
            }
            if (entity == "Loan")
            {
                Console.Write("Enter the user's ID: ");
                int userId = int.Parse(Console.ReadLine());

                Console.Write("Enter the book's ID: ");
                int bookId = int.Parse(Console.ReadLine());

                Console.Write("Enter the loan date (yyyy.MM.dd): ");
                DateTime loanDate;
                while (!DateTime.TryParse(Console.ReadLine(), out loanDate))
                {
                    Console.Write("Invalid date format. Please enter the date in yyyy-MM-dd format: ");
                }

                Console.Write("Enter the return date (yyyy.MM.dd): ");
                DateTime returnDate;
                while (!DateTime.TryParse(Console.ReadLine(), out returnDate))
                {
                    Console.Write("Invalid date format. Please enter the date in yyyy-MM-dd format: ");
                }

                var newLoan = new Loan
                {
                    UserId = userId,
                    BookId = bookId,
                    LoanDate = loanDate,
                    ReturnDate = returnDate
                };

                //API
                rest.Post(newLoan, "loan");
                Console.WriteLine("Loan created successfully.");
            }

            Console.ReadLine();
        }
        static void List(string entity)
        {
            if (entity == "Book")
            {
                List<Book> books = rest.Get<Book>("book");
                foreach (var item in books)
                {

                    Console.WriteLine("BookID: "+item.BookId +" | Title: "+item.Title+" | Genre: "+ item.Genre +" | AuthorID: "+item.AuthorId+" | Author Name: "+item.Author.Name);
                }
            }

            if (entity == "Author")
            {
                List<Author> authors = rest.Get<Author>("author");
                foreach (var author in authors)
                {
                    Console.WriteLine($"AuthorID: {author.AuthorId} | Name: {author.Name} | Birthdate: {author.BirthDate.ToShortDateString()} | Country: {author.Country}");
                }
            }

            if (entity == "User")
            {
                List<User> users = rest.Get<User>("user");
                foreach (var user in users)
                {
                    Console.WriteLine($"UserID: {user.UserId} | Name: {user.Name} | Email: {user.Email} | Phone number: {user.Phone} | Loans: ");

                    if (user.Loans != null && user.Loans.Any())
                    {
                        foreach (var loan in user.Loans)
                        {
                            Console.WriteLine($"Loan ID: {loan.LoanId}, Book ID: {loan.BookId}, Loan Date: {loan.LoanDate.ToShortDateString()}, Return Date: {loan.ReturnDate.ToShortDateString()}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There's no loans from this user");
                    }
                    Console.WriteLine(new string('-', 50));
                }
            }

            if (entity == "Loan")
            {
                List<Loan> loans = rest.Get<Loan>("loan");
                foreach (var loan in loans)
                {
                    Console.WriteLine($"Loan ID: {loan.LoanId}, User ID: {loan.UserId}, Book ID: {loan.BookId}, Loan Date: {loan.LoanDate.ToShortDateString()}, Return Date: {loan.ReturnDate.ToShortDateString()}");
                }
            }

            Console.ReadLine();
        }
        static void Update(string entity)
        {
            if (entity == "Book")
            {
                Console.Write("Enter the book's ID to update: ");
                int bookId = int.Parse(Console.ReadLine());
                Book one = rest.Get<Book>(bookId, "book");

                if (one == null)
                {
                    Console.WriteLine("Book not found.");
                    return;
                }

                Console.Write($"Enter the new title [Old: {one.Title}]: ");
                one.Title = Console.ReadLine();

                Console.WriteLine($"Enter the new genre from the list (Mystery, Science Fiction, Fantasy, Adventure, Horror, Drama, Thriller) [Old: {one.Genre}]: ");
                one.Genre = Console.ReadLine();

                Console.WriteLine($"Enter the new publication year [Old: {one.PublicationYear}]: ");
                one.PublicationYear = int.Parse(Console.ReadLine());

                Console.WriteLine("Do you want to add a new author? (yes/no)");
                string addNewAuthor = Console.ReadLine();

                if (addNewAuthor.Equals("yes", StringComparison.OrdinalIgnoreCase))
                {
                    Console.Write("Enter the new author's name: ");
                    string name = Console.ReadLine();

                    var newAuthor = new Author { Name = name };
                    rest.Post(newAuthor, "author");

                    one.AuthorId = rest.Get<Author>("author").Last().AuthorId;
                }
                else
                {
                    Console.WriteLine($"Enter the existing author's ID [Old: {one.AuthorId}]: ");
                    one.AuthorId = int.Parse(Console.ReadLine());
                }

                rest.Put(one, "book");
                Console.WriteLine("Book updated successfully.");
            }
            if (entity == "Author")
            {
                Console.Write("Enter the author's ID to update: ");
                int authorId = int.Parse(Console.ReadLine());
                Author old = rest.Get<Author>(authorId, "author");
                if (old == null)
                {
                    Console.WriteLine("Author not found.");
                    return;
                }

                Console.Write($"Enter the new name of the author [Old: {old.Name}]: ");
                old.Name = Console.ReadLine();

                Console.Write($"Enter the new birth date of the author (yyyy.MM.dd) [Old: {old.BirthDate.ToShortDateString()}]: ");
                DateTime birthDate;
                while (!DateTime.TryParse(Console.ReadLine(), out birthDate))
                {
                    Console.Write("Invalid date format. Please enter the date in yyyy.MM.dd format: ");
                }
                old.BirthDate = birthDate;

                Console.Write($"Enter the new country of the author [Old: {old.Country}]: ");
                old.Country = Console.ReadLine();

                
                rest.Put(old, "author");
                Console.WriteLine("Author updated successfully.");
            }
            if (entity == "User")
            {
                Console.Write("Enter the user's ID to update: ");
                int userId = int.Parse(Console.ReadLine());

                User old = rest.Get<User>(userId, "user");
                if (old == null)
                {
                    Console.WriteLine("User not found.");
                    return;
                }

                Console.Write($"Enter the new name of the user [Old: {old.Name}]: ");
                old.Name = Console.ReadLine();

                Console.Write($"Enter the new email of the user [Old: {old.Email}]: ");
                old.Email = Console.ReadLine();

                Console.Write($"Enter the new phone number of the user [Old: {old.Phone}]: ");
                old.Phone = Console.ReadLine();

                rest.Put(old, "user");
                Console.WriteLine("User updated successfully.");
            }
            if (entity == "Loan")
            {
                Console.Write("Enter the loan's ID to update: ");
                int loanId = int.Parse(Console.ReadLine());

                // Lekérdezi a kölcsönzést
                Loan old = rest.Get<Loan>(loanId, "loan");
                if (old == null)
                {
                    Console.WriteLine("Loan not found.");
                    return;
                }

                Console.Write($"Enter the new user ID for the loan [Old: {old.UserId}]: ");
                old.UserId = int.Parse(Console.ReadLine());

                Console.Write($"Enter the new book ID for the loan [Old: {old.BookId}]: ");
                old.BookId = int.Parse(Console.ReadLine());

                Console.Write($"Enter the new loan date (yyyy.MM.dd) [Old: {old.LoanDate.ToShortDateString()}]: ");
                old.LoanDate = DateTime.Parse(Console.ReadLine());

                Console.Write($"Enter the new return date (yyyy.MM.dd) [Old: {old.ReturnDate.ToShortDateString()}]: ");
                old.ReturnDate = DateTime.Parse(Console.ReadLine());

                rest.Put(old, "loan");
                Console.WriteLine("Loan updated successfully.");
            }

            Console.ReadLine();
        }
        static void Delete(string entity)
        {
            if (entity == "Book")
            {
                Console.WriteLine("Enter the ID of the book you would like to remove: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "book");
                Console.WriteLine("Book removed successfully");
            }
            if (entity == "Author")
            {
                Console.WriteLine("Enter the ID of the author you would like to remove: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "author");
                Console.WriteLine("Author removed successfully");
            }
            if (entity == "User")
            {
                Console.WriteLine("Enter the ID of the user you would like to remove: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "user");
                Console.WriteLine("User removed successfully");
            }
            if (entity == "Loan")
            {
                Console.WriteLine("Enter the ID of the loan you would like to remove: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "loan");
                Console.WriteLine("Loan removed successfully");
            }

            Console.ReadLine();
        }
        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:4356/","swagger");

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

            var noncrudSubMenu = new ConsoleMenu(args, level: 1)
                .Add("Books by author", () => BooksByAuthor("BooksByAuthor"))
                .Add("Most loaned books", ()=> MostLoanedBooks("MostLoanedBooks"))
                .Add("Books by genre", ()=> BooksByGenre("BooksByGenre"))
                .Add("Books loaned by user", ()=> BooksLoanedByUser("BooksLoanedByUser"))
                .Add("Books loaned between dates", ()=> BooksLoanedBetweenDates("BooksLoanedBetweenDates"))
                .Add("Authors by name", ()=> AuthorsByName("AuthorsByName"))
                .Add("Author popularity", ()=> AuthorPopularities("AuthorPopularities"))
                .Add("User activity", ()=> Activities("Activity"))
                .Add("Exit", ConsoleMenu.Close)
                ;

            var menu = new ConsoleMenu(args, level: 0)
                .Add("Books", () => bookSubMenu.Show())
                .Add("Authors", () => authorSubMenu.Show())
                .Add("Users", () => userSubMenu.Show())
                .Add("Loans", () => loanSubMenu.Show())
                .Add("Non-Cruds", () => noncrudSubMenu.Show());

            menu.Show();
        }
        //noncruds
        private static void Activities(string endpoint)
        {
            var result = rest.Get<UserActivity>($"Stat/{endpoint}");

            foreach (var item in result)
            {
                Console.WriteLine($"UserId: {item.UserId} | Name: {item.Name} | Loan count: {item.LoanCount}");
            }
            Console.ReadLine();
        }

        private static void AuthorsByName(string endpoint)
        {
            Console.WriteLine("Enter the searched author: ");
            string author = Console.ReadLine();
            var result = rest.Get<AuthorDetail>($"Stat/{endpoint}/{author}");

            foreach (var item in result)
            {
                Console.WriteLine($"AuthorId: {item.AuthorId} | Name: {item.Name} | Birth date: {item.BirthDate.ToShortTimeString()} | Country: {item.Country} | Book count: {item.BookCount}");
            }
            Console.ReadLine();
        }

        private static void BooksLoanedBetweenDates(string endpoint)
        {
            Console.Write("Enter the start date (yyyy.MM.dd): ");
            string startDate = Console.ReadLine();
            Console.Write("Enter the end date (yyyy.MM.dd): ");
            string endDate = Console.ReadLine();
            var result = rest.Get<BookDetail>($"Stat/{endpoint}/{startDate},{endDate}");

            foreach (var item in result)
            {
                Console.WriteLine($"BookId: {item.BookId} | Title: {item.Title} | Puplication year: {item.PublicationYear} | Genre: {item.Genre}");
            }
            Console.ReadLine();
        }

        private static void BooksLoanedByUser(string endpoint)
        {
            Console.WriteLine("Enter the searched user's ID: ");
            int userId= int.Parse(Console.ReadLine());
            var result = rest.Get<UserLoanDetail>($"Stat/{endpoint}/{userId}");

            foreach (var item in result)
            {
                Console.WriteLine($"BookId: {item.BookId} | Title: {item.BookTitle} | Username: {item.UserName} | Loan date: {item.LoanDate.ToShortDateString()} | Return date: {item.ReturnDate.ToShortDateString()}");
            }
            Console.ReadLine();
        }

        private static void BooksByGenre(string endpoint)
        {
            Console.WriteLine("Enter the genre from the list (Mystery, Science Fiction, Fantasy, Adventure, Horror, Drama, Thriller): ");
            string genre = Console.ReadLine();
            var result = rest.Get<BookDetail>($"Stat/{endpoint}/{genre}");
            foreach (var item in result)
            {
                Console.WriteLine($"BookId: {item.BookId} | Title: {item.Title} | Puplication year: {item.PublicationYear} | Genre: {item.Genre}");
            }
            Console.ReadLine();
        }

        private static void AuthorPopularities(string endpoint)
        {
            
            var result = rest.Get<AuthorPopularity>($"Stat/{endpoint}");

            foreach (var item in result)
            {
                Console.WriteLine($"AuthorId: {item.AuthorId} | Name: {item.AuthorName} | Loan count: {item.LoanCount}");
            }
            Console.ReadLine();
        }

        private static void BooksByAuthor(string endpoint)
        {
            Console.WriteLine("Enter the searched author's ID: ");
            int authorId = int.Parse(Console.ReadLine());
            var result = rest.Get<BookDetail>($"Stat/{endpoint}/{authorId}");
            
            foreach (var item in result)
            {
                Console.WriteLine($"BookId: {item.BookId} | Title: {item.Title} | Puplication year: {item.PublicationYear} | Genre: {item.Genre}") ;
            }
            Console.ReadLine();
        }

        private static void MostLoanedBooks(string endpoint)
        {
            
            var result = rest.Get<BookLoanCount>($"Stat/{endpoint}");
            
            foreach (var item in result)
            {
                Console.WriteLine($"BookId: {item.BookId} | Title: {item.Title} | Loan count: {item.LoanCount}");
            }
            Console.ReadLine();
        }

        
    }
}
