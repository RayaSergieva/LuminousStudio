# LuminousStudio

LuminousStudio is a full-stack ASP.NET Core web application for browsing, managing, and ordering Tiffany lamps. It is built with a professional multi-layer architecture, a RESTful Web API, real-time stock notifications via SignalR, a role-based admin dashboard, and comprehensive unit test coverage.

---

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Technologies](#technologies)
- [Architecture](#architecture)
- [Project Structure](#project-structure)
- [Main Entities](#main-entities)
- [Getting Started](#getting-started)
- [Default Admin Account](#default-admin-account)
- [Access Levels](#access-levels)
- [Web API](#web-api)
- [Real-Time Features](#real-time-features)
- [Validation and Security](#validation-and-security)
- [Unit Tests](#unit-tests)
- [Deployment](#deployment)

---

## Overview

LuminousStudio is a feature-rich e-commerce application focused on Tiffany lamps. It provides a public product catalog, a shopping cart, order management, and a full administrative interface. The project follows clean architecture principles with strict separation of concerns across multiple class libraries, a repository pattern, strongly-typed configuration, custom middleware, and a RESTful API layer.

---

## Features

### Public
- Browse the full Tiffany lamp catalog with pagination
- Filter lamps by style and manufacturer
- View detailed product pages
- Real-time stock updates without page refresh (SignalR)
- Responsive design with Bootstrap

### Authenticated Users
- User registration and login with ASP.NET Core Identity
- Add products to a shopping cart
- Adjust quantities and remove items
- Place single or bulk orders from the cart
- View personal order history

### Administrators
- Full Admin Area dashboard
- Create, edit, and delete Tiffany lamps
- View and manage all customer orders
- Manage client accounts
- View site-wide statistics (clients, lamps, orders, revenue)
- Automatically redirected to Admin Dashboard on login

### System
- Custom error pages for 400, 401, 403, 404, 500, 503
- Global exception handling middleware
- Security headers middleware (XSS, clickjacking protection)
- Request logging middleware
- Strongly-typed configuration from appsettings.json
- Assembly-scanned service and repository registration

---

## Technologies

| Category | Technology |
|---|---|
| Framework | ASP.NET Core MVC (.NET 10) |
| ORM | Entity Framework Core 10 |
| Database | Microsoft SQL Server |
| Identity | ASP.NET Core Identity |
| Real-Time | SignalR |
| API Documentation | Swagger / OpenAPI |
| Frontend | Razor Views, Bootstrap 5, Bootstrap Icons |
| JavaScript | Vanilla JS, SignalR JS Client |
| Testing | xUnit, Moq, FluentAssertions |
| Architecture | Repository Pattern, Service Layer, MVC Areas |

---

## Architecture

The solution is organized into 15 class library projects following clean architecture principles:

```
LuminousStudio (Solution)
├── Data
│   ├── LuminousStudio.Data              — DbContext, Fluent API configurations, Migrations, Repositories
│   ├── LuminousStudio.Data.Common       — EntityConstants (validation constraints)
│   ├── LuminousStudio.Data.Models       — Entity classes
│   └── LuminousStudio.Data.Seeding      — Identity, LampStyle, Manufacturer seeders
├── Services
│   ├── LuminousStudio.Services.Admin    — Admin-specific services (UserManagement, LampManagement)
│   ├── LuminousStudio.Services.Common   — Shared interfaces (ApplicationRoles, IStockHubService)
│   └── LuminousStudio.Services.Core     — Business logic services and contracts
├── Tests
│   ├── LuminousStudio.Tests.Integration
│   ├── LuminousStudio.Tests.Selenium
│   └── LuminousStudio.Tests.Unit        — Unit tests with 31 passing tests
└── Web
    ├── LuminousStudio.Web               — MVC application (controllers, views, hubs)
    ├── LuminousStudio.Web.Common        — Configuration classes, ValidationMessages, PaginatedList
    ├── LuminousStudio.Web.Infrastructure — Middleware, extensions (ServiceCollection, ApplicationBuilder)
    ├── LuminousStudio.Web.ViewModels    — ViewModels with validation attributes
    └── LuminousStudio.WebApi            — RESTful Web API with Swagger
```

### Dependency Flow

```
Web → Web.Infrastructure → Services.Core → Data → Data.Models
                         → Services.Admin → Data.Repository
                         → Services.Common
```

---

## Project Structure

### Data Layer
- **Fluent API** configuration for all entities — no magic strings or numbers in entity classes
- **Repository Pattern** with `IRepository<T>`, `IAsyncRepository<T>`, and `BaseRepository<T>`
- **Seeding** separated into dedicated seeder classes (`IdentitySeeder`, `LampStyleSeeder`, `ManufacturerSeeder`)
- **Entity Constants** centralize all validation constraints in `EntityConstants.cs`

### Service Layer
- Services depend only on repository interfaces — never on `DbContext` directly
- Admin services separated into `LuminousStudio.Services.Admin`
- Shared role constants and hub interfaces in `LuminousStudio.Services.Common`

### Web Layer
- **MVC Areas** — full Admin area with dashboard, lamp management, user management, order overview, statistics
- **Pagination** on TiffanyLamps and Orders
- **Custom Middleware** — global exception handling, security headers, admin redirection
- **Validation Messages** centralized in `ValidationMessages.cs`
- **Strongly-typed configuration** — `AdminSettings` and `IdentitySettings` from `appsettings.json`

---

## Main Entities

| Entity | Description |
|---|---|
| `ApplicationUser` | Extends IdentityUser with FirstName, LastName, Address |
| `TiffanyLamp` | Main product entity with price, discount, quantity |
| `Manufacturer` | Lamp manufacturer or designer |
| `LampStyle` | Style category (Table Lamp, Floor Lamp, Chandelier, etc.) |
| `Order` | Customer order with quantity, price, discount |
| `ShoppingCart` | Cart items per user before checkout |

---

## Getting Started

### Prerequisites
- Visual Studio 2022 or later
- .NET 10 SDK
- SQL Server (local or remote)

### Setup

**1. Clone the repository:**
```bash
git clone https://github.com/RayaSergieva/LuminousStudio
cd LuminousStudio
```

**2. Configure the database connection** in `LuminousStudio.Web/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=LuminousStudioDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  }
}
```

**3. Restore dependencies:**
```bash
dotnet restore
```

**4. Apply migrations** — set default project to `LuminousStudio.Data` in Package Manager Console:
```
Update-Database
```
or with .NET CLI:
```bash
dotnet ef database update --project LuminousStudio.Data
```

**5. Run the application** — press `F5` in Visual Studio or:
```bash
dotnet run --project LuminousStudio.Web
```

The application will seed roles, the admin account, lamp styles, and manufacturers automatically on first run.

---

## Default Admin Account

| Field | Value |
|---|---|
| Username | admin |
| Email | admin@admin.com |
| Password | Admin123456 |

These credentials are configurable in `appsettings.json` under `AdminSettings`.

---

## Access Levels

### Guest
- Browse the Tiffany lamp catalog
- Filter by style and manufacturer
- View product details
- Access About Us and Contacts pages

### Registered User (Client role)
- Everything a guest can do
- Add items to the shopping cart
- Adjust quantities in the cart
- Place orders (single or from cart)
- View personal order history

### Administrator
- Everything a registered user can do
- Automatically redirected to Admin Dashboard on login
- Full CRUD for Tiffany lamps
- View all customer orders
- Manage client accounts
- View site statistics (clients, lamps, orders, total revenue)

---

## Web API

The `LuminousStudio.WebApi` project exposes a RESTful API documented with Swagger.

### Running the API

The API runs on a separate port. To start both the Web app and the API simultaneously, configure **Multiple Startup Projects** in Visual Studio solution properties.

Access Swagger UI at:
```
https://localhost:{api_port}/swagger
```

### Endpoints

#### TiffanyLamps
| Method | Route | Description | Auth |
|---|---|---|---|
| GET | `/api/TiffanyLamps` | Get all lamps | Public |
| GET | `/api/TiffanyLamps/{id}` | Get lamp by ID | Public |
| GET | `/api/TiffanyLamps/search` | Search by style and manufacturer | Public |
| GET | `/api/TiffanyLamps/style/{styleName}` | Filter by style | Public |
| GET | `/api/TiffanyLamps/discounted` | Get discounted lamps | Public |

#### Orders
| Method | Route | Description | Auth |
|---|---|---|---|
| GET | `/api/OrdersApi` | Get all orders | Admin |
| GET | `/api/OrdersApi/my` | Get current user orders | User |

#### Statistics
| Method | Route | Description | Auth |
|---|---|---|---|
| GET | `/api/StatisticsApi` | Get site statistics | Admin |

#### LampStyles
| Method | Route | Description | Auth |
|---|---|---|---|
| GET | `/api/LampStylesApi` | Get all lamp styles | Public |

#### Manufacturers
| Method | Route | Description | Auth |
|---|---|---|---|
| GET | `/api/ManufacturersApi` | Get all manufacturers | Public |

---

## Real-Time Features

LuminousStudio uses **SignalR** to broadcast real-time stock updates to all connected clients.

When a user places an order, all users currently viewing the TiffanyLamps page will immediately see the updated quantity for that lamp without refreshing the page. A toast notification appears in the bottom-right corner confirming the stock change.

### How it works
1. User places an order via the web application
2. `OrderController` calls `IStockHubService.NotifyStockUpdateAsync`
3. `StockHubService` broadcasts via `IHubContext<StockHub>`
4. All connected clients receive the `ReceiveStockUpdate` event
5. The quantity display updates and a toast notification appears

---

## Validation and Security

### Validation
- **Client-side** — Data Annotations on ViewModels with centralized error messages in `ValidationMessages.cs`
- **Server-side** — `ModelState` validation in all controllers
- **Database-level** — Fluent API constraints (MaxLength, IsRequired, column types)

### Security
- **Authentication** — ASP.NET Core Identity with role-based authorization
- **CSRF Protection** — AntiForgeryToken on all forms
- **SQL Injection** — EF Core parameterized queries
- **XSS Protection** — Razor auto-escaping + `X-XSS-Protection` security header
- **Clickjacking** — `X-Frame-Options: DENY` security header
- **Content Sniffing** — `X-Content-Type-Options: nosniff` header
- **Referrer Policy** — `strict-origin-when-cross-origin` header
- **Global Exception Handling** — custom middleware catches all unhandled exceptions

---

## Unit Tests

The `LuminousStudio.Tests.Unit` project contains **31 unit tests** covering the business logic layer with 65%+ coverage.

### Test Coverage

| Service | Tests | Methods Covered |
|---|---|---|
| `OrderService` | 9 | `CreateAsync`, `UserHasOrdersAsync`, `RemoveByIdAsync` |
| `ShoppingCartService` | 4 | `AddOrUpdateItemAsync`, `RemoveAsync`, `UpdateAsync` |
| `StatisticService` | 8 | All 4 methods |
| `TiffanyLampService` | 4 | `CreateAsync`, `RemoveByIdAsync`, `UpdateAsync`, `GetTiffanyLampsAsync` |
| `LampStyleService` | 3 | All 3 methods |
| `ManufacturerService` | 3 | All 3 methods |

### Running Tests

In Visual Studio go to **Test → Run All Tests** or press `Ctrl+R, A`.

### Test Stack
- **xUnit** — test framework
- **Moq** — mocking framework
- **FluentAssertions** — readable assertions
- Custom `AsyncQueryHelper` for EF Core async query support in tests

---

## Deployment

The application is designed to be deployed to **Microsoft Azure App Service**.

### Environment Configuration

For production deployment, override sensitive settings through Azure App Service Configuration (environment variables) rather than `appsettings.json`:

```
ConnectionStrings__DefaultConnection = <your-production-connection-string>
AdminSettings__Password = <strong-production-password>
AdminSettings__Email = <production-admin-email>
```

### Steps
1. Create an Azure App Service
2. Create an Azure SQL Database
3. Configure connection string in Azure App Service settings
4. Deploy via Visual Studio Publish or GitHub Actions
5. Run migrations on the production database

---

## Possible Future Improvements

- Product reviews and ratings system
- Online payment integration
- Advanced search with price range filtering
- Product recommendations based on order history
- Email notifications for orders
- React frontend consuming the Web API
- Azure Blob Storage for product images
- Performance monitoring and analytics dashboard
