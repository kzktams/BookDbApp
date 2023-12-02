using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using R9IOPN_HFT_2023241.Logic;
using R9IOPN_HFT_2023241.Models;
using R9IOPN_HFT_2023241.Repository;

namespace R9IOPN_HFT_2023241.Test
{
    //public class FakeLoanRepo : IRepository<Loan>
    //{
    //    public void Create(Loan item)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Delete(int id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Loan Read(int id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IQueryable<Loan> ReadAll()
    //    {
    //        return new List<Loan>()
    //        {
    //new Loan { LoanId = 1, UserId = 1, BookId = 1, LoanDate = new DateTime(2022, 1, 10), ReturnDate = new DateTime(2022, 1, 20) },
    //new Loan { LoanId = 2, UserId = 1, BookId = 2, LoanDate = new DateTime(2022, 1, 15), ReturnDate = new DateTime(2022, 1, 25) },
    //new Loan { LoanId = 3, UserId = 3, BookId = 5, LoanDate = new DateTime(2022, 2, 1), ReturnDate = new DateTime(2022, 2, 11) },
    //new Loan { LoanId = 4, UserId = 4, BookId = 8, LoanDate = new DateTime(2022, 2, 5), ReturnDate = new DateTime(2022, 2, 15) },
    //new Loan { LoanId = 5, UserId = 5, BookId = 11, LoanDate = new DateTime(2022, 3, 10), ReturnDate = new DateTime(2022, 3, 20) },
    //new Loan { LoanId = 6, UserId = 6, BookId = 6, LoanDate = new DateTime(2022, 3, 15), ReturnDate = new DateTime(2022, 3, 25) },
    //new Loan { LoanId = 7, UserId = 7, BookId = 2, LoanDate = new DateTime(2022, 4, 1), ReturnDate = new DateTime(2022, 4, 11) },
    //new Loan { LoanId = 8, UserId = 13, BookId = 8, LoanDate = new DateTime(2022, 4, 5), ReturnDate = new DateTime(2022, 4, 15) },
    //new Loan { LoanId = 9, UserId = 15, BookId = 10, LoanDate = new DateTime(2022, 5, 10), ReturnDate = new DateTime(2022, 5, 20) },
    //new Loan { LoanId = 10, UserId = 10, BookId = 10, LoanDate = new DateTime(2022, 5, 15), ReturnDate = new DateTime(2023, 5, 25) }
    //        }.AsQueryable();
    //    }

    //    public void Update(Loan item)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
    //public class FakeUserRepo : IRepository<User>
    //{
    //    public void Create(User item)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Delete(int id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public User Read(int id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IQueryable<User> ReadAll()
    //    {
    //        return new List<User>()
    //        {
    //new User { UserId = 1, Name = "TestUser1", Email = "testEmail1", Phone = "+36262073389" },
    //new User { UserId = 2, Name = "TestUser2", Email = "testEmail2", Phone = "+36802834423" },
    //new User { UserId = 3, Name = "TestUser3", Email = "testEmail3", Phone = "+36917195844" },
    //new User { UserId = 4, Name = "TestUser4", Email = "testEmail4", Phone = "+36665244504" },
    //new User { UserId = 5, Name = "TestUser5", Email = "testEmail5", Phone = "+36365943802" },
    //new User { UserId = 6, Name = "TestUser6", Email = "testEmail6", Phone = "+36862082856" }
    //        }.AsQueryable();
    //    }

    //    public void Update(User item)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //public class FakeAuthorRepo : IRepository<Author>
    //{
    //    public void Create(Author item)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Delete(int id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Author Read(int id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IQueryable<Author> ReadAll()
    //    {
    //        return new List<Author>()
    //        {
    //new Author { AuthorId = 1, Name = "TestAuthor1", BirthDate = new DateTime(1965, 7, 21), Country = "USA" },
    //new Author { AuthorId = 2, Name = "TestAuthor2", BirthDate = new DateTime(1974, 1, 15), Country = "UK" },
    //new Author { AuthorId = 3, Name = "TestAuthor3", BirthDate = new DateTime(1980, 11, 3), Country = "Canada" },
    //new Author { AuthorId = 4, Name = "TestAuthor4", BirthDate = new DateTime(1992, 3, 8), Country = "Australia" },
    //new Author { AuthorId = 5, Name = "TestAuthor5", BirthDate = new DateTime(1955, 5, 17), Country = "Ireland" },
    //new Author { AuthorId = 6, Name = "TestAuthor6", BirthDate = new DateTime(1985, 8, 24), Country = "New Zealand" },
    //new Author { AuthorId = 7, Name = "TestAuthor7", BirthDate = new DateTime(1970, 12, 13), Country = "South Africa" },
    //new Author { AuthorId = 8, Name = "TestAuthor8", BirthDate = new DateTime(1991, 4, 2), Country = "USA" }
    //        }.AsQueryable();
    //    }

