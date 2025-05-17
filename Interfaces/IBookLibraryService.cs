using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Interfaces;

public interface IBookLibraryService
{
    void AddNewBook(string title, string author, int pubYear);

    void BorrowAvailableBook(int bookId);

    void ReturnBook(int bookId);

    List<Book> ListBooks();

    List<Book> SearchBook(int caseId);

    public List<Book> SearchBookByTitle(string title);

    public List<Book> SearchBookByAuthor(string title);

    public List<Book> SearchBookByPublicationYear(int pubYear);
}