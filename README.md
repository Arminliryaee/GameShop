# GameShop Project

## About This Project

This project is a simple web application for a video game store, developed as a learning exercise. The main goal was to implement a clean, layered architecture using modern .NET technologies.

**Important Note:** This project is for practice purposes only. AI tools were used during development to speed up the process and assist in implementing design patterns.

## Architecture and Design

The project is built upon a **Layered Architecture** to separate the different logical concerns of the application, making it easier to develop and maintain. The main layers of the project are:

  * **`GameShop.Domain`**: This is the core of the application, containing the entities, enums, and repository interfaces. It has no dependencies on other layers.
  * **`GameShop.Application`**: This layer holds the business logic. Services, DTOs (Data Transfer Objects), and service interfaces are defined here. It communicates with the Domain layer to manage the application's core processes.
  * **`GameShop.Infrastructure`**: This layer is responsible for data access and communication with the database. The implementation of the Repository and Unit of Work patterns using Entity Framework Core resides here.
  * **`GameShop.Presentation`**: This is the user interface (UI) layer, built with ASP.NET Core Razor Pages. Users interact with the system through this layer, which uses services from the Application layer to perform operations.

### Design Patterns Used:

  * **Repository & Unit of Work Pattern**: Used to decouple the data access logic from the business logic. The `UnitOfWork` ensures that all changes within a single transaction are saved together.
  * **Dependency Injection (DI)**: To reduce coupling between components and improve testability, all services and repositories are injected via DI where needed. This is configured in the `Program.cs` file.

## Technologies Used

  * **.NET 9**
  * **ASP.NET Core Razor Pages**
  * **Entity Framework Core**
  * **SQL Server**
  * **Cookie-based Authentication**

## Core Features

  * User registration and login functionality.
  * Display a list of available games.
  * Add games to a shopping cart.
  * Shopping cart management (add/remove items).
  * Order checkout process.
  * Admin panel for managing games (Create, Read, Update, Delete).
  * User profile view with order history.

## How to Run the Project

1.  **Clone the repository:**
    ```sh
    git clone <repository-url>
    ```
2.  **Configure the database connection:**
      * Open the `appsettings.json` file in the `GameShop.Presentation` project.
      * Modify the connection string under `"ConnectionStrings"` to point to your local SQL Server instance.
    <!-- end list -->
    ```json
    "ConnectionStrings": {
      "cnn": "Server=.;Database=GameShop;Trusted_Connection=True;TrustServerCertificate=True;"
    }
    ```
3.  **Apply database migrations:**
      * Open a terminal or PowerShell in the root directory of the `GameShop.Infrastecture` project.
      * Run the following command to apply the EF Core migrations and create the database:
    <!-- end list -->
    ```sh
    dotnet ef database update
    ```
4.  **Run the application:**
      * Navigate to the `GameShop.Presentation` project directory.
      * Run the project using the .NET CLI:
    <!-- end list -->
    ```sh
    dotnet run
    ```
5.  **Access the application:**
      * Open your web browser and navigate to the URL provided in the console (e.g., `https://localhost:7148`).
