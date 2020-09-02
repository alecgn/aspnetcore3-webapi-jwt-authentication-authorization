# aspnetcore3-webapi-authentication-authorization
[![Build status (aspnetcore3-webapi-jwt-authentication-authorization)](https://github.com/alecgn/aspnetcore3-webapi-jwt-authentication-authorization/workflows/build/badge.svg)](#)

A simple ASP.NET Core 3.1 example API with JWT (Json Web Token) authentication and role support, using Dependecy Injection, AutoMapper to map entities to DTOs, multi-database access with MultiDBHelper library and CryptHash.Net library to hash/verify user passwords.  
This example uses a SQLite database file with the following example/test user records:

**Table:** Users

**- Username:** employee_user
**- Password (BCrypt hash):** employee_password
**- Role:** employee

**- Username:** manager_user
**- Password (BCrypt hash):** manager_password
**- Role:** manager
