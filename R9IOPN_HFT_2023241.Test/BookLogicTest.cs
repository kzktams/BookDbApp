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
                new Loan { LoanId = 3, BookId = 1, LoanDate = new DateTime(2022,1,1), ReturnDate = new DateTime(2023,1,1)}
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
                    LoanCount = 2

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

        //CREATE-TESTS--------------------------------------------------------------------
        [Test]
        public void CreateBookWithCorrect()
        {
            // Arrange
            
            var newBook = new Book
            {
                BookId = 1,
                Title = "Valid Book",
                PublicationYear = DateTime.Now.Year,
                Genre = "Mystery"
            };

            // Act
            bookLogic.Create(newBook);

            // Assert
            mockBookRepo.Verify(b => b.Create(newBook), Times.Once);
        }
        [Test]
        public void CreateBookWithInCorrect()
        {
            // Arrange
            var newBook = new Book
            {
                BookId = 1,
                Title = "Valid Book",
                PublicationYear = DateTime.Now.Year,
                Genre = "p"
            };
            try
            {
            // Act
            bookLogic.Create(newBook);
            }
            catch
            {
            }
            

            // Assert
            mockBookRepo.Verify(b => b.Create(newBook), Times.Never);
        }


        [Test]
        public void CreateAuthorWithCorrect()
        {
            // Arrange

            var newAuthor = new Author
            {
                Name = "test",
                BirthDate = new DateTime(1933,1,1)
            };

            // Act
            authorLogic.Create(newAuthor);

            // Assert
            mockAuthorRepo.Verify(b => b.Create(newAuthor), Times.Once);
        }
        [Test]
        public void CreateAuthorWithInCorrect()
        {
            // Arrange
            var newAuthor = new Author
            {
                Name = "",
                BirthDate = new DateTime(2024,1,1)
            };
            try
            {
                // Act
                authorLogic.Create(newAuthor);
            }
            catch
            {
            }


            // Assert
            mockAuthorRepo.Verify(b => b.Create(newAuthor), Times.Never);
        }

        //DELETE--------------------
        [Test]
        public void DeleteBookWhenLoaned()
        {
            var loans = new List<Loan>
            {
            new Loan { BookId = 2, ReturnDate = DateTime.Now.AddDays(1) } 
            };
            mockLoanRepo.Setup(l => l.ReadAll()).Returns(loans.AsQueryable());

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => bookLogic.Delete(2));
            Assert.That(ex.Message, Is.EqualTo("The book is currently under loan, cannot be deleted"));

        }
        [Test]
        public void DeleteBookWhenCorrect()
        {
            bookLogic.Delete(2);
            mockBookRepo.Verify(l => l.Delete(2), Times.Once);
        }

    }
}
