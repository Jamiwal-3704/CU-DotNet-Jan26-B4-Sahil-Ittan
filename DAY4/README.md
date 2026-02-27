# TrainingPortal

A simple ASP.NET Core MVC web application for college training programs, demonstrating fundamental MVC architecture concepts including Controllers, Action Methods, Views, and Navigation.

## 📋 Table of Contents
- [Overview](#overview)
- [Business Scenario](#business-scenario)
- [Functional Requirements](#functional-requirements)
- [Technologies Used](#technologies-used)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [Implementation Guide](#implementation-guide)
- [Learning Objectives](#learning-objectives)
- [Request Flow](#request-flow)
- [Screenshots](#screenshots)
- [Contributing](#contributing)

## Overview

TrainingPortal is a beginner-friendly ASP.NET Core MVC application designed to help new learners understand the core building blocks of the MVC pattern. The application serves as a simple training portal for a college, displaying information about training offerings with clean navigation between sections.

## Business Scenario

A college requires a simple Training Portal to display information related to its training offerings. The portal enables users to navigate between different sections including:
- **Home** - Welcome page and overview
- **Courses** - List of available training courses
- **Contact** - Contact information and inquiry form

Each section follows proper MVC conventions and implements clean navigation without hard-coded URLs.

## Functional Requirements

✅ Create an ASP.NET Core MVC application named 'TrainingPortal'  
✅ Implement a controller to manage training-related pages  
✅ Create action methods for Home, Courses, and Contact pages  
✅ Design separate views for each action method  
✅ Configure navigation links using Tag Helpers  
✅ Ensure proper routing for URL mapping  

## Technologies Used

- **Framework**: ASP.NET Core MVC
- **Language**: C#
- **Frontend**: Razor Views, HTML5, CSS3
- **Tooling**: Tag Helpers, Routing Middleware

## Project Structure

```
TrainingPortal/
├── Controllers/
│   └── TrainingController.cs
├── Views/
│   ├── Training/
│   │   ├── Index.cshtml (Home)
│   │   ├── Courses.cshtml
│   │   └── Contact.cshtml
│   ├── Shared/
│   │   └── _Layout.cshtml
│   └── _ViewStart.cshtml
├── wwwroot/
│   ├── css/
│   │   └── site.css
│   └── js/
├── appsettings.json
└── Program.cs
```

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (6.0 or later)
- [Visual Studio](https://visualstudio.microsoft.com/) 2022 or VS Code
- Basic understanding of C# and HTML

### Installation

1. **Create new project**
   ```bash
   dotnet new mvc -n TrainingPortal
   cd TrainingPortal
   ```

2. **Build and run**
   ```bash
   dotnet build
   dotnet run
   ```

3. **Navigate to application**
   ```
   https://localhost:5001
   ```

## Implementation Guide

### 1. Create the Controller

Create `Controllers/TrainingController.cs`:

```csharp
using Microsoft.AspNetCore.Mvc;

namespace TrainingPortal.Controllers
{
    public class TrainingController : Controller
    {
        // GET: /Training/
        public IActionResult Index()
        {
            ViewData["Title"] = "Home";
            return View();
        }

        // GET: /Training/Courses
        public IActionResult Courses()
        {
            ViewData["Title"] = "Courses";
            return View();
        }

        // GET: /Training/Contact
        public IActionResult Contact()
        {
            ViewData["Title"] = "Contact";
            return View();
        }
    }
}
```

### 2. Create Views

#### Index.cshtml (Home Page)
Create `Views/Training/Index.cshtml`:

```html
@{
    ViewData["Title"] = "Home";
}

<div class="text-center">
    <h1 class="display-4">Welcome to Training Portal</h1>
    <p>Your gateway to professional development and skill enhancement.</p>
</div>

<div class="row mt-4">
    <div class="col-md-4">
        <h3>About Our Training</h3>
        <p>We offer comprehensive training programs designed to equip students with industry-relevant skills.</p>
    </div>
    <div class="col-md-4">
        <h3>Why Choose Us</h3>
        <ul>
            <li>Expert instructors</li>
            <li>Hands-on projects</li>
            <li>Industry-recognized certificates</li>
        </ul>
    </div>
    <div class="col-md-4">
        <h3>Get Started</h3>
        <p>Browse our courses and enroll today to begin your learning journey.</p>
        <a asp-controller="Training" asp-action="Courses" class="btn btn-primary">View Courses</a>
    </div>
</div>
```

#### Courses.cshtml
Create `Views/Training/Courses.cshtml`:

```html
@{
    ViewData["Title"] = "Courses";
}

<h1>Our Training Courses</h1>

<div class="row mt-4">
    <div class="col-md-4 mb-3">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title">Web Development</h5>
                <p class="card-text">Learn ASP.NET Core MVC, JavaScript, and modern web technologies.</p>
                <p><strong>Duration:</strong> 8 weeks</p>
                <a href="#" class="btn btn-primary">Learn More</a>
            </div>
        </div>
    </div>
    
    <div class="col-md-4 mb-3">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title">Data Science</h5>
                <p class="card-text">Master Python, SQL, and machine learning fundamentals.</p>
                <p><strong>Duration:</strong> 10 weeks</p>
                <a href="#" class="btn btn-primary">Learn More</a>
            </div>
        </div>
    </div>
    
    <div class="col-md-4 mb-3">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title">Cloud Computing</h5>
                <p class="card-text">Azure fundamentals and cloud architecture patterns.</p>
                <p><strong>Duration:</strong> 6 weeks</p>
                <a href="#" class="btn btn-primary">Learn More</a>
            </div>
        </div>
    </div>
</div>
```

#### Contact.cshtml
Create `Views/Training/Contact.cshtml`:

```html
@{
    ViewData["Title"] = "Contact";
}

<h1>Contact Us</h1>

<div class="row mt-4">
    <div class="col-md-6">
        <h3>Get in Touch</h3>
        <form>
            <div class="form-group mb-3">
                <label for="name">Name</label>
                <input type="text" class="form-control" id="name" placeholder="Enter your name">
            </div>
            <div class="form-group mb-3">
                <label for="email">Email</label>
                <input type="email" class="form-control" id="email" placeholder="Enter your email">
            </div>
            <div class="form-group mb-3">
                <label for="message">Message</label>
                <textarea class="form-control" id="message" rows="3" placeholder="Your message"></textarea>
            </div>
            <button type="submit" class="btn btn-primary">Send Message</button>
        </form>
    </div>
    
    <div class="col-md-6">
        <h3>Contact Information</h3>
        <address>
            <strong>Training Portal</strong><br>
            123 College Street<br>
            City, State 12345<br>
            <abbr title="Phone">P:</abbr> (123) 456-7890
        </address>
        
        <h4>Office Hours</h4>
        <p>Monday - Friday: 9:00 AM - 5:00 PM<br>
        Saturday: 10:00 AM - 2:00 PM<br>
        Sunday: Closed</p>
    </div>
</div>
```

### 3. Configure Navigation

Update `Views/Shared/_Layout.cshtml` to use Tag Helpers:

```html
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>@ViewData["Title"] - TrainingPortal</title>
    <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.min.css" />
    <link rel="stylesheet" href="~/css/site.css" asp-append-version="true" />
</head>
<body>
    <header>
        <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
            <div class="container-fluid">
                <a class="navbar-brand" asp-controller="Training" asp-action="Index">TrainingPortal</a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                    <ul class="navbar-nav flex-grow-1">
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-controller="Training" asp-action="Index">Home</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-controller="Training" asp-action="Courses">Courses</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-controller="Training" asp-action="Contact">Contact</a>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    </header>
    
    <div class="container">
        <main role="main" class="pb-3">
            @RenderBody()
        </main>
    </div>

    <footer class="border-top footer text-muted">
        <div class="container">
            &copy; 2024 - TrainingPortal - <a asp-controller="Training" asp-action="Contact">Contact</a>
        </div>
    </footer>
    
    <script src="~/lib/jquery/dist/jquery.min.js"></script>
    <script src="~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
    @await RenderSectionAsync("Scripts", required: false)
</body>
</html>
```

### 4. Configure Routing

Update `Program.cs` (default MVC routing is already configured):

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Default MVC route pattern
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Training}/{action=Index}/{id?}");

app.Run();
```

## Learning Objectives

By completing this exercise, learners will understand:

1. **Role of Controllers and Action Methods**
   - How controllers handle HTTP requests
   - Action method return types (IActionResult, ViewResult)
   - Convention-based routing

2. **MVC View Organization**
   - View folder structure matching controller names
   - Shared layouts for consistent UI
   - ViewData and ViewBag for data passing

3. **Tag Helpers for Navigation**
   - `asp-controller` and `asp-action` attributes
   - Generating URLs without hard-coding
   - Maintaining links during refactoring

4. **Request Flow Understanding**
   - Browser → Routing → Controller → Action → View → Response
   - How URLs map to controller actions

## Request Flow

```
User Request (URL)                    Response (HTML)
       ↓                                      ↑
[Routing Middleware]                  [View Engine]
       ↓                                      ↑
/ → TrainingController          ←   Razor View (.cshtml)
       ↓                                      ↑
Index() Action Method            →   Returns View()
```

1. **Browser** requests `/Training/Courses`
2. **Routing** maps URL to `TrainingController.Courses()`
3. **Controller** processes request, prepares data
4. **View** renders HTML with data
5. **Response** sent back to browser

## Screenshots

### Home Page
*Welcome page with overview and quick navigation*

### Courses Page
*List of available training courses with descriptions*

### Contact Page
*Contact form and information*

## Contributing

This is a learning exercise. Feel free to:
- Add more pages (About, FAQ, etc.)
- Implement database integration
- Add user authentication
- Enhance UI with custom CSS
- Add form validation

## 📝 License

Educational purposes - Free to use for learning ASP.NET Core MVC.

---

**Happy Learning!** 🚀
