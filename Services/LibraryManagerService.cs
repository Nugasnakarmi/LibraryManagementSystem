using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Utility;

namespace LibraryManagementSystem.Services;

public class LibraryManagerService : ILibraryManagerService
{
    private IBookLibraryService _bookLibraryService;

    public LibraryManagerService(IBookLibraryService libraryService) => _bookLibraryService = libraryService;

    public void AddNewBook()
    {
        string title = Helper.GetValidatedInput("Enter title for the book:");
        string author = Helper.GetValidatedInput("Enter author of the book:");
        int pubYear = Helper.GetValidatedNumber("Enter the year in which the book was published:",
            "Publication year is not valid. Please try again.");

        _bookLibraryService.AddNewBook(title, author, pubYear);
    }

    public List<Book> ListAvailableBooks()
    {
        List<Book> books = _bookLibraryService.ListAvailableBooks();
        if (books.Count > 0)
        {
            Console.WriteLine("Id | Title | Author | Publication Year");
            foreach (Book b in books)
            {
                Console.WriteLine($"{b.Id} | {b.Title} | {b.Author} | {b.PublicationYear} ");
            }
        }
        else
        {
            Console.WriteLine("No books are available.");
        }
        return books;
    }

    public void ListBorrowedBooks()
    {
        List<Book> borrowedBooks = _bookLibraryService.ListBorrowedBooks();
        if (borrowedBooks.Count > 0)
        {
            Console.WriteLine("Id | Title | Author | Publication Year");
            foreach (Book b in borrowedBooks)
            {
                Console.WriteLine($"{b.Id} | {b.Title} | {b.Author} | {b.PublicationYear}");
            }
        }
        else
        {
            Console.WriteLine("No books have been borrowed.");
        }
    }

    public void BorrowBook()
    {
        
        List<Book> books = _bookLibraryService.ListAvailableBooks();
        if (books.Count > 0)
        {
            int bookId = Helper.GetValidatedNumber("Which book do you want to borrow? Please enter the book id.",
                "Number not valid. Please try again");

            _bookLibraryService.BorrowAvailableBook(bookId);
        }
        else
        {
            Console.WriteLine("No books are available.");
        }
    }

    public void ReturnBook()
    {
        int bookId = Helper.GetValidatedNumber("Please enter the book id that you want to return.", "Number not valid. Please try again");
        _bookLibraryService.ReturnBook(bookId);
    }

    public void SearchBook()
    {
        int searchByCase = Helper.GetValidatedNumber("Do you want to search by: \n1)Title\n2)Author\n3)Publication Year\n",
            "Number not valid. Please try again");
        List<Book> resultBooks = _bookLibraryService.SearchBook(searchByCase);
        if (resultBooks.Count > 0)
        {
            foreach (Book book in resultBooks)
            {
                Console.WriteLine($"{book.Id} | {book.Title} | {book.Author} | {book.PublicationYear}");
            }
        }
        else
        {
            Console.WriteLine($"No books are available as per the search.");
        }
    }

    public void RunLibrary()
    {
        bool isRunning = true;
        //Add few books for library

        _bookLibraryService.AddNewBook("Don Quixote", "Miguel de Cervantes", 2018);
        _bookLibraryService.AddNewBook("Alices adventures in wonderland", "Lewis Carroll", 2017);
        _bookLibraryService.AddNewBook("Moby Dick", "Herman Melville", 1989);
        _bookLibraryService.AddNewBook("The Hobbit, or, There and back again", "J. R. R. Tolkien", 2016);
        _bookLibraryService.AddNewBook("Oliver Twist", "Charles Dickens", 1992);

        while (isRunning)
        {
            //do things
            Console.WriteLine("\nHello, welcome to the Library Management System!");
            Console.WriteLine("1. Add a new book");
            Console.WriteLine("2. Borrow a book");
            Console.WriteLine("3. Return a book");
            Console.WriteLine("4. List available books");
            Console.WriteLine("5. List borrowed books");
            Console.WriteLine("6. Search books");
            Console.WriteLine("7. Exit\n");
            Console.WriteLine("Please enter the number you want: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("Add a new book");
                    AddNewBook();
                    break;

                case "2":
                    Console.WriteLine("Borrow a book from the library");
                    BorrowBook();
                    break;

                case "3":
                    Console.WriteLine("Return book");
                    ReturnBook();
                    break;

                case "4":
                    Console.WriteLine("Books that are available currently:");
                    ListAvailableBooks();
                    break;

                case "5":

                    ListBorrowedBooks();
                    break;

                case "6":
                    SearchBook();
                    break;

                case "7":
                    Console.WriteLine("You will now exit. Please visit the library again.");
                    isRunning = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}