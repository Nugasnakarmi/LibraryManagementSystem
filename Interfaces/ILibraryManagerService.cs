using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Interfaces;

public interface ILibraryManagerService
{
    void AddNewBook();
    void ListAvailableBooks();
    void BorrowBook();
    void ReturnBook();
    void SearchBook();
    Task Run();
}
