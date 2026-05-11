
# EduLink - Learning Management System (LMS)

EduLink is a robust, scalable Backend system built with **ASP.NET Core 8**, focusing on modern software engineering practices. 
The project is designed using **Clean Architecture** principles to ensure maintainability, testability, and a clear separation of concerns.

## 🚀 Key Features
- **User & Academy Management**: Comprehensive API for managing educational entities and user roles.
- **Clean Architecture**: Strictly decoupled layers (Domain, Application, Infrastructure, and API).
- **CQRS Pattern**: Implementation of Command Query Responsibility Segregation using **MediatR**.
- **Advanced Logging**: Structured logging implemented with **Serilog**, supporting both Console and File sinks.
- **Security**: JWT-based authentication and authorization for secure API access.
- **Unit Testing**: Dedicated test projects to ensure business logic reliability and API stability.
- **Global Error Handling**: Centralized middleware for consistent and professional API error responses.

## 🛠️ Tech Stack
- **Framework**: .NET 8.0 (Web API)
- **Database**: SQL Server / Entity Framework Core
- **Patterns**: Repository Pattern, CQRS, Dependency Injection, Options Pattern
- **Libraries**: MediatR, FluentValidation, Serilog, AutoMapper
- **DevOps**: Docker ready (Dockerfile configuration included)

## 📁 Project Structure
- **EduLink.Domain**: Enterprise logic, Entities, and Value Objects.
- **EduLink.Application**: Application logic, DTOs, Mapping, and CQRS Handlers.
- **EduLink.Infrastructure**: Persistence (EF Core), External Services, and Migrations.
- **EduLink.Api**: Presentation layer, Controllers, and Middleware.
- **EduLink.Api.Test**: Automated tests for ensuring project reliability.
