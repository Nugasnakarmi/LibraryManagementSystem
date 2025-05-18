using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

//Adding services to Dependency Injection Container
var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<IBookLibraryService, BookLibraryService>();
        services.AddSingleton<ILibraryManagerService, LibraryManagerService>();
    })
    .Build();

var libraryManager = host.Services.GetRequiredService<ILibraryManagerService>();
libraryManager.RunLibrary();