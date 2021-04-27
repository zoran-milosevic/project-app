# VISUAL STUDIO CODE EXTENSIONS
* C# OmniSharp
* Debugger for Chrome
* Visual Studio IntelliCode
* vscode-icons
* Bracket Pair Colorizer 2
* Path Intellisense
* Partial Diff
* Markdown Preview Enhanced
* .NET Core Test Explorer

# PROJECT CONFIGURATION #
Step by step guide to get your application up and running from scratch.

### CREATE AN EMPTY REPOSITORY ###
* Create folder named 'project-app' in your local repository
* Create an empty git repository:
```
git init
```
* Create subfolders for 'backend' and 'frontend'
* Create subfolder named ProjectApp under the backend folder and change directory to ProjectApp
* Add project's solution file:
```
dotnet new sln --name ProjectApp
```
* Lists all project templates
```
dotnet new --list
```
* Add new project:
```
dotnet new webapi --auth None -lang "C#" -o ProjectApp.API
```
* Lists all projects in a solution file:
```
dotnet sln .\ProjectApp.sln list
```
* Add class library for Service, Data, Model, Common, Interface
```
dotnet new classlib -lang "C#" -f net5.0 -o ProjectApp.Service
dotnet new classlib -lang "C#" -f net5.0 -o ProjectApp.Data
dotnet new classlib -lang "C#" -f net5.0 -o ProjectApp.Model
dotnet new classlib -lang "C#" -f net5.0 -o ProjectApp.Common
dotnet new classlib -lang "C#" -f net5.0 -o ProjectApp.Interface
```
* Add a projects to the solution file:
```
dotnet sln add .\ProjectApp.API\ProjectApp.API.csproj
dotnet sln add .\ProjectApp.Service\ProjectApp.Service.csproj
dotnet sln add .\ProjectApp.Data\ProjectApp.Data.csproj
dotnet sln add .\ProjectApp.Model\ProjectApp.Model.csproj
dotnet sln add .\ProjectApp.Common\ProjectApp.Common.csproj
dotnet sln add .\ProjectApp.Interface\ProjectApp.Interface.csproj
```
* Add a project's references
```
dotnet add .\ProjectApp.API\ProjectApp.API.csproj reference .\ProjectApp.Service\ProjectApp.Service.csproj
dotnet add .\ProjectApp.API\ProjectApp.API.csproj reference .\ProjectApp.Data\ProjectApp.Data.csproj
dotnet add .\ProjectApp.API\ProjectApp.API.csproj reference .\ProjectApp.Model\ProjectApp.Model.csproj
dotnet add .\ProjectApp.API\ProjectApp.API.csproj reference .\ProjectApp.Interface\ProjectApp.Interface.csproj
dotnet add .\ProjectApp.Service\ProjectApp.Service.csproj reference .\ProjectApp.Data\ProjectApp.Data.csproj
dotnet add .\ProjectApp.Service\ProjectApp.Service.csproj reference .\ProjectApp.Model\ProjectApp.Model.csproj
dotnet add .\ProjectApp.Service\ProjectApp.Service.csproj reference .\ProjectApp.Common\ProjectApp.Common.csproj
dotnet add .\ProjectApp.Service\ProjectApp.Service.csproj reference .\ProjectApp.Interface\ProjectApp.Interface.csproj
dotnet add .\ProjectApp.Data\ProjectApp.Data.csproj reference .\ProjectApp.Model\ProjectApp.Model.csproj
dotnet add .\ProjectApp.Data\ProjectApp.Data.csproj reference .\ProjectApp.Common\ProjectApp.Common.csproj
dotnet add .\ProjectApp.Data\ProjectApp.Data.csproj reference .\ProjectApp.Interface\ProjectApp.Interface.csproj
dotnet add .\ProjectApp.Interface\ProjectApp.Interface.csproj reference .\ProjectApp.Model\ProjectApp.Model.csproj
dotnet add .\ProjectApp.Interface\ProjectApp.Interface.csproj reference .\ProjectApp.Common\ProjectApp.Common.csproj
```
* Add package for ProjectApp.Service, ProjectApp.Data class libraries
```
dotnet add package NLog.Extensions.Logging
```
* Reload VS Code Window if you re missing an using directive or an assembly reference - Open the command palette ( Ctrl + Shift + P ) and execute the command: >Reload Window. Delete the "C:\Program Files\dotnet\sdk\NuGetFallbackFolder" which fixed the issue.
* Change dir to ProjectApp.API and run project:
```
dotnet run
```

### CLEAN UP AND INSTALL ProjectApp.API PROJECT PACKAGE DEPENDENCIES
```
dotnet add package NLog.Web.AspNetCore
dotnet add package Microsoft.Extensions.Logging.Debug
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package AutoMapper
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet add package FluentValidation
```

### INSTALL ProjectApp.Service PROJECT PACKAGE DEPENDENCIES
```
dotnet add package System.IdentityModel.Tokens.Jwt
dotnet add package NLog.Extensions.Logging
```

### INSTALL ProjectApp.Data PROJECT PACKAGE DEPENDENCIES
```
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Relational
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package NLog.Extensions.Logging
```

