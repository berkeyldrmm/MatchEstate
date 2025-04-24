# MatchEstate

## Project Overview

MatchEstate is a multi-layered application designed to streamline real estate management and matching processes. The application is built using .NET technologies and adheres to a modular architecture for scalability and maintainability.

## Features

- **Business Logic Layer**: Contains core business operations.
- **Data Access Layer**: Manages database interactions using Entity Framework.
- **DTO Layer**: Defines Data Transfer Objects for efficient communication between layers.
- **Entity Layer**: Houses the entities representing the application's data models.
- **Web Application**: A user-friendly interface with views, controllers, and middlewares.
- **Built-in Validation**: Ensures data integrity and accuracy.

## File Structure

```plaintext
MatchEstate/
├── BusinessLayer/          # Core business logic and validations
├── DataAccessLayer/        # Database context and migrations
├── DTOLayer/               # Data Transfer Objects
├── EntityLayer/            # Entity models
├── MatchEstate/            # Main application logic
│   ├── Controllers/        # API and web controllers
│   ├── Views/              # Razor views for the front-end
│   ├── Middlewares/        # Custom middleware
│   ├── wwwroot/            # Static files (CSS, JS, images)
├── README.md               # Project documentation
```

## Technologies Used

- **Framework**: .NET 8.0
- **Database**: Entity Framework Core with SQL Server
- **Frontend**: Razor Views, Bootstrap
- **Validation**: Fluent Validation
- **Version Control**: Git

## Setup Instructions

1. **Clone the Repository**
   ```bash
   git clone <repository-url>
   cd MatchEstate
   ```

2. **Install Dependencies**
   Ensure you have the .NET SDK installed. Restore NuGet packages:
   ```bash
   dotnet restore
   ```

3. **Configure the Database**
   Update the connection string in `appsettings.json` located in the `MatchEstate` folder.

4. **Run Migrations**
   Apply database migrations to set up the schema:
   ```bash
   dotnet ef database update
   ```

5. **Build and Run**
   Build the project and start the application:
   ```bash
   dotnet run
   ```

6. **Access the Application**
   Open a web browser and navigate to `http://localhost:7191`.

   ## Application is live
   You can try the application by visiting following link.
   
   https://berkeyldrmm-001-site1.jtempurl.com/
   
   Demo Account:
   
   Email: berke.yildirimm44@gmail.com
   
   Password: Berkeyld34
