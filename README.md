# Driving & Vehicle License Department

A C# desktop application for managing driving license services and related administrative workflows.

## Overview

This project is a full-scale desktop application developed to simulate a real-world Driving & Vehicle License Department system.

It brings together object-oriented programming, database design, SQL, ADO.NET, and 3-tier architecture to implement different business workflows and administrative operations.

## Features

* User authentication and account management
* User roles
* People management
* Application management
* Application types and test types
* Driving license issuance and renewal
* Replacement of lost or damaged licenses
* License detain and release
* Local driving licenses
* International driving licenses
* License history
* Business rules and input validation

## Architecture

The application follows a **3-tier architecture** that separates responsibilities between different layers:

* **Presentation Layer** — Handles the Windows Forms user interface and user interactions.
* **Business Layer** — Contains business rules, validation, and application logic.
* **Data Access Layer** — Handles communication with the SQL database through ADO.NET.

This separation keeps the code organized and gives each layer responsibility for a specific part of the application.

## Technologies

* **C#**
* **.NET Framework**
* **Windows Forms (WinForms)**
* **Object-Oriented Programming (OOP)**
* **SQL**
* **ADO.NET**
* **3-Tier Architecture**

## Project Structure

```text
DrivingAndVehicleLicenseDepartment/
│
├── DVLD_DataAccess/
│   └── Data access and database operations
│
├── DVLD_Business/
│   └── Business logic and application rules
│
└── DrivingAndVehicleLicenseDepartment/
    └── Presentation layer and Windows Forms user interface
```

## Database

The application uses a relational SQL database to store and manage information related to:

* People
* Users
* Applications
* Licenses
* Tests
* Application types
* Test types
* License history
* Related administrative data

Database communication is handled through **ADO.NET**.

## What This Project Demonstrates

This project demonstrates the practical application of:

* Object-oriented design
* Separation of responsibilities
* 3-tier application architecture
* Database interaction
* SQL querying
* ADO.NET
* Business rule implementation
* User authentication
* User roles
* Input validation
* Working with a larger C# codebase

## Getting Started

### Requirements

* Windows
* .NET Framework
* SQL Server
* Visual Studio
* A configured database for the application

### Setup

1. Clone the repository.
2. Open the solution in Visual Studio.
3. Configure the database connection.
4. Create or restore the required SQL database.
5. Build and run the application.

> Database configuration may need to be adjusted for your local SQL Server environment.

## Project Status

This project was developed as a practical full-project application and is part of my continued software development learning path.
