# Cinephile Web API with JWT Authentication

This is a .NET 8 Web API project that implements JWT authentication using the repository-service-controller pattern.

## Project Structure

- **Cinephile**: Main Web API project (startup project)
- **Application**: Contains DTOs, interfaces, and services
- **Domain**: Contains entities and domain interfaces
- **Infrastructure**: Contains database context and repositories

## Features

- JWT Authentication
- User registration and login
- Role-based authorization (Admin and User roles)
- Repository-Service-Controller pattern
- Entity Framework Core with SQL Server
- ASP.NET Core Identity

## Setup

1. **Database Connection**: Update the connection string in `appsettings.json`
2. **Run Migrations**: The database will be created automatically on first run
3. **Seed Data**: Admin and User roles will be created automatically, along with a default admin user

## Default Admin User

- Email: `admin@cinephile.com`
- Password: `Admin123!`
- Role: Admin

## API Endpoints

### Authentication

#### Register User
```
POST /api/auth/register
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "Password123!",
  "confirmPassword": "Password123!",
  "name": "John Doe",
  "age": 25
}
```

#### Login
```
POST /api/auth/login
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "Password123!"
}
```

### Test Endpoints

#### Public Endpoint (No Authentication Required)
```
GET /api/test/public
```

#### Authenticated Endpoint (Requires JWT Token)
```
GET /api/test/authenticated
Authorization: Bearer <your-jwt-token>
```

#### Admin Only Endpoint
```
GET /api/test/admin
Authorization: Bearer <your-jwt-token>
```

#### User Only Endpoint
```
GET /api/test/user
Authorization: Bearer <your-jwt-token>
```

## JWT Token Usage

1. **Login or Register** to get a JWT token
2. **Include the token** in the Authorization header: `Authorization: Bearer <token>`
3. **Token expires** after 60 minutes (configurable in appsettings.json)

## JWT Configuration

The JWT settings are configured in `appsettings.json`:

```json
{
  "Jwt": {
    "Key": "ThisIsASecretKeyForJwtDontShareeeeeeeee",
    "Issuer": "CinephileAPI",
    "Audience": "CinephileAPIUsers",
    "ExpireMinutes": 60
  }
}
```

## Running the Application

1. Open the solution in Visual Studio or VS Code
2. Set `Cinephile` as the startup project
3. Run the application
4. Navigate to `/swagger` for the API documentation

## Architecture

- **Controllers**: Handle HTTP requests and responses
- **Services**: Contain business logic
- **Repositories**: Handle data access
- **DTOs**: Data transfer objects for API requests/responses
- **Entities**: Domain models

## Security Features

- Password hashing using ASP.NET Core Identity
- JWT token validation
- Role-based authorization
- Input validation using data annotations
- Secure password requirements 