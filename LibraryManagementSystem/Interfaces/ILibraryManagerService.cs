using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Interfaces;

public interface ILibraryManagerService
{
    void AddNewBook();
    List<Book> ListAvailableBooks();
    void ListBorrowedBooks();
    void BorrowBook();
    void ReturnBook();
    void SearchBook();
    void RunLibrary();
}
