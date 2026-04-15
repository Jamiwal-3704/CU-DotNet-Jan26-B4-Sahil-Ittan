**📘 Assignment: End-to-End Azure Web App Deployment**

**Title:** Deploy Web API with EF Core + Azure SQL + MVC Client on Azure

---

**🎯 Objective**

Build and deploy a complete cloud-based solution using:

* .NET Core Web API
* Entity Framework Core
* Azure SQL Database
* ASP.NET Core MVC
* Azure App Service

---

**🧱 Architecture Overview**

MVC App (Frontend)

```
↓ (HTTP calls)
```

Web API (App Service)

```
↓
```

Azure SQL Database

---

**📝 Functional Requirement**

Create a **Student Management System**

**API Features:**

* Get all students
* Get student by ID
* Add student
* Update student
* Delete student

**MVC App:**

* Display list of students
* Add / Edit / Delete using API

---

**🔨 Part 1: Create Web API with EF Core**

**Tasks:**

1. Create ASP.NET Core Web API project
2. Install EF Core packages
3. Create Student model:
   * Id
   * Name
   * Age
   * Course
     [![1776225852149](https://github.com/D4RKCRY09/CU-DotNet-Jan26-B4/raw/main/Assignments/Day81Assignment/image/Day81/1776225852149.png)](https://github.com/D4RKCRY09/CU-DotNet-Jan26-B4/blob/main/Assignments/Day81Assignment/image/Day81/1776225852149.png)
4. Create DbContext
   [![1776225885872](https://github.com/D4RKCRY09/CU-DotNet-Jan26-B4/raw/main/Assignments/Day81Assignment/image/Day81/1776225885872.png)](https://github.com/D4RKCRY09/CU-DotNet-Jan26-B4/blob/main/Assignments/Day81Assignment/image/Day81/1776225885872.png)
5. Configure connection string (local SQL first)
6. Add Migration & Update Database
7. Implement CRUD API

---

**☁️ Part 2: Create Azure SQL Database**

**Tasks:**

1. Go to Azure Portal
2. Create:

   * SQL Server (logical server)
   * Database

   [![1776226031791](https://github.com/D4RKCRY09/CU-DotNet-Jan26-B4/raw/main/Assignments/Day81Assignment/image/Day81/1776226031791.png)](https://github.com/D4RKCRY09/CU-DotNet-Jan26-B4/blob/main/Assignments/Day81Assignment/image/Day81/1776226031791.png)
3. Configure:

   * Firewall → Allow client IP
   * Authentication (SQL login)
4. Copy connection string

---

**🔐 Part 3: Secure Connection String**

Update in API project:

"ConnectionStrings": {

"DefaultConnection": "Your Azure SQL Connection String"

}

[![1776236464529](https://github.com/D4RKCRY09/CU-DotNet-Jan26-B4/raw/main/Assignments/Day81Assignment/image/Day81/1776236464529.png)](https://github.com/D4RKCRY09/CU-DotNet-Jan26-B4/blob/main/Assignments/Day81Assignment/image/Day81/1776236464529.png)

👉 Best Practice:

* Use **Azure Key Vault** (optional advanced task)

---

**🚀 Part 4: Deploy Web API to Azure**

**Tasks:**

1. Create App Service (Web App)
2. Publish API from Visual Studio:
   * Right-click project → Publish
   * Select Azure → App Service
     [![1776236541388](https://github.com/D4RKCRY09/CU-DotNet-Jan26-B4/raw/main/Assignments/Day81Assignment/image/Day81/1776236541388.png)](https://github.com/D4RKCRY09/CU-DotNet-Jan26-B4/blob/main/Assignments/Day81Assignment/image/Day81/1776236541388.png)
3. Configure:
   * App Settings → Connection String
   * Runtime: .NET
4. Test API:

---

**🖥️ Part 5: Create MVC Application**

**Tasks:**

1. Create ASP.NET Core MVC project
2. Create Student ViewModel
   [![1776236574773](https://github.com/D4RKCRY09/CU-DotNet-Jan26-B4/raw/main/Assignments/Day81Assignment/image/Day81/1776236574773.png)](https://github.com/D4RKCRY09/CU-DotNet-Jan26-B4/blob/main/Assignments/Day81Assignment/image/Day81/1776236574773.png)
3. Call API using:
   * HttpClient
     [![1776236620070](https://github.com/D4RKCRY09/CU-DotNet-Jan26-B4/raw/main/Assignments/Day81Assignment/image/Day81/1776236620070.png)](https://github.com/D4RKCRY09/CU-DotNet-Jan26-B4/blob/main/Assignments/Day81Assignment/image/Day81/1776236620070.png)
4. Implement:
   * Index (list)
   * Create
   * Edit
   * Delete
5. Use API URL (deployed one)

---

**🌐 Part 6: Deploy MVC App**

**Tasks:**

1. Create second App Service
2. Publish MVC app
3. Update API base URL in MVC app

---

**🧪 Testing Checklist**

* API accessible via browser/Postman
* MVC app loads data from API
* CRUD operations working
* Data persists in Azure SQL
