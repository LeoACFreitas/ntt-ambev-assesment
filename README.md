# Ambev Developer Evaluation - Backend

This project is the backend component of the Ambev Developer Evaluation, implemented as a RESTful API for managing sales records. It showcases a modern .NET application built with a focus on clean architecture, domain-driven design (DDD), and the Command Query Responsibility Segregation (CQRS) pattern.

## Technologies Used

*   **.NET 8.0:** The core development platform.
*   **C#:** The primary programming language.
*   **ASP.NET Core:** For building the RESTful API.
*   **Entity Framework Core:** ORM for database interactions (currently configured for PostgreSQL).
*   **MediatR:** For implementing the CQRS pattern, separating commands/queries from their handlers.
*   **AutoMapper:** For object-to-object mapping between domain entities and DTOs.
*   **FluentValidation:** For defining and enforcing validation rules.
*   **xUnit:** The testing framework for unit tests.
*   **NSubstitute:** A mocking framework for unit testing.
*   **Bogus:** A library for generating fake data for testing.
*   **PostgreSQL:** The relational database used for persistence.
*   **Docker/Docker Compose:** For containerizing the application and its dependencies, simplifying local development setup.

## Project Structure

The project follows a Clean Architecture approach, organized into several layers:

*   `Ambev.DeveloperEvaluation.Domain`: Contains the core business logic, entities, value objects, and domain services.
*   `Ambev.DeveloperEvaluation.Application`: Implements application-specific business rules and orchestrates domain objects to fulfill use cases (CQRS commands and queries).
*   `Ambev.DeveloperEvaluation.ORM`: Handles data persistence using Entity Framework Core, acting as the infrastructure layer for data access.
*   `Ambev.DeveloperEvaluation.IoC`: Manages dependency injection and service registration.
*   `Ambev.DeveloperEvaluation.WebApi`: The entry point of the application, exposing the RESTful API endpoints.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

*   [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
*   [Docker Desktop](https://www.docker.com/products/docker-desktop) (or Docker Engine if on Linux)

### Setup and Running

1.  **Navigate to the backend directory:**
    ```bash
    cd template/backend
    ```

2.  **Build the images using Docker Compose:**
    ```bash
    docker compose build --no-cache
    ```

3.  **Run the containers:**
    ```bash
    docker compose up
    ```

### Running Tests

To run all unit tests in the solution:

```bash
dotnet test
```

### Code Coverage

To generate code coverage reports, you can use the provided scripts:

*   **Windows:**
    ```bash
    ./coverage-report.bat
    ```
*   **Linux/macOS:**
    ```bash
    ./coverage-report.sh
    ```

These scripts typically use `dotnet-coverage` or `coverlet` to generate reports in a human-readable format (e.g., HTML).

## Contributing

Refer to the main project `README.md` and `.doc` folder for overall project guidelines and requirements.
