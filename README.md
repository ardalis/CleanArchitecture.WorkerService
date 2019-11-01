# CleanArchitecture.WorkerService

A solution template using Clean Architecture for building a .NET Core Worker Service.

## Getting Started

This app is currently configured to run against a localdb SQL Server instance. To configure this, you will need to run `dotnet ef database update` before running the app. Check the connection string in `appsettings.json` in the CleanArchitecture.Worker project

Clone or download the repository. Open it in Visual Studio and run it with ctrl-F5 or in the console go to the `src/CleanArchitecture.Worker` folder and run `dotnet run`.

On startup the app queues up 10 URLs to hit (google.com) and you should see it make 10 requests and save them to the database and then do nothing, logging each second.

## Using this for your own worker service

To use this as a template for your own worker server projects, make the following changes:

- Rename CleanArchitecture to YourAppName or YourCompany.YourAppName
- Configure the connection string to your database if you're using one
- Replace InMemory queue implementations with Azure, AWS, Rabbit, etc. actual queues you're using
- Remove UrlStatusHistory and related services and interfaces

