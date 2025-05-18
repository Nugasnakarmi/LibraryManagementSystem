using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystemTests
{
    public class BookLibraryServiceTests
    {
        private readonly IBookLibraryService _bookLibraryService;
        private readonly StringWriter _consoleWriter;
        private readonly TextWriter _originalconsoleOutput;

        public BookLibraryServiceTests()
        {
            _bookLibraryService = new BookLibraryService();

            //Get console output for testing
            _consoleWriter = new StringWriter();
            _originalconsoleOutput = Console.Out;
            Console.SetOut(_consoleWriter);
        }

        public void Dispose()
        {
            //This will restore the original console output
            //This will be called after the test has been completed by xUnit

            Console.SetOut(_originalconsoleOutput);
            _consoleWriter.Dispose();
        }

        [Fact]
        public void AddNewBook_ShouldAddBookToTheLibrary()
        {
            //Arrange
            var initialBookCount = _bookLibraryService.ListAvailableBooks().Count;

            //Act
            _bookLibraryService.AddNewBook("How to Add A New Book", "Tester Man", 2025);

            //Assert

            List<Book> books = _bookLibraryService.ListAvailableBooks();
            Assert.Equal(initialBookCount + 1, books.Count);
            Assert.Contains(books, b =>
                b.Title == "How to Add A New Book" &&
                b.Author == "Tester Man" &&
                b.PublicationYear == 2025);
        }

        [Fact]
        public void BorrowBook_ShowsBookIsBorrowed()
        {
            //Arrange
            _bookLibraryService.AddNewBook("Test Borrow of Book", "Tester Man", 2025);

            int bookIdToBeBorrowed = 1;
            //Act
            _bookLibraryService.BorrowAvailableBook(bookIdToBeBorrowed);

            List<Book> borrowedBooks = _bookLibraryService.ListBorrowedBooks();

            //Assert
            Assert.Contains(borrowedBooks, b => b.Title == "Test Borrow of Book"
            && b.Author == "Tester Man"
            && b.PublicationYear == 2025);
        }

        [Fact]
        public void ReturnBook_ShouldMarkBorrowedBookAsReturned()
        {
            //Arrange
            _bookLibraryService.AddNewBook("Test Return of Borrowed Book", "Tester Man", 2025);
            int bookIdToBeBorrowed = 1;
            //Act
            _bookLibraryService.BorrowAvailableBook(bookIdToBeBorrowed);
            _bookLibraryService.ReturnBook(bookIdToBeBorrowed);

            List<Book> availableBooks = _bookLibraryService.ListAvailableBooks();

            //Assert - available books consist of the book that was borrowed and returned.
            Assert.Contains(availableBooks, b => b.Title == "Test Return of Borrowed Book"
            && b.Author == "Tester Man"
            && b.PublicationYear == 2025);
        }

        [Fact]
        public void ReturnBook_ShouldNotReturnNonExistentBook()
        {
            // Act
            int bookId = 999;
            _bookLibraryService.ReturnBook(bookId);

            // Assert
            string output = _consoleWriter.ToString();
            Assert.Contains("Book doesn't exist with Id: 999.", output);
        }

        [Fact]
        public void ReturnBook_ShouldHandleUnborrowedBook()
        {
            //Arrange
            _bookLibraryService.AddNewBook("Test Return of Borrowed Book", "Tester Man", 2025);
            int bookId = 1;

            // Act
            _bookLibraryService.ReturnBook(bookId);

            // Assert
            string output = _consoleWriter.ToString();
            Assert.Contains("Book has not been borrowed, book Id is: 1.", output);
        }

        [Fact]
        public void SearchBookByTitle_ShouldReturnListOfBooks()
        {
            //Arrange
            _bookLibraryService.AddNewBook("Test One Book", "Tester Man", 2025);
            _bookLibraryService.AddNewBook("Test Another Book", "Tester Man", 2025);
            //Act
            string searchTitle = "One";
            List<Book> resultBooks = _bookLibraryService.SearchBookByTitle(searchTitle);

            //Assert
            Assert.Single(resultBooks);
            Assert.Equal("Test One Book", resultBooks[0].Title);

            string searchTitle2 = "Another";
            List<Book> resultBooks2 = _bookLibraryService.SearchBookByTitle(searchTitle2);

            //Assert
            Assert.Single(resultBooks2);
            Assert.Equal("Test Another Book", resultBooks2[0].Title);
        }

        [Fact]
        public void SearchBookByAuthor_ShouldReturnListOfBooks()
        {
            //Arrange
            _bookLibraryService.AddNewBook("Test One Book", "Elon Musk", 2025);
            _bookLibraryService.AddNewBook("Test Another Book", "Mark Zuckerburg", 2025);
            //Act
            string author1 = "Elon";
            List<Book> resultBooks = _bookLibraryService.SearchBookByAuthor(author1);

            //Assert
            Assert.Single(resultBooks);
            Assert.Equal("Test One Book", resultBooks[0].Title);

            string author2 = "Mark";
            List<Book> resultBooks2 = _bookLibraryService.SearchBookByAuthor(author2);

            //Assert
            Assert.Single(resultBooks2);
            Assert.Equal("Test Another Book", resultBooks2[0].Title);
        }

        [Fact]
        public void SearchBookByPublicationYear_ShouldReturnListOfBooks()
        {
            //Arrange
            _bookLibraryService.AddNewBook("Test One Book", "Tester Man", 1920);
            _bookLibraryService.AddNewBook("Test Another Book", "Tester Man", 2020);
            //Act
            int pubYear1 = 1920;
            List<Book> resultBooks = _bookLibraryService.SearchBookByPublicationYear(pubYear1);

            //Assert
            Assert.Single(resultBooks);
            Assert.Equal("Test One Book", resultBooks[0].Title);

            int pubYear2 = 2020;
            List<Book> resultBooks2 = _bookLibraryService.SearchBookByPublicationYear(pubYear2);

            //Assert
            Assert.Single(resultBooks2);
            Assert.Equal("Test Another Book", resultBooks2[0].Title);
        }

        [Fact]
        public void ListAvailableBooks_ReturnsOnlyAvailableBooks()
        {
            // Arrange
            _bookLibraryService.AddNewBook("Test One Book", "Tester Man", 2025);
            _bookLibraryService.AddNewBook("Test Another Book", "Tester Man", 2025);

            //Act
            int bookId = _bookLibraryService.ListAvailableBooks()[1].Id;
            _bookLibraryService.BorrowAvailableBook(bookId); //Borrow the second book

            List<Book> availableBooks = _bookLibraryService.ListAvailableBooks();

            //Assert
            Assert.DoesNotContain(availableBooks, b => b.Title == "Test Another Book");
        }

        [Fact]
        public void ListBorrowedBooks_ReturnsOnlyBorrowedBooks()
        {
            // Arrange
            _bookLibraryService.AddNewBook("Test One Book", "Tester Man", 2025);
            _bookLibraryService.AddNewBook("Test Another Book", "Tester Man", 2025);

            //Act
            int bookId = _bookLibraryService.ListAvailableBooks()[0].Id;
            _bookLibraryService.BorrowAvailableBook(bookId); //Borrow the first book

            List<Book> borrowedBooks = _bookLibraryService.ListBorrowedBooks();

            //Assert
            Assert.DoesNotContain(borrowedBooks, b => b.Title == "Test Another Book");
        }
    }
}