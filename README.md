# CleanArchitecture.WorkerService

A solution template using Clean Architecture for building a .NET Core 3.1 Worker Service.

## Give a Star! :star:

If you like or are using this project to learn or start your solution, please give it a star. Thanks!

## Credits

Big thanks to [all of the great contributors to this project](https://github.com/ardalis/CleanArchitecture.WorkerService/graphs/contributors)!

## Getting Started

Install the ef core cli tools `dotnet tool install --global dotnet-ef`. If you already have an old version, first try `dotnet tool update --global dotnet-ef  --version 3.1.0-*`, if that doesn't work, see [Updating Ef Core Cli](https://github.com/aspnet/EntityFrameworkCore/issues/14016#issuecomment-487308603) First, delete C:\Users\{yourUser}\.dotnet\tools\.store\dotnet-ef tool.

This app is currently configured to run against a localdb SQL Server instance. To configure this, you will need to run `dotnet ef database update` in the src\CleanArchitecture.Worker folder before running the app. 

Check the connection string in `appsettings.json` in the CleanArchitecture.Worker project

Clone or download the repository. Open it in Visual Studio and run it with ctrl-F5 or in the console go to the `src/CleanArchitecture.Worker` folder and run `dotnet run`.

On startup the app queues up 10 URLs to hit (google.com) and you should see it make 10 requests and save them to the database and then do nothing, logging each second.

## Using this for your own worker service

To use this as a template for your own worker server projects, make the following changes:

- Rename CleanArchitecture to YourAppName or YourCompany.YourAppName
- Configure the connection string to your database if you're using one
- Replace InMemory queue implementations with Azure, AWS, Rabbit, etc. actual queues you're using
- Remove UrlStatusHistory and related services and interfaces

## References

- [Clean Architecture template for ASP.NET Core solutions](https://github.com/ardalis/CleanArchitecture)
- [Creating a Clean Architecture Worker Service Template](https://www.youtube.com/watch?v=_jfnnAMNb94) ([Twitch](https://twitch.tv/ardalis) Stream 1)
- [Creating a Clean Architecture Worker Service Template](https://www.youtube.com/watch?v=Nttt33GoTXg) ([Twitch](https://twitch.tv/ardalis) Stream 2)

Useful Pluralsight courses:
- [SOLID Principles of Object Oriented Design](https://www.pluralsight.com/courses/principles-oo-design)
- [SOLID Principles for C# Developers](https://www.pluralsight.com/courses/csharp-solid-principles)
- [Domain-Driven Design Fundamentals](https://www.pluralsight.com/courses/domain-driven-design-fundamentals)
