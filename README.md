# LuminousStudio

LuminousStudio is a web application for browsing and ordering Tiffany lamps. It is built with ASP.NET Core MVC and provides a modern interface for viewing products, filtering them by style and manufacturer, managing a shopping cart, placing orders, and administering the system through a role-based dashboard.

## Overview

The application is designed with a clear separation of responsibilities and includes both a public product catalog and protected functionality for authenticated users and administrators. Visitors can browse the available Tiffany lamps, view product details, and explore informational pages such as About Us and Contacts. Registered users can add products to a shopping cart, place orders, and review their personal order history. Administrators have access to additional management features such as product maintenance, user management, order overview, and site statistics.

## Features

LuminousStudio includes user registration and login, role-based access for guests, clients, and administrators, a product catalog for Tiffany lamps, filtering by lamp style and manufacturer, a product details page, shopping cart functionality, order creation and order history, administrative CRUD operations for products, an admin overview of all orders, admin user management, and a statistics dashboard.

## Technologies Used

The project is built with C#, ASP.NET Core MVC, Entity Framework Core, ASP.NET Core Identity, SQL Server, Razor Views, HTML, CSS, Bootstrap, and JavaScript.

## Project Structure

The solution is organized into three layers. The LuminousStudio project is the presentation layer, LuminousStudio.Core contains the business logic, and LuminousStudio.Infrastructure is responsible for data access. This structure helps keep the code organized, easier to maintain, and easier to extend.

## Main Entities

The main entities used in the application are:

- ApplicationUser
- TiffanyLamp
- Manufacturer
- LampStyle
- Order
- ShoppingCart

## Main Pages

The main pages in the application include:

- Home
- About Us
- Contacts
- Tiffany Lamps
- Shopping Cart
- My Orders
- Clients
- Statistics

## Getting Started

To run the project locally, first clone the repository:

git clone https://github.com/RayaSergieva/LuminousStudio

Then open the solution in Visual Studio 2022 or a newer compatible version.

After that, configure the database connection in appsettings.json by updating the DefaultConnection value so it matches your local SQL Server setup. A typical example looks like this:

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=LuminousStudioDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  }
}

If your SQL Server uses a different instance name, replace Server=. with your actual server or instance.

Once the connection string is configured, restore the project dependencies if necessary by running:

dotnet restore

After restoring the packages, apply the database migrations to create or update the database. You can do this either from Package Manager Console with:

Update-Database

or with the .NET CLI using:

dotnet ef database update

When the database is ready, start the application from Visual Studio by pressing F5 for debugging or Ctrl + F5 to run without debugging. The project will then open in your browser.

## Default Admin Account

On the first run, the application seeds the required roles and creates a default administrator account.

Default administrator credentials:

- Username: admin
- Email: admin@admin.com
- Password: Admin123456

This account can be used to access the administrative features of the system.

## Access Levels

### Guest

A guest can browse products, view product details, filter lamps by style and manufacturer, and access the public pages of the site.

### Registered User

A registered user can log in, add products to the shopping cart, place orders, and review personal orders.

### Administrator

An administrator can create, edit, and delete products, view all orders, manage users, and access the statistics dashboard.

## Validation and Security

The application includes both server-side and client-side validation. Server-side validation is implemented through Data Annotations and controller validation checks, while client-side validation is enabled through Razor validation helpers and validation scripts. Authentication and authorization are handled with ASP.NET Core Identity, and access to protected pages and actions is controlled through user roles.

## Notes

The application uses Entity Framework Core with SQL Server. The database is managed through migrations, and initial data such as roles, the administrator account, lamp styles, and manufacturers are seeded automatically when the application starts.

## Possible Future Improvements

Possible future improvements include product reviews and ratings, online payment integration, advanced search and filtering, product recommendations, expanded administrative tools, and improved user profile features.
