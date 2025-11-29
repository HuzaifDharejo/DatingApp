# DatingApp

A full-stack dating application built with .NET Core and Angular.

## Project Structure

```
DatingApp/
├── DatingApp/          # .NET Core Web API Backend
├── DatingApp-SPA/      # Angular Frontend SPA
├── DatingApp.Tests/    # Unit Tests
└── DatingApp.sln       # Solution File
```

## Technology Stack

### Backend
- .NET Core 3.0
- Entity Framework Core
- SQLite / SQL Server
- JWT Authentication
- AutoMapper
- Cloudinary (for image uploads)

### Frontend
- Angular 8
- Bootstrap 4 / Bootswatch
- ngx-bootstrap
- ngx-gallery
- AlertifyJS
- Font Awesome

## Getting Started

### Prerequisites
- [.NET Core SDK 3.0](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) (for Angular CLI)
- [Angular CLI](https://cli.angular.io/) (`npm install -g @angular/cli`)

### Backend Setup

1. Navigate to the API project:
   ```bash
   cd DatingApp
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Run the API:
   ```bash
   dotnet run
   ```

The API will be available at `https://localhost:5001` or `http://localhost:5000`.

### Frontend Setup

1. Navigate to the SPA project:
   ```bash
   cd DatingApp-SPA
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Start the development server:
   ```bash
   ng serve
   ```

The application will be available at `http://localhost:4200`.

## Running Tests

### Backend Tests
```bash
cd DatingApp.Tests
dotnet test
```

### Frontend Tests
```bash
cd DatingApp-SPA
ng test
```

### End-to-End Tests
```bash
cd DatingApp-SPA
ng e2e
```

## Features

- User registration and authentication
- User profiles with photo uploads
- Member listing and search
- Messaging between users
- Like/Match functionality

## License

This project is for educational purposes.
