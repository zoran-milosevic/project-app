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
* Create subfolder named AuthService under the backend folder and change directory to AuthService
* Add project's solution file:
```
dotnet new sln --name AuthService
```
* Lists all project templates
```
dotnet new --list
```
* Add new project:
```
dotnet new webapi --auth None -lang "C#" -o AuthService.API
```
* Lists all projects in a solution file:
```
dotnet sln .\AuthService.sln list
```
* Add class library for Service, Data, Model, Common, Interface
```
dotnet new classlib -lang "C#" -f net6.0 -o AuthService.Service
dotnet new classlib -lang "C#" -f net6.0 -o AuthService.Data
dotnet new classlib -lang "C#" -f net6.0 -o AuthService.Model
dotnet new classlib -lang "C#" -f net6.0 -o AuthService.Interface
dotnet new classlib -lang "C#" -f net6.0 -o AuthService.Configuration
```
* Add a projects to the solution file:
```
dotnet sln add .\AuthService.API\AuthService.API.csproj
dotnet sln add .\AuthService.Service\AuthService.Service.csproj
dotnet sln add .\AuthService.Data\AuthService.Data.csproj
dotnet sln add .\AuthService.Model\AuthService.Model.csproj
dotnet sln add .\AuthService.Interface\AuthService.Interface.csproj
dotnet sln add .\AuthService.Configuration\AuthService.Configuration.csproj
```
* Add a project's references
```
dotnet add .\AuthService.API\AuthService.API.csproj reference .\AuthService.Service\AuthService.Service.csproj
dotnet add .\AuthService.API\AuthService.API.csproj reference .\AuthService.Model\AuthService.Model.csproj
dotnet add .\AuthService.API\AuthService.API.csproj reference .\AuthService.Interface\AuthService.Interface.csproj
dotnet add .\AuthService.API\AuthService.API.csproj reference .\AuthService.Configuration\AuthService.Configuration.csproj
dotnet add .\AuthService.Interface\AuthService.Interface.csproj reference .\AuthService.Model\AuthService.Model.csproj
dotnet add .\AuthService.Service\AuthService.Service.csproj reference .\AuthService.Interface\AuthService.Interface.csproj
dotnet add .\AuthService.Service\AuthService.Service.csproj reference .\AuthService.Model\AuthService.Model.csproj
dotnet add .\AuthService.Service\AuthService.Service.csproj reference .\AuthService.Data\AuthService.Data.csproj
dotnet add .\AuthService.Data\AuthService.Data.csproj reference .\AuthService.Interface\AuthService.Interface.csproj
dotnet add .\AuthService.Configuration\AuthService.Configuration.csproj reference .\AuthService.Model\AuthService.Model.csproj
dotnet add .\AuthService.Configuration\AuthService.Configuration.csproj reference .\AuthService.Data\AuthService.Data.csproj
```

* Reload VS Code Window if you re missing an using directive or an assembly reference - Open the command palette ( Ctrl + Shift + P ) and execute the command: >Reload Window.
* Change dir to AuthService.API and run project:
```
dotnet run --launch-profile "Dev"
```

### CLEAN UP AND INSTALL AuthService.API PROJECT PACKAGE DEPENDENCIES
```
dotnet add package NLog.Web.AspNetCore
dotnet add package Microsoft.Extensions.Logging.Debug
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package FluentValidation
```

### INSTALL AuthService.Service PROJECT PACKAGE DEPENDENCIES
```
dotnet add package NLog.Extensions.Logging
dotnet add package System.IdentityModel.Tokens.Jwt
```

### INSTALL AuthService.Data PROJECT PACKAGE DEPENDENCIES
```
dotnet add package NLog.Extensions.Logging
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Relational
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design
```

### INSTALL AuthService.Model PROJECT PACKAGE DEPENDENCIES
```
dotnet add package Microsoft.Extensions.Identity.Stores
```

### INSTALL AuthService.Configuration PROJECT PACKAGE DEPENDENCIES
```
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
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
* Change to working directory .\AuthService.Data and add Migrations for AuthService.Data project
```
dotnet ef migrations add Initial_Auth_Database --context AuthDbContext --output-dir Migrations/Auth
```
* Create your database and schema.The connection string to the database is defaults to the one specified in AddDbContext or OnConfiguring.
```
dotnet ef database update Initial_Auth_Database --context AuthDbContext
```
* Generate an Update SQL Script from Migrations
```
dotnet ef migrations script --context AuthDbContext --output Migrations\Auth\Scripts\Database_2021_04_09_1200.sql
```

### INTEGRATION TESTS

What is the difference between unit tests and integration tests? For unit tests, we simulate all dependencies for Web API project and for integration tests, we run a process that simulates Web API execution, this means Http requests.

Rather than publishing an API and call it from integration test, we basically create local version to test

Integration tests will perform Http requests, each Http request will perform operations to an existing database in SQL Server instance. We'll work with a local instance of SQL Server, this can change according to your working environment, I mean the scope for integration tests.

* In order to add integration tests for API project, follow next steps.
* Remove UnitTest1.cs file
* Create a xUnit test project for AuthService.API
```
dotnet new xunit -lang "C#" -f net6.0 -o AuthService.API.IntegrationTest
dotnet sln add .\AuthService.API.IntegrationTest\AuthService.API.IntegrationTest.csproj
dotnet add .\AuthService.API.IntegrationTest\AuthService.API.IntegrationTest.csproj reference .\AuthService.API\AuthService.API.csproj
```

* Add the following NuGet packages for the project:
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