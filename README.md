# Rock, Paper, Scissors, Lizard, Spock API

## Description

This project is a RESTful API that simulates the game **Rock, Paper, Scissors, Lizard, Spock** from _The Big Bang Theory_. It is built with **.NET 8** and uses **MSSQL** as the database.

## Features

- Play the game **Rock, Paper, Scissors, Spock, Lizard** against a computer opponent.
- Random computer choice generation from the five options: **Rock, Paper, Scissors, Spock, Lizard.**
- Retrieve all possible game choices via an API endpoint.

## Technologies Used

This project integrates several powerful tools, frameworks, and patterns:

- **Docker**: Used for containerizing the API and the MSSQL database.
- **Entity Framework**: Provides Object-Relational Mapping (ORM) to manage database operations and migrations.
- **Serilog**: Logging library for structured and detailed logging throughout the application.
- **Refit**: A REST library to easily integrate HTTP API calls.
- **TestContainers**: Utilized for running MSSQL in containers during integration testing, ensuring isolated and reproducible test environments.
- **CQRS with Mediatr**: Implements the CQRS (Command Query Responsibility Segregation) pattern using the Mediatr library to manage commands and queries cleanly and efficiently.

## Running the App

You can run the application using the following methods:

### 1. Docker Compose

The easiest way to run the app is by using the `docker-compose.yml` file located in the root folder. This will spin up an instance of MSSQL Server and the API without any additional configuration.

Run the following command from root folder:

```bash
docker-compose up --build
```

After running this command the app will be hosted on http://localhost:8080

Or if using `Visual Studio`, set the docker compose project as startup and run the app.

### 2. Local Development without Docker

Alternatively, you can run the API locally by modifying the `appsettings.Development.json` file located in the `src/GameService.Host` folder. Change the `GameDb` variable in the `ConnectionStrings` section to point to your existing database.
And run this command in `src/GameService.Host` folder:
```bash
dotnet run
```

After running this command the app will be hosted on http://localhost:8080 and https://localhost:8081

## API Documentation

The API includes Swagger documentation, providing an interactive interface to explore and test the available endpoints. You can access the Swagger UI by navigating to `/swagger` after starting the application.

## Applying Migrations

Migrations for the application are located in the `src/GameService.Infrastructure/Migrations` folder. To apply the migrations manually modify the `appsettings.Development.json` file located in the `src/GameService.Host` folder. Change the `GameDb` variable in the `ConnectionStrings` section to point to your existing database and use the following command in the root folder :

```bash
dotnet ef database update --startup-project src/GameService.Host --project src/GameService.Infrastructure
```

Make sure that the dotnet ef is installed if not run this command to install it gloablly:

```bash
dotnet tool install -g dotnet-ef
```

For the development environment, when running the app using Docker or dotnet run, migrations will be automatically applied. The database will also be seeded with predefined choices and rules for the game to ensure a smooth start.

## Running Tests

To run the tests, use the following command:

```bash
dotnet test
```

During the testing process, a Docker container for MSSQL will automatically spin up to provide a clean environment for integration tests. Once the tests finish running, the container will be removed, ensuring no lingering resources are left behind.
