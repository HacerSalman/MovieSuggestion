# MovieSuggestion

## Getting Started
The solution contains 5 different projects. They are Api,Job,Data,Core and Test.
Api project contains Rest api endpoints.
Job is a background job application for get all movies from an online movie site.
Data contains entities,enums,context and migrations.
Core contains repositories, unitOfWork, services, clients, DTO, models and utils.
Test contains unit tests for services methods.
To run these projects on your own computer, add the MOVIE_SUGGESTION_DB_CONNECTION, MOVIE_SUGGESTION_JWT_KEY, 
MOVIE_SUGGESTION_VALID_ISSUER, MOVIE_SUGGESTION_VALID_AUDIENCE variables to the environmet variables and add 
the connection string of the mysql database you set on your computer as the value. 
After that write  `dotnet ef database update` command to Package Manager Console Window for generate database tables.

## Architecture
This repository uses Repository Patterns and UnitOfWork. Also uses Interface Segregation and Single Responsibility SOLID principles.
Instead of gathering all responsibilities in a single interface, we used more customized interfaces, 
thus becoming compatible with the Interface Segregation Principle.Thanks to the Repository Pattern, the Single Responsibility 
principle is followed by using a separate method for each process.

## Used technologies
.Net 5, Entity Framework, AutoMapper, Pomelo, Repository Pattern, UnitOfWork, Nunit, Moq, MySql, Hangfire, Jwt, Swagger, Fluent Api


![image](https://user-images.githubusercontent.com/20662989/172023633-ff737813-5944-425c-bd7e-0397e89f819c.png)
