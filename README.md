# BallastLaneApplication

This is a technical interview exercise

Ballast Lane Application is a .NET application that allows users to manage tasks and provides user authentication functionality. This project adheres to Clean Architecture principles and utilizes Test-Driven Development (TDD) methodologies.

## Architecture Overview

The Ballast Lane Application is structured following the principles of Clean Architecture, aiming to create a system that is:

- **Independent of Frameworks**: The architecture does not rely on the existence of some library or feature-laden software. This allows you to use such frameworks as tools, rather than having to fit your system into their limited constraints.
- **Testable**: The business rules can be tested without the UI, database, web server, or any external element.
- **Independent of UI**: The UI can change easily, without changing the rest of the system. A web UI could be replaced with a console UI, for example, without changing the business rules.
- **Independent of Database**: You can swap out Oracle or SQL Server for MongoDB or another database. Your business rules are not bound to the database.
- **Independent of any external agency**: In fact, your business rules simply don’t know anything at all about the outside world.

### Clean Architecture Layers

1. **Domain Layer (Core)**:
   - Contains all entities, enums, exceptions, interfaces, types, and logic specific to the domain that can be used across the entire application.
   - Example: `User` and `Task` entities are defined here with their properties and any domain logic.

2. **Application Layer (Services)**:
   - Contains business logic and application behavior.
   - Encapsulates and implements all of the use cases of the system.
   - Coordinates the domain layer objects to perform the actual work of the application.
   - Example: `UserService` and `TaskService` are part of this layer, implementing interfaces like `IUserService` and `ITaskService`.

3. **Infrastructure Layer**:
   - Provides concrete implementations for interfaces defined in the domain layer, allowing the application to interact with external concerns like databases, file systems, and web services.
   - This layer contains the data access logic and can implement repository interfaces from the domain layer.
   - Example: `UserRepository` and `TaskRepository` provide MongoDB-specific data access functionalities.

4. **Presentation Layer (API)**:
   - The entry point of the application, where the application interacts with the outside world.
   - Handles HTTP requests, translates them into actions against the model, and responds with views or data.
   - Example: `UsersController` and `TasksController` handle incoming HTTP requests, invoke application layer services, and respond with data or views.

### Database Design

- **MongoDB**: A NoSQL database was chosen for its schema flexibility, scalability, and performance, allowing the application to handle a wide variety of data types and structures without the need for predefined schemas.
- **Schema**: The database schema includes collections like `Users` and `Tasks`, each identified by a unique ID, and includes fields necessary for the respective functionalities.

### API Layer

- **ASP.NET Core Web API**: Used to create RESTful APIs that handle HTTP requests and return responses in JSON format, providing an interface for front-end applications to interact with the back-end.
- **Endpoints**: A set of endpoints are defined for operations like user authentication and task management (CRUD operations).

### Business Logic Layer

- **Services**: Business logic is encapsulated in services which are responsible for executing specific business actions, ensuring that business rules are adhered to and that the correct data is passed between the API and data layers.
- **Validation and Business Rules**: Ensures that the data entering the system meets certain criteria and that the system behaves correctly according to the defined business rules.

### Data Layer

- **Repositories**: The data access logic is abstracted behind repository interfaces, allowing for a clean separation between how data is accessed and how it's used in the application.
- **Data Access**: Data is accessed and manipulated directly through MongoDB's driver, providing a lightweight, flexible approach to data management without the overhead of additional ORM frameworks.

By adopting Clean Architecture principles, the Ballast Lane Application ensures that its core logic is decoupled from external frameworks and databases, making it more maintainable, scalable, and adaptable to change.


## Features

- CRUD operations for managing tasks.
- User registration and authentication.
- JWT-based authorization.
- Docker support for easy deployment.

## Technology Stack

- **Backend**: .NET 6, ASP.NET Core
- **Database**: MongoDB
- **Authentication**: JWT Authentication
- **Testing**: NUnit, Moq
- **Containerization**: Docker

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

What you need to install the software:

- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)

### Installing

A step-by-step series of examples that tell you how to get a development environment running.

1. **Clone the Repository**

