# Appointment Management System – ASP.NET Core Web API

This project is a structured, maintainable Appointment Management System built with ASP.NET Core Web API and Entity Framework Core. It provides complete management for Doctors, Patients, and Appointments using a Repository Pattern, DTOs, unified validation logic, and a clean project structure.

---

## 📁 Project Structure

BL/
├── ExtensionMethods/
│ └── Allextensionmethods.cs
├── Interface/
│ ├── IAppointment.cs
│ ├── IDoctor.cs
│ ├── IGenericRepository.cs
│ └── IPatient.cs
├── Repositories/
│ ├── AppointmentRepository.cs
│ ├── DoctorRepository.cs
│ ├── GenericRepository.cs
│ └── PatientRepository.cs
└── Transactions/

Controllers/
├── AppointmentController.cs
├── DoctorController.cs
└── PatientController.cs

Data/
DataBase/
DTO/
Filters/
Migrations/
AppDbContext.cs
Program.cs


---

## Features

### Doctors
- Create / Update / Delete doctors
- Dropdowns
- Soft-delete support
- Audit fields for created/modified dates

### Patients
- CRUD operations
- Dropdowns
- Soft delete + audit trail

### Appointments
- Create / Update / Delete
- Unified validation:
  - No future duplicate appointment with same doctor
  - Appointment must be less or equl to 1 hour
  - Limited to working hours (10 AM – 05 PM)
  - Prevent overlapping appointments
- Returns full details including Patient & Doctor names

---

## Architecture

- Repository Pattern (Generic + Specific Repos)
- DTO layer for input/output
- Controllers use dependency injection
- Extension methods for shared logic
- EF Core with SQLite database
- Modular folder structure designed for scale
- Soft delete 
- globle Response DTO structure
- Globle state check
- Mapper for mapping DTO

Unified appointment validation is handled in one method inside `AppointmentRepository`:

