# Clean Architecture Project - Gym Management

This project provides an example of implementing Clean Architecture principles in a .NET-based Gym Management application. It covers core architectural concepts and patterns, aiming to separate concerns across multiple layers and improve modularity, testability, and maintainability.

## Table of Contents

- [Overview](#overview)
- [Project Structure](#project-structure)
- [Setup and Installation](#setup-and-installation)
- [Usage](#usage)
- [Testing](#testing)

## Overview

This solution follows a Clean Architecture approach, using .NET technologies and a layered architecture that separates responsibilities across the Presentation, Application, Domain, and Infrastructure layers.

The code structure showcases key design patterns, including CQRS, Mediator, Repository, and Unit of Work, which facilitate a scalable and decoupled architecture. It also illustrates how domain events and eventual consistency can be integrated into a project following Clean Architecture, as well as how to apply authorization, authentication, and testing techniques. This README provides a comprehensive guide to the project structure and its design.

## Project Structure

### Root Directory

- **`GymManagement.sln`**: The main solution file.
- **`requests/`**: Contains `.http` files to test API endpoints.
- **`src/`**: Source code for each layer, organized by functionality.
- **`tests/`**: Test projects for validation and functionality assurance.

### Layered Architecture

#### Presentation Layer

- `GymManagement.Api`: API layer responsible for handling HTTP requests, routing them to appropriate services in the Application layer, and configuring dependencies.
- **Controllers**: Defines controllers for managing endpoints, acting as entry points for API requests.
- **Services**: Contains utilities like `CurrentUserProvider.cs` for user context
- `GymManagement.Contracts`: Defines the data contracts. It includes request and response models for gyms, rooms, subscriptions etc.

#### Application Layer

- `GymManagement.Application`: Contains business logic, including commands, queries, and service interfaces. Organized by feature (e.g. `Gyms`, `Rooms`, `Subscriptions`).
- **Commands and Queries**: Implements CQRS for managing and retrieving data with the Mediator pattern, using the `MediatR` library.
- **Behaviors**: Includes validation using FluentValidation and custom pipeline behaviors for cross-cutting concerns.
- **Dependency Injection**: Facilitates loose coupling by providing interfaces for dependencies.

#### Domain Layer

- `GymManagement.Domain`: Encapsulates core domain logic and entities.
- **Entities**: Defines domain models, errors, and custom types specific to gyms, rooms, and subscriptions.
- **Domain-Driven Design (DDD)**: Follows DDD principles, ensuring domain models represent business rules and policies.
- **Domain Events**: Implements events and event handlers to propagate changes. Eventual Consistency is used.

#### Infrastructure Layer

- `GymManagement.Infrastructure`: Handles persistence, external dependencies, and middleware.
- **Repositories and UoW**: Implements the Repository and Unit of Work patterns for database operations.
- **Authentication**: Provides services for authorization, claims, and token generation.
- **Migrations**: Manages database schema with migrations, enabling version control for the database structure.
- **Middleware**: Includes `EventualConsistencyMiddleware.cs` for handling eventual consistency.

### Testing

- **`GymManagement.Application.IntegrationTests`**: Verifies that modules interact correctly in an end-to-end setup.
- **`GymManagement.Application.SubcutaneousTests`**: Subcutaneous tests focusing on application boundary interactions.
- **`GymManagement.Application.UnitTests`**: Unit tests for business logic in the Application layer.
- **`GymManagement.Domain.UnitTests`**: Unit tests for domain logic and rules.
- **`TestCommon`**: Shared utilities, factories, and constants to support test projects.

## Setup and Installation

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- A supported database for data persistence. This project uses SQLite by default, but you can configure it to use SQL Server or other supported databases if desired.

### Installation Steps

1. Clone the repository.

   ```bash
   git clone https://github.com/Gregapos/Gym-Management.git
   ```

2. Navigate to the project directory.

   ```bash
   cd Gym-Management
   ```

3. Restore dependencies.

   ```bash
   dotnet restore
   ```

4. Run the database migrations.

   ```bash
   dotnet ef database update -p .\src\GymManagement.Infrastructure\ -s .\src\GymManagement.Api\
   ```

5. Run the API project.

   ```bash
   dotnet run --project src/GymManagement.Api
   ```

## Usage

The `.http` files in the `requests` folder allow you to test each API endpoint:

1. Open an `.http` file (e.g., `ListGyms.http`).
2. Send requests directly from your editor (e.g., Visual Studio Code) to test endpoint responses.

## Testing

To run all tests, navigate to the root folder and run:

```bash
dotnet test
```