    //    public void Update(Author item)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
    //public class FakeBookRepo : IRepository<Book>
    //{
    //    public void Create(Book item)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Delete(int id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Book Read(int id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IQueryable<Book> ReadAll()
    //    {
    //        return new List<Book>()
    //        {
    //new Book { BookId = 1, Title = "TestBook1", PublicationYear = 2001, Genre = "Mystery", AuthorId = 1 },
    //new Book { BookId = 2, Title = "TestBook2", PublicationYear = 1999, Genre = "Science Fiction", AuthorId = 1 },
    //new Book { BookId = 3, Title = "TestBook3", PublicationYear = 2003, Genre = "Fantasy", AuthorId = 2 },
    //new Book { BookId = 4, Title = "TestBook4", PublicationYear = 2005, Genre = "Adventure", AuthorId = 3 },
    //new Book { BookId = 5, Title = "TestBook5", PublicationYear = 2000, Genre = "Horror", AuthorId = 3 },
    //new Book { BookId = 6, Title = "TestBook6", PublicationYear = 2004, Genre = "Drama", AuthorId = 3 },
    //new Book { BookId = 7, Title = "TestBook7", PublicationYear = 1998, Genre = "Science Fiction", AuthorId = 4 }
    //        }.AsQueryable();
    //    }

    //    public void Update(Book item)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //---------------------------------------------------------------------------
    [TestFixture]
    public class BookLogicTest
    {

        BookLogic bookLogic;
        AuthorLogic authorLogic;
        UserLogic userLogic;
        Mock<IRepository<Book>> mockBookRepo;
        Mock<IRepository<Author>> mockAuthorRepo;
        Mock<IRepository<Loan>> mockLoanRepo;
        Mock<IRepository<User>> mockUserRepo;

        [SetUp]
        public void Init()
        {
            mockBookRepo = new Mock<IRepository<Book>>();
            mockBookRepo.Setup(b => b.ReadAll()).Returns(new List<Book>()
            {
                new Book { BookId = 1, Title = "TestBook1", PublicationYear = 2001, Genre = "Mystery", AuthorId = 1 },
                new Book { BookId = 2, Title = "TestBook2", PublicationYear = 1999, Genre = "Science Fiction", AuthorId = 1 },
                new Book { BookId = 3, Title = "TestBook3", PublicationYear = 2003, Genre = "Fantasy", AuthorId = 2 },
                new Book { BookId = 4, Title = "TestBook4", PublicationYear = 2005, Genre = "Adventure", AuthorId = 3 },
                new Book { BookId = 5, Title = "TestBook5", PublicationYear = 2000, Genre = "Horror", AuthorId = 3 },
                new Book { BookId = 6, Title = "TestBook6", PublicationYear = 2004, Genre = "Drama", AuthorId = 3 },
                new Book { BookId = 7, Title = "TestBook7", PublicationYear = 1998, Genre = "Science Fiction", AuthorId = 4 }
            }.AsQueryable());

            mockAuthorRepo = new Mock<IRepository<Author>>();
            mockAuthorRepo.Setup(a => a.ReadAll()).Returns(new List<Author>()
            {
                new Author { AuthorId = 1, Name = "TestAuthor1", BirthDate = new DateTime(1965, 7, 21), Country = "USA" },
                new Author { AuthorId = 2, Name = "TestAuthor2", BirthDate = new DateTime(1974, 1, 15), Country = "UK" },
                new Author { AuthorId = 3, Name = "TestAuthor3", BirthDate = new DateTime(1980, 11, 3), Country = "Canada" },
                new Author { AuthorId = 4, Name = "TestAuthor4", BirthDate = new DateTime(1992, 3, 8), Country = "Australia" },
                new Author { AuthorId = 5, Name = "TestAuthor5", BirthDate = new DateTime(1955, 5, 17), Country = "Ireland" },
                new Author { AuthorId = 6, Name = "TestAuthor6", BirthDate = new DateTime(1985, 8, 24), Country = "New Zealand" },
                new Author { AuthorId = 7, Name = "TestAuthor7", BirthDate = new DateTime(1970, 12, 13), Country = "South Africa" },
                new Author { AuthorId = 8, Name = "TestAuthor8", BirthDate = new DateTime(1991, 4, 2), Country = "USA" }
            }.AsQueryable());

            mockLoanRepo = new Mock<IRepository<Loan>>();
            mockLoanRepo.Setup(l => l.ReadAll()).Returns(new List<Loan>
            {
                new Loan { LoanId = 1, BookId = 1, LoanDate = new DateTime(2022, 1, 10), ReturnDate = new DateTime(2022, 1, 20), Book = new Book { BookId = 1, Title = "TestBook1", PublicationYear = 2001, Genre = "Mystery" } },
                new Loan { LoanId = 2, BookId = 2, LoanDate = new DateTime(2022, 1, 15), ReturnDate = new DateTime(2022, 1, 25), Book = new Book { BookId = 2, Title = "TestBook2", PublicationYear = 1999, Genre = "Science Fiction" } },
                // További Loan objektumok...
            }.AsQueryable());

            mockUserRepo = new Mock<IRepository<User>>();
            mockUserRepo.Setup(u => u.ReadAll()).Returns(new List<User>()
            {
                new User { UserId = 1, Name = "TestUser1", Email = "testEmail1", Phone = "+36262073389" },
                
            }.AsQueryable());

            bookLogic = new BookLogic(mockBookRepo.Object, mockLoanRepo.Object, mockUserRepo.Object);
            userLogic = new UserLogic(mockUserRepo.Object, mockLoanRepo.Object);
            authorLogic = new AuthorLogic(mockAuthorRepo.Object, mockBookRepo.Object, mockLoanRepo.Object);

        }


