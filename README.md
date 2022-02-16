# Task Tracker

This is an ASP.NET Core Web Api app that provides Rest API for task tracking. Api documentation generates using Swagger, more info can be found in [Usage](#usage).

## Technologies used

1. .NET 5
2. C# 9
3. ASP.NET Core 5 Web Api
4. Entity Framework Core 5 with Code-First approach
5. Swagger 3

## Prerequisites

<!-- 1. [IIS](https://docs.microsoft.com/en-us/iis/get-started/whats-new-in-iis-10-version-1709/new-features-introduced-in-iis-10-1709) or [IIS Express](https://docs.microsoft.com/en-us/iis/extensions/introduction-to-iis-express/iis-express-overview) -->
1. [MSSQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
2. [.NET 5 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)

## Building and running

1. Add MSSQL server name and credentials if needed to the database connection string in [appsettings.json](/src/TaskTracker/appsettings.json):
```json
"ConnectionStrings": {
    "TaskTrackerDb": "Server=<your_mssql_server_name>;Database=TaskTracker;Trusted_Connection=true"
}
```
2. Install the `dotnet-ef` tool to use migrations for database creation and modification
   Globally:
   ```bash
   dotnet tool install --global dotnet-ef --version 5.0.14
   ```
   Or locally, in the solution folder:
   ```bash
   dotnet new tool-manifest
   dotnet tool install --global dotnet-ef --version 5.0.14
   ```
3. Apply migrations to the database, this command will also build the project:
```bash
dotnet ef database update --project src/TaskTracker/TaskTracker.csproj
```
4. Relocate to [/src/TaskTracker](/src/TaskTracker):
```bash
cd src/TaskTracker
```
5. Run the app:
```bash
dotnet run --no-build
```

## Usage

When the app is running, http://localhost:5000/swagger/index.html can be opened to explore swagger api documentation and to try api methods.

## License

MIT License

Copyright (c) 2022 Andrey Plekhov

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
