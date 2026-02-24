## Project creation

dotnet new webapi -o ProjectName

## Adding .ignignore for dotnet project

dotnet new gitignore

## Project structure for MVC Pattern

1. Controllers:
   This is the entry point. It handles HTTP stuff(URLs, status codes like 200 OK or 404 Not Found).
   It never talks to the database. It only talks to the Service.

2. Models (Essential)
   This is where your data structures live. Most developers split these into two sub-folders:

   Entities: These match your database tables exactly (e.g., User.cs, Product.cs).

   DTOs (Data Transfer Objects): These are "trimmed down" versions of your models used for API requests and responses. You don't always want to expose your full database schema to the user!

3. Data (Essential)
   If you're using Entity Framework Core, this folder is where your AppDbContext.cs goes. It handles the connection between your code and the database.

4. Services
   Don't put your business logic (calculations, validation, database calls) directly inside the Controller. Controllers should just handle the "traffic" (receiving requests and returning status codes).

   Services handle the "brain work." This is where the logic happens. It connects the Controller and the Repo.

   It translates DTOs into Entities (and vice versa).

   Simple Logic: "I’ll take this DTO from the controller, turn it into a full Product Entity, and ask the Repo to save it."

5. Repositories (Optional but Clean)
   This is the only place that actually touches the data(your database).

   It hides how the data is stored. The rest of the app doesn't care if you're using a List or a SQL Server.

## Why did we do this?

By setting it up this way, your code is Decoupled. If you want to change your database tomorrow, you only change the Repository. If you want to change your business rules, you only change the Service. Your Controller stays clean and tiny!

## Configure Entity Framework Core

1. `dotnet add package` defaults to the latest version available on NuGet. Since I am on .NET 9.0.6, those packages are incompatible. To fix this, you need to explicitly tell NuGet to install the 9.x versions that match my framework.

```
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 9.0.0
dotnet add package Microsoft.EntityFrameworkCore.Design --version 9.0.0
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 9.0.0
```

2. AppDbContext.cs
   This class in Data folder is the bridge between my application and SQL Server.

3. Configure the Connection String in appsettings.json

4. Register the AppDbContext in your program.cs so your app knows to use SQL Server.

5. Update your Repository

6. Create the Database (Migrations)

   Create the script: dotnet ef migrations add InitialCreate
   Push to SQL Server: dotnet ef database update
