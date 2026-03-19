# WealthTrack Portfolio Manager

## What We Are Doing In This Project

We are building an ASP.NET Core MVC application that helps a user manage a portfolio of market assets such as stocks and ETFs.

The application allows users to:

- View all investments
- Add a new investment with validation
- Edit investment details
- View investment details
- Delete investments

This project demonstrates:

- MVC architecture (Model, View, Controller)
- Entity Framework Core for data persistence
- ViewModel-based form handling and validation
- Manual mapping from ViewModel to domain model

## Assignment Mapping

### 1) Data Model

Implemented through Investment model with fields:

- Id
- TickerSymbol
- AssetName
- PurchasePrice
- Quantity
- PurchaseDate

### 2) ViewModel

Implemented through InvestmentCreateViewModel with:

- TickerSymbol validation
- Price validation
- Quantity validation
- Computed TotalValue

### 3) Database Configuration

- EF Core DbContext is configured in Program startup
- SQL Server connection string is set in appsettings.json
- Initial migration exists and database update can be applied

### 4) Controller Logic

InvestmentsController contains:

- Index (list all investments)
- Create GET (returns create view with ViewModel)
- Create POST (validates ViewModel, maps to model, saves to DB)
- Edit, Details, Delete actions from scaffolded CRUD flow

### 5) Mapping Logic

Create POST performs manual mapping:

- TickerSymbol from ViewModel
- PurchasePrice from Price
- Quantity from ViewModel
- PurchaseDate set to current time

## Recent Fixes Applied

- Fixed Create view model binding issue by keeping only one model directive for InvestmentCreateViewModel.
- Added Investments link in shared layout navigation so it is accessible directly from the top menu.
- Added explicit decimal precision configuration for PurchasePrice in DbContext.

## Tech Stack

- .NET 8
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server (Local/Express)
- Razor Views + Bootstrap

## Project Structure

- Controllers: MVC controllers including InvestmentsController
- Models: Domain models including Investment
- ViewModels: View-specific models including InvestmentCreateViewModel
- Data: EF Core DbContext
- Views: Razor UI pages
- Migrations: EF Core migration files

## Prerequisites

- .NET 8 SDK
- SQL Server instance (LocalDB or SQL Express)
- EF Core tools

Install EF Core tools globally if needed:

dotnet tool install --global dotnet-ef

## Configuration

Update connection string in appsettings.json if your SQL Server instance name differs:

ConnectionStrings:
WealthTrackPortfolioManagerContext: Data Source=YOUR_SERVER;Initial Catalog=DAY60DB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True

## Run The Project

From the project folder:

dotnet restore
dotnet build
dotnet ef database update
dotnet run

Then open the displayed local URL in the browser.

## Typical Workflow

1. Open Investments from the top navigation.
2. Use Create to add an investment.
3. Validate that data appears on Index.
4. Edit/Delete as needed.

## Notes

- Create form uses ViewModel validation attributes.
- EF migration files are already present in Migrations folder.
- If database connectivity fails, verify your server name and SQL instance availability.
