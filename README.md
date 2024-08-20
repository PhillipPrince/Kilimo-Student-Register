
 Kilimo Student Register

 Project Overview

Kilimo Student Register is a web application designed to manage class streams and student records. It provides functionalities to create, view, edit, and delete class streams and students. The application uses ASP.NET Core for the backend, and it connects to a SQL Server database to store and manage data.

 Features

- Class Streams Management: Create, view, and manage class streams.
- Student Management: Add, view, and assign students to class streams.
- AJAX Integration: Use AJAX for dynamic data updates and interactions.
- Modals for CRUD Operations: Use Bootstrap modals for creating and editing records.

 Technology Stack

- Backend: ASP.NET Core
- Frontend: HTML, CSS, Bootstrap, jQuery
- Database: SQL Server
- Data Access: ADO.NET
- Configuration Management: `appsettings.json`

 Getting Started

 Prerequisites

- .NET SDK 6.0 or later
- SQL Server
- Visual Studio or any other IDE compatible with .NET Core

 Installation

1. Clone the Repository

   ```bash
   git clone https://github.com/PhillipPrince/Kilimo-Student-Register
   cd kilimo-student-register
   ```

2. Configure the Database

   - Create a database in SQL Server.
   - Update the connection string in `appsettings.json` to match your database configuration:

     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=your_server;Database=your_database;User Id=your_username;Password=your_password;"
     }
     ```

3. Apply Database Migrations

   If using Entity Framework, apply migrations using:

   ```bash
   dotnet ef database update
   ```

4. Build and Run the Application

   ```bash
   dotnet build
   dotnet run
   ```

   Navigate to `https://localhost:7102` in your web browser to access the application.

 Usage

 Class Streams

- Create Class Stream: Click the "Create New Stream" button to open a modal for adding new class streams.
- View Class Streams: Class streams are displayed in a table. Use AJAX to dynamically update the list.

 Students

- Add Student: Fill out the form to add a new student, including selecting a class stream.
- View Students: Students are listed in a table with options to view, edit, and delete records.
- Edit and Delete: Use buttons in the student table to edit or delete student records.

 API Endpoints

 Class Streams

- GET `/Streams/GetClassStreams` - Retrieve a list of class streams.
- POST `/Streams/CreateClassStream` - Create a new class stream.

 Students

- GET `/Students/GetStudents` - Retrieve a list of students.
- POST `/Students/CreateStudent` - Add a new student.

 Troubleshooting

- 404 Error: Ensure that the URL matches the route defined in your controller and that the application is running.
- Database Connection Issues: Verify that the connection string in `appsettings.json` is correct and that SQL Server is accessible.