        [Test]
        public void GetBooksByAuthorTest()
        {
            // Arrange
            int testAuthorId = 1; // Választott szerző azonosítója
            var expectedBooks = new List<BookDetail>()
        {
            new BookDetail { BookId = 1, Title = "TestBook1", PublicationYear = 2001, Genre = "Mystery" },
            new BookDetail { BookId = 2, Title = "TestBook2", PublicationYear = 1999, Genre = "Science Fiction" }
        };

            // Act
            var actualBooks = bookLogic.GetBooksByAuthor(testAuthorId).ToList();

            // Assert
            Assert.AreEqual(expectedBooks, actualBooks);

        }

        [Test]
        public void GetMostLoanedBooksTest()
        {

            var expectedBooks = new List<BookLoanCount>
            {
                new BookLoanCount()
                {
                    BookId = 1,
                    Title = "TestBook1",
                    LoanCount = 1

                },
                
            };

            // Act
            var actualBooks = bookLogic.GetMostLoanedBooks().ToList();

            // Assert
            Assert.AreEqual(expectedBooks, actualBooks);
            
        }

        [Test]
        public void GetBooksByGenreTest()
        {
            // Arrange
            string testGenre = "Mystery";
            var expectedBooks = new List<BookDetail>
        {
            new BookDetail { BookId = 1, Title = "TestBook1", PublicationYear = 2001, Genre = "Mystery" },
            
        };

            // Act
            var actualBooks = bookLogic.GetBooksByGenre(testGenre).ToList();

            // Assert
            Assert.AreEqual(expectedBooks, actualBooks);

        }
        [Test]
        public void GetBooksLoanedByUserTest()
        {
            // Arrange
            var mockBooks = new List<Book>
        {
            new Book { BookId = 1, Title = "Book 1", AuthorId = 1 },
            new Book { BookId = 2, Title = "Book 2", AuthorId = 2 },
        };
            mockBookRepo.Setup(b => b.ReadAll()).Returns(mockBooks.AsQueryable());

            var mockUsers = new List<User>
        {
            new User { UserId = 1, Name = "User 1" },
            new User { UserId = 2, Name = "User 2" },
        };
            mockUserRepo.Setup(u => u.ReadAll()).Returns(mockUsers.AsQueryable());

            var mockLoans = new List<Loan>
        {
            new Loan { LoanId = 1, UserId = 1, BookId = 1, LoanDate = new DateTime(2022, 1, 10), ReturnDate = new DateTime(2022, 1, 20), Book = mockBooks[0], User = mockUsers[0] },
            new Loan { LoanId = 2, UserId = 1, BookId = 2, LoanDate = new DateTime(2022, 1, 15), ReturnDate = new DateTime(2022, 1, 25), Book = mockBooks[1], User = mockUsers[0] },
        };
            mockLoanRepo.Setup(l => l.ReadAll()).Returns(mockLoans.AsQueryable());

            var expectedUserLoans = new List<UserLoanDetail>
        {
            new UserLoanDetail { BookId = 1, BookTitle = "Book 1", UserName = "User 1", LoanDate = new DateTime(2022, 1, 10), ReturnDate = new DateTime(2022, 1, 20) },
            new UserLoanDetail { BookId = 2, BookTitle = "Book 2", UserName = "User 1", LoanDate = new DateTime(2022, 1, 15), ReturnDate = new DateTime(2022, 1, 25) }
        };

            // Act
            var actualUserLoans = bookLogic.GetBooksLoanedByUser(1).ToList();

            // Assert
            Assert.AreEqual(expectedUserLoans, actualUserLoans);
        }

