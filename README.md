# aspnetcore3-webapi-authentication-authorization

A simple ASP.NET Core 3.1 example API with JWT (Json Web Token) authentication and role support, using Dependecy Injection, AutoMapper to map entities/models to DTOs and multi-database access with MultiDBHelper library.  
This example uses a SQLite database file with the following example/test user records:

**Table:** Users

**- Username:** employee_user
**- Password:** employee_password
**- Role:** employee

**- Username:** manager_user
**- Password:** manager_password
**- Role:** manager