### ENTITY FRAMEWORK CORE TOOLS
* The command-line interface (CLI) tools for Entity Framework Core perform design-time development tasks. create migrations, apply migrations, and generate code for a model based on an existing database. Dotnet EF can be installed as either a global or local tool.
```
dotnet tool install --global dotnet-ef
```
* Verify installation
```
dotnet ef
```
* Change to working directory .\ProjectApp.Data and add Migrations for ProjectApp.Data project
```
dotnet ef migrations add Initial_App_Database --context AppDbContext --output-dir Migrations/App
```
* Create your database and schema.The connection string to the database is defaults to the one specified in AddDbContext or OnConfiguring.
```
dotnet ef database update Initial_App_Database --context AppDbContext
```
* Generate an Update SQL Script from Migrations
```
dotnet ef migrations script --context AppDbContext --output Migrations\App\Scripts\Database_2021_03_08_1200.sql
```

### INTEGRATION TESTS

What is the difference between unit tests and integration tests? For unit tests, we simulate all dependencies for Web API project and for integration tests, we run a process that simulates Web API execution, this means Http requests.

Rather than publishing an API and call it from integration test, we basically create local version to test

Integration tests will perform Http requests, each Http request will perform operations to an existing database in SQL Server instance. We'll work with a local instance of SQL Server, this can change according to your working environment, I mean the scope for integration tests.

* In order to add integration tests for API project, follow next steps.
* Remove UnitTest1.cs file
* Create a xUnit test project for ProjectApp.API
```
dotnet new xunit -lang "C#" -f net5.0 -o ProjectApp.API.IntegrationTest
dotnet sln add .\ProjectApp.API.IntegrationTest\ProjectApp.API.IntegrationTest.csproj
dotnet add .\ProjectApp.API.IntegrationTest\ProjectApp.API.IntegrationTest.csproj reference .\ProjectApp.API\ProjectApp.API.csproj
```

* Add the following NuGet packages for project:
```
dotnet add package Microsoft.AspNetCore.Mvc
dotnet add package Microsoft.AspNetCore.Diagnostics
dotnet add package Microsoft.AspNetCore.TestHost
dotnet add package Microsoft.AspNetCore.Mvc.Core
dotnet add package Microsoft.Extensions.Configuration.Json
```

* Add Visual Studio Code extension .NET Core Test Explorer
* Open a .NET Core test project in VS Code
* In .NET Test Explorer of Explorer view, all the tests will be automatically detected, and you are able to run all tests or a certain test

* The process for integration tests is:
    1. The Http client in created in class constructor
    2. Define the request: url and request model (if applies)
    3. Send the request
    4. Get the value from response
    5. Ensure response has success status

To execute integration tests, you need to have running an instance of SQL Server, the connection string in appsettings.Development.json file will be used to establish connection with SQL Server.

If you get any error executing integration tests, check the error message, review code and repeat the process.

### UNIT TESTS

What is TDD? Testing is a common practice in nowadays, because with unit tests, it's easy to performing tests for features before to publish, Test Driven Development (TDD) is the way to define unit tests and validate the behavior in code.

Another concept in TDD is AAA: Arrange, Act and Assert; Arrange is the block for creation of objects, Act is the block to place all invocations for methods and Assert is the block to validate the results from methods invocation.

Since We're working with In memory database for unit tests, We need to create a class to mock UserProfileDbContext class and also add data to perform testing for UserProfileDbContext extension methods.

To be clear: these unit tests do not establish a connection with SQL Server.

* Create a xUnit test project for ProjectApp.Data.Test, add a projects to the solution file and add project's reference
```
dotnet new xunit -lang "C#" -f net5.0 -o ProjectApp.Data.Test
dotnet sln add .\ProjectApp.Data.Test\ProjectApp.Data.Test.csproj
dotnet add .\ProjectApp.Data.Test\ProjectApp.Data.Test.csproj reference .\ProjectApp.Data\ProjectApp.Data.csproj
dotnet add .\ProjectApp.Data.Test\ProjectApp.Data.Test.csproj reference .\ProjectApp.Model\ProjectApp.Model.csproj
```

* Add the following NuGet packages for project:
```
dotnet add package Microsoft.AspNetCore.Mvc.Core
dotnet add package Microsoft.EntityFrameworkCore.InMemory
```

* Remove UnitTest1.cs file

Now we proceed to add code related for unit tests, these tests will work with In memory database. Here we unit test isolated domain models, integration test the rest. Unit test is decoupled from the implementation details of controllers and business logic.

How Unit Tests Work?

DbContextMocker creates an instance of AppDbContext using in memory database, the dbName parameter sets the name for in memory database; then there is an invocation for Seed method, this method adds entities for AppDbContext instance in order to provide results.

DbContextExtensions class contains Seed extension method.

UserProfileRepositoryUnitTest class contains all tests for UserProfileRepository class.

Keep in mind each test uses a different in memory database, inside of each test method. We retrieve in memory database using the name of test method with nameof operator.

At this level (Unit tests), we only need to check the operations for repositories.

* The process for unit tests is:
    1. Create an instance of DbContext
    2. Create an instance of repository
    3. Invoke repository's method
    4. Get value from repository's invocation
    5. Dispose DbContext instance
    6. Validate response

Run all tests using test explorer, if you get any error, check the error message, review code and repeat the process.