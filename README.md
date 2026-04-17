# Online Learning Management System — REST API

A production-ready ASP.NET Core 8 Web API for an online learning platform, built to apply and demonstrate real-world backend engineering practices: clean architecture, CI/CD pipelines with GitHub Actions, and cloud deployment on Azure.

---

## Purpose

This project was built as a hands-on exercise in real production backend workflows — not just writing code that works, but shipping it correctly. The focus was on:

- Structuring a scalable API with Clean Architecture
- Enforcing quality gates through automated testing in CI
- Deploying to Azure with environment-separated pipelines
- Using industry-standard patterns: Repository, Service Layer, DTO, Dependency Injection

---

## Tech Stack

| Layer | Technology |
|---|---|
| Runtime | .NET 8 / ASP.NET Core Web API |
| ORM | Entity Framework Core 8 (SQL Server) |
| Authentication | JWT Bearer (role-based: Student, Instructor, Admin) |
| Validation | FluentValidation 11 |
| Mapping | AutoMapper 13 |
| File Storage | Azure Blob Storage (Azure.Storage.Blobs 12) |
| Testing | xUnit + Moq + FluentAssertions |
| Test Coverage | Coverlet |
| Documentation | Swagger / OpenAPI (Swashbuckle) |
| CI/CD | GitHub Actions |
| Cloud | Azure App Service (Web Apps) |
| Config | DotNetEnv + Azure environment variables |

---

## Architecture

Clean Architecture with strict layer separation:

```
Online Learning Management/
├── Domain/               # Entities, interfaces, domain contracts
├── Application/          # Feature modules (14 total), DTOs, validators, AutoMapper profiles
├── Infrastructure/       # EF Core DbContext, repositories, Azure services, DI registration
├── Presentation/         # ASP.NET Controllers (15 endpoints)
└── Migrations/           # EF Core migration history
```

### Key Patterns Applied

**Repository Pattern** — 13 repositories abstract all data access. Controllers never touch DbContext directly.

**Service Layer** — Business logic lives in services, not controllers. Controllers delegate and return HTTP responses.

**Dependency Injection** — All services and repositories registered via a centralized `AddProjectServices()` extension method with scoped lifetimes.

**DTO Pattern** — Separate request/response DTOs per feature. AutoMapper profiles handle entity-to-DTO mapping with explicit configurations.

**FluentValidation** — Custom validators per DTO (e.g., `CreateUserValidator`, `CreateCourseValidator`) run before any service logic executes.

**EF Core Fluent API** — Entity configurations are isolated in separate `IEntityTypeConfiguration<T>` classes, not scattered in `OnModelCreating`.

---

## Domain Model

11 core entities covering the full learning lifecycle:

```
User ──< Instructor ──< Course ──< Module ──< ModuleTask
                    └── Student >──< CourseStudent
                                └── ModuleProgress
                                └── TaskStudent
                                └── GradeStudents
Course ──< Forum ──< Posts
Course ──< ReportCourse
Module ──< FileMetadata
```

---

## API Features

| Module | Description |
|---|---|
| Auth | Registration, JWT login, role assignment |
| Courses | CRUD, search by title, instructor-scoped access |
| Modules | Course modules with dates, resources, learning outcomes |
| ModuleTasks | Task creation and assignment within modules |
| ModuleProgresses | Student progress tracking per module |
| Students | Student profile management |
| Instructors | Instructor profile management |
| CourseStudents | Enrollment management (many-to-many) |
| TaskStudents | Task submission tracking |
| Files | Upload to Azure Blob Storage, metadata stored in DB |
| Forums | Per-course discussion forums |
| Posts | Forum post CRUD |
| GradeStudents | Grade assignment and retrieval |
| ReportCourses | Course reporting |

### Authorization

- `[Authorize(Roles = "Instructor")]` — course and module management
- `[Authorize(Roles = "Student")]` — enrollment and progress
- `[AllowAnonymous]` — login and registration only

---

## GitHub Actions — CI/CD Pipelines

Two pipelines mirror the branching strategy:

### `develop` branch → staging slot

```yaml
# .github/workflows/develop_learmanage-develop.yml
Trigger: push to develop | manual dispatch

Jobs:
  build  → dotnet build --configuration Release → upload artifact
  test   → xUnit with TRX reporter → upload test results
  deploy → Azure login (OIDC service principal) → deploy to learmanage-develop
```

### `master` branch → production

```yaml
# .github/workflows/master_learnmanage.yml
Trigger: push to master | manual dispatch

Jobs:
  build  → dotnet build --configuration Release → upload artifact
  test   → xUnit with TRX reporter → upload test results
  deploy → Azure login (OIDC service principal) → deploy to learnmanage (production)
```

**What this enforces:**
- No deployment without a passing build
- Tests run on every push — failures block deployment
- Artifacts are built once and promoted, not rebuilt at deploy time
- Separate credentials per environment (staging vs production)
- Azure service principal with OIDC — no long-lived secrets in repo

---

## Azure Deployment

| Resource | Purpose |
|---|---|
| Azure App Service (`learnmanage`) | Production Web App (.NET 8) |
| Azure App Service (`learmanage-develop`) | Staging/develop slot |
| Azure SQL Server | Relational database (EF Core migrations) |
| Azure Blob Storage | File uploads (course materials, attachments) |

Configuration (connection strings, JWT secrets) is managed via Azure App Service environment variables — no secrets committed to source control.

---

## Testing

```
OLM Tests/
├── Repository tests  (8 files) — in-memory EF Core database
├── Service tests     — Moq for dependency mocking
└── Validator tests   — FluentValidation rule verification
```

- xUnit as test runner
- Moq for mocking service/repository dependencies
- FluentAssertions for readable assertions
- Coverlet for coverage measurement
- TRX output consumed by GitHub Actions test reporter

Run tests locally:

```bash
dotnet test --configuration Release
```

---

## Local Setup

**Prerequisites:** .NET 8 SDK, SQL Server, Azure Storage account (or Azurite for local emulation)

```bash
# Clone
git clone https://github.com/NicoZela23/Online-Learning-Management.git
cd Online-Learning-Management

# Configure
cp .env.example .env
# Fill in: ConnectionStrings__DefaultConnection, JWT settings, AzureBlobStorage

# Apply migrations
dotnet ef database update --project "Online Learning Management"

# Run
dotnet run --project "Online Learning Management"
```

API available at `https://localhost:5001`. Swagger UI at `/swagger`.

---

## What This Project Demonstrates

- **Clean Architecture** applied to a real multi-feature API — not a toy CRUD app
- **CI/CD pipeline design** with build → test → deploy gates and environment separation
- **Azure cloud deployment** with proper credential management via OIDC service principals
- **Production patterns**: Repository + Service + DTO + validation layers working together
- **EF Core** schema management with migrations across iterative feature development
- **Role-based JWT auth** with per-endpoint authorization policies
- **Cloud file storage** integration with Azure Blob Storage behind a service interface
