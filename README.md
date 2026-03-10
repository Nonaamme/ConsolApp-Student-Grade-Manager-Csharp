# Student Grade Manager

A console application for managing students and their grades. Add, edit, and remove students; record grades; search and view statistics—all with data persisted to a local file.

## Technologies Used

- **C#** (.NET 9)
- **LINQ** for queries and aggregations
- **System.Text.Json** for file persistence
- **Console I/O** with basic input validation

## Features

- **CRUD** — Add, edit, and delete students
- **Grades** — Add multiple grades (1–6) per student
- **Search** — Find students by name, surname, or ID
- **Statistics** — Top/lowest student by average; filter students above a given average
- **Persistence** — Load on startup and save on exit (JSON file)

## Project Structure

```
Student_Grade_Manager/
├── Student_Grade_Manager.sln
└── Student_Grade_Manager/
    └── Student_Grade_Manager/
        ├── Program.cs          # Entry point, menu, UI flow
        ├── Student.cs          # Student entity and grade logic
        ├── StudentManager.cs   # In-memory list and CRUD/search
        └── StudentStorage.cs   # JSON load/save
```

## How to Run

**Prerequisites:** .NET 9 SDK (or compatible .NET version).

```bash
git clone <repo-url>
cd ConsolApp - Student_Grade_Manager/Student_Grade_Manager
dotnet run
```

Or open the solution in Visual Studio and run the **Student_Grade_Manager** project. Data is stored in `students.json` next to the executable.

## What This Project Demonstrates

- **OOP** — Clear separation between entity (`Student`), service (`StudentManager`), and storage (`StudentStorage`)
- **Collections** — `List<T>` for students and grades
- **LINQ** — Filtering, sorting, averages, and conditional queries
- **File I/O** — JSON serialization/deserialization and error handling
- **Exception handling** — Validation for names and grade range (1–6)
- **Console UX** — Simple menu-driven interface with guided input
