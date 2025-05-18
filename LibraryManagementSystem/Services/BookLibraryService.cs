using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Utility;

namespace LibraryManagementSystem.Services;

public class BookLibraryService : IBookLibraryService
{
    //List of books will be where the console app will store books in memory
    private List<Book> books = new List<Book>();

    //The id that will be incremented each time a book is added to the library.
    private int newBookId = 1;

    //Method to add new book
    public void AddNewBook(string title, string author, int pubYear)
    {
        books.Add(new Book
        {
            Id = newBookId++,
            Title = title,
            Author = author,
            PublicationYear = pubYear,
            IsCurrentlyBorrowed = false
        });

        Console.WriteLine($"{author}'s book- {title} was added to the Library.");
    }

    //Method to borrow available book
    public void BorrowAvailableBook(int bookId)
    {
        Book? book = books.FirstOrDefault(b => b.Id == bookId);

        if (book != null && book.IsCurrentlyBorrowed)
        {
            Console.WriteLine($"{book.Title} has already been borrowed.");
        }
        else if (book == null)
        {
            Console.WriteLine($"No available book found with ID {bookId}.");
        }
        else
        {
            Console.WriteLine($"You have successfully borrowed: {book.Title}");
            book.IsCurrentlyBorrowed = true;
        }
    }

    //Method to return borrowed book
    public void ReturnBook(int bookId)
    {
        Book? book = books.Find(b => b.Id == bookId);
        if (book == null)
        {
            Console.WriteLine($"Book doesn't exist with Id: {bookId}.");
        }
        else if (!book.IsCurrentlyBorrowed)
        {
            Console.WriteLine($"Book has not been borrowed, book Id is: {bookId}.");
        }
        else
        {
            Console.WriteLine($"You have successfully returned: {book.Title} to the library.");
            book.IsCurrentlyBorrowed = false;
        }
    }

    
    public List<Book> ListAvailableBooks() => books.FindAll(b=>!b.IsCurrentlyBorrowed);
    
    public List<Book> ListBorrowedBooks() => books.FindAll(b => b.IsCurrentlyBorrowed);

    //To Search bookds

    public List<Book> SearchBook(int caseId)
    {
        switch (caseId)
        {
            case 1:
                return SearchBookByTitle(Helper.GetValidatedInput("Enter title to search: "));

            case 2:
                return SearchBookByAuthor(Helper.GetValidatedInput("Enter author to search: "));

            case 3:
                return SearchBookByPublicationYear(Helper.GetValidatedNumber("Enter publication year to search: ",
                    "Year not valid, please try again. "));

            default:
                return [];
        }
    }

    public List<Book> SearchBookByTitle(string title) => books.FindAll(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));

    public List<Book> SearchBookByAuthor(string author) => books.FindAll(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase));

    public List<Book> SearchBookByPublicationYear(int pubYear) => books.FindAll(b => b.PublicationYear == pubYear);
}