```bash
git clone https://github.com/Pvmendes/BallastLaneApplication.git
cd BallastLaneApplication
```

2. **Set Up the Database**

Run the MongoDB container using Docker Compose:

```bash 
docker-compose up -d
```

3. **Build and Run the Application**

Navigate to the src directory and build the application:
```bash 
dotnet build
```

Run the application:
```bash 
dotnet run --project BallastLaneApplication.API/BallastLaneApplication.API.csproj
```

The API should now be running on https://localhost:5001/swagger/index.html.

### Running the tests
Explain how to run the automated tests for this system.

```bash 
dotnet test
```

## Usage

After running the application, you can interact with the API using the following endpoints:

### Tasks

1. **Get All Tasks**

   - **Description**: Retrieve all tasks.
   - **Method**: GET
   - **Endpoint**: `/api/tasks`
   - **CURL Example**:

     ```bash
     curl -X GET https://localhost:5001/api/tasks -H "Authorization: Bearer YOUR_JWT_TOKEN"
     ```

2. **Get Task by ID**

   - **Description**: Retrieve a single task by its ID.
   - **Method**: GET
   - **Endpoint**: `/api/tasks/{taskId}`
   - **CURL Example**:

     ```bash
     curl -X GET https://localhost:5001/api/tasks/{taskId} -H "Authorization: Bearer YOUR_JWT_TOKEN"
     ```

3. **Create a New Task**

   - **Description**: Create a new task.
   - **Method**: POST
   - **Endpoint**: `/api/tasks`
   - **CURL Example**:

     ```bash
     curl -X POST https://localhost:5001/api/tasks \
     -H "Authorization: Bearer YOUR_JWT_TOKEN" \
     -H "Content-Type: application/json" \
     -d '{
           "title": "New Task",
           "description": "Description of the new task."
         }'
     ```

4. **Update an Existing Task**

   - **Description**: Update an existing task by its ID.
   - **Method**: PUT
   - **Endpoint**: `/api/tasks/{taskId}`
   - **CURL Example**:

     ```bash
     curl -X PUT https://localhost:5001/api/tasks/{taskId} \
     -H "Authorization: Bearer YOUR_JWT_TOKEN" \
     -H "Content-Type: application/json" \
     -d '{
           "title": "Updated Task",
           "description": "Updated description."
         }'
     ```

5. **Delete a Task**

   - **Description**: Delete an existing task by its ID.
   - **Method**: DELETE
   - **Endpoint**: `/api/tasks/{taskId}`
   - **CURL Example**:

     ```bash
     curl -X DELETE https://localhost:5001/api/tasks/{taskId} \
     -H "Authorization: Bearer YOUR_JWT_TOKEN"
     ```

Remember to replace `YOUR_JWT_TOKEN` with your actual JWT token obtained from the authentication process and `{taskId}` with the actual ID of the task you want to manipulate.


### Users

1. **User Registration**

   - **Description**: Register a new user.
   - **Method**: POST
   - **Endpoint**: `/api/users/register`
   - **CURL Example**:

     ```bash
     curl -X POST https://localhost:5001/api/users/register \
     -H "Content-Type: application/json" \
     -d '{
           "username": "newuser",
           "password": "newpassword123"
         }'
     ```

2. **User Authentication**

   - **Description**: Authenticate a user and receive a JWT token.
   - **Method**: POST
   - **Endpoint**: `/api/users/authenticate`
   - **CURL Example**:

     ```bash
     curl -X POST https://localhost:5001/api/users/authenticate \
     -H "Content-Type: application/json" \
     -d '{
           "username": "registereduser",
           "password": "userpassword123"
         }'
     ```

3. **Get User Information**

   - **Description**: Get details of the currently authenticated user.
   - **Method**: GET
   - **Endpoint**: `/api/users/me`
   - **CURL Example**:

     ```bash
     curl -X GET https://localhost:5001/api/users/me \
     -H "Authorization: Bearer YOUR_JWT_TOKEN"
     ```

Remember to replace `YOUR_JWT_TOKEN` with your actual JWT token obtained from the authentication process.