        [Test]
        public void GetBooksLoanedBetweenDatesTest()
        {
            // Arrange
            var startDate = new DateTime(2022, 1, 10);
            var endDate = new DateTime(2022, 1, 25);
            var expectedBooks = new List<BookDetail>
        {
            new BookDetail { BookId = 1, Title = "TestBook1", PublicationYear = 2001, Genre = "Mystery" },
            new BookDetail { BookId = 2, Title = "TestBook2", PublicationYear = 1999, Genre = "Science Fiction" }
            // Add more expected books if needed
        };

            // Act
            var actualBooks = bookLogic.GetBooksLoanedBetweenDates(startDate, endDate).ToList();

            // Assert
            Assert.AreEqual(expectedBooks, actualBooks);
            
        }

        [Test]
        public void SearchAuthorsByNameTest()
        {
            // Arrange
            string searchName = "TestAuthor1";
            var expectedAuthors = new List<AuthorDetail>
        {
            new AuthorDetail { AuthorId = 1, Name = "TestAuthor1", BirthDate = new DateTime(1965, 7, 21), Country = "USA", BookCount = 2 }
            // További várható szerzők, ha vannak
        };

            // Act
            var actualAuthors = authorLogic.SearchAuthorsByName(searchName).ToList();

            // Assert
            Assert.AreEqual(expectedAuthors, actualAuthors);
            // Additional assertions for author details can be added here
        }

        [Test]
        public void GetMostPopularAuthorsTest()
        {
            // Arrange
            var mockLoans = new List<Loan>
        {
            new Loan { LoanId = 1, Book = new Book { BookId = 1, AuthorId = 1 } },
            new Loan { LoanId = 2, Book = new Book { BookId = 2, AuthorId = 1 } },
            new Loan { LoanId = 3, Book = new Book { BookId = 3, AuthorId = 2 } },
        };
            mockLoanRepo.Setup(l => l.ReadAll()).Returns(mockLoans.AsQueryable());

            var mockAuthors = new List<Author>
        {
            new Author { AuthorId = 1, Name = "Author 1" },
            new Author { AuthorId = 2, Name = "Author 2" },
        };
            mockAuthorRepo.Setup(a => a.ReadAll()).Returns(mockAuthors.AsQueryable());
            mockAuthorRepo.Setup(a => a.Read(It.IsAny<int>())).Returns<int>(id => mockAuthors.FirstOrDefault(a => a.AuthorId == id));

            var expectedAuthors = new List<AuthorPopularity>
        {
            new AuthorPopularity { AuthorId = 1, AuthorName = "Author 1", LoanCount = 2 },
            new AuthorPopularity { AuthorId = 2, AuthorName = "Author 2", LoanCount = 1 }
        };

            // Act
            var actualAuthors = authorLogic.GetMostPopularAuthors().ToList();

            // Assert
            Assert.AreEqual(expectedAuthors, actualAuthors);
        }


        [Test]
        public void GetMostActiveUsersTest()
        {
            // Arrange
            var mockUsers = new List<User>
        {
            new User { UserId = 1, Name = "User 1" },
            new User { UserId = 2, Name = "User 2" },
        };
            mockUserRepo.Setup(u => u.ReadAll()).Returns(mockUsers.AsQueryable());

            var mockLoans = new List<Loan>
        {
            new Loan { LoanId = 1, UserId = 1 },
            new Loan { LoanId = 2, UserId = 1 },
            new Loan { LoanId = 3, UserId = 2 },
        };
            mockLoanRepo.Setup(l => l.ReadAll()).Returns(mockLoans.AsQueryable());

            var expectedUserActivities = new List<UserActivity>
        {
            new UserActivity { UserId = 1, Name = "User 1", LoanCount = 2 },
            new UserActivity { UserId = 2, Name = "User 2", LoanCount = 1 }
        };

            // Act
            var actualUserActivities = userLogic.GetMostActiveUsers().ToList();

            // Assert
            Assert.AreEqual(expectedUserActivities, actualUserActivities);
        }

    }
}
