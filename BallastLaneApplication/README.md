# BallastLaneApplication

This is a technical interview exercise

Ballast Lane Application is a .NET application that allows users to manage tasks and provides user authentication functionality. This project adheres to Clean Architecture principles and utilizes Test-Driven Development (TDD) methodologies.

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

