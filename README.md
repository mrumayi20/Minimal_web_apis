## Project creation

dotnet new webapi -o ProjectName

## adding .ignignore for dotnet project

dotnet new gitignore

## Project structure for MVC Pattern

1. Models (Essential)
   This is where your data structures live. Most developers split these into two sub-folders:

   Entities: These match your database tables exactly (e.g., User.cs, Product.cs).

   DTOs (Data Transfer Objects): These are "trimmed down" versions of your models used for API requests and responses (e.g., UserLoginRequest.cs). You don't always want to expose your full database schema to the user!

2. Data (Essential)
   If you're using Entity Framework Core, this folder is where your AppDbContext.cs goes. It handles the connection between your code and the database.

3. Services
   Don't put your business logic (calculations, validation, database calls) directly inside the Controller.

   Controllers should just handle the "traffic" (receiving requests and returning status codes).

   Services handle the "brain work."

   Example: ProductService.cs.

4. Repositories (Optional but Clean)
   If you want to be extra organized, use the Repository Pattern. This sits between your Service and your Database.

   It keeps your data access logic separate so that if you ever switch from SQL Server to MongoDB, you only have to change the code in this folder.
