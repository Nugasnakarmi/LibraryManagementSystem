using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Services;

public class BookLibraryService : IBookLibraryService
{
    //List of books will be where the console app will store books in memory
    private List<Book> books = new List<Book>();

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
        Book book = ListBooks().FirstOrDefault(b => b.Id == bookId);

        if (book != null)
        {
            Console.WriteLine($"You have successfully borrowed: {book.Title}");
            book.IsCurrentlyBorrowed = true;
        }
        else
        {
            Console.WriteLine($"No available book found with ID {bookId}.");
        }
    }

    //Method to return borrowed book
    public void ReturnBook(int bookId)
    {
        Book book = books.Find(b => b.IsCurrentlyBorrowed && b.Id == bookId);
        if (book != null)
        {
            Console.WriteLine($"You have successfully returned: {book.Title} to the library.");
            book.IsCurrentlyBorrowed = false;
        }
        else
        {
            Console.WriteLine($"Book doesn't exist or has not been borrowed with Id: {bookId}.");
        }
    }

    //Method to list available books
    public List<Book> ListBooks()
    {
        return books.FindAll(b => !b.IsCurrentlyBorrowed);
    }

    //To Search bookds

    public List<Book> SearchBook(int caseId)
    {
        switch (caseId)
        {
            case 1:
                return SearchBookByTitle(Helper.GetValidatedInput("Enter title to search: "));
                break;

            case 2:
                return SearchBookByAuthor(Helper.GetValidatedInput("Enter author to search: "));
                break;

            case 3:
                return SearchBookByPublicationYear(Helper.GetValidatedNumber("Enter publication year to search: ",
                    "Year not valid, please try again. "));
                break;

            default:
                return [];
                break;
        }
    }

    public List<Book> SearchBookByTitle(string title) => books.FindAll(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));

    public List<Book> SearchBookByAuthor(string author) => books.FindAll(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase));

    public List<Book> SearchBookByPublicationYear(int pubYear) => books.FindAll(b => b.PublicationYear == pubYear);
}