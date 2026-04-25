# 🏆 InsureTrust — Project Introduction & Startup Guide

## What Is InsureTrust?

**InsureTrust** is a **full-stack, production-grade Insurance Management Platform** built entirely with **C# / ASP.NET Core 8**. It simulates a real-world insurance company's digital system — from customer registration and policy buying to admin approval, claims processing, and real-time notifications.

> Think of it as a complete digital insurance office — customers apply online, admins approve from a dashboard, money moves into wallets on claim approval, and everyone gets color-coded notifications throughout.

---

## 🏗️ Architecture at a Glance

The project is split into **two separate .NET applications** that talk to each other over HTTPS:

```
┌──────────────────────────────────────────────────────────┐
│           InsureTrust.Web  (MVC, Port 7001 / 5101)        │
│     Razor Views → MVC Controllers → ApiClient (HTTP)      │
│            JWT token stored in server Session             │
└────────────────────────┬─────────────────────────────────┘
                         │  HTTPS + Bearer Token
                         ▼
┌──────────────────────────────────────────────────────────┐
│           InsureTrust.API  (REST API, Port 7000)          │
│   Controller → Service → Repository → DbContext → SQL DB  │
└──────────────────────────────────────────────────────────┘
```

| Project | Role | Port |
|---------|------|------|
| `InsureTrust.API` | Backend REST API (JSON responses) | **7000** |
| `InsureTrust.Web` | Frontend MVC (HTML pages shown in browser) | **7001 / 5101** |

---

## 🛠️ Technology Stack

| Layer | Technology |
|-------|-----------|
| Language | **C# 12** |
| Backend Framework | **ASP.NET Core 8 Web API** |
| Frontend Framework | **ASP.NET Core 8 MVC** (Razor Views) |
| ORM | **Entity Framework Core 8** |
| Database | **SQL Server** (LocalDB or Express) |
| Authentication | **JWT Bearer Tokens** |
| Password Hashing | **BCrypt.Net-Next** |
| API Documentation | **Swagger / OpenAPI** |
| Frontend Fonts | **DM Sans + DM Serif Display** (Google Fonts) |

---

## 👥 Two Types of Users

### 🧑 Customer
| Action | Where |
|--------|-------|
| Register with KYC document upload | `/Account/Register` |
| Login | `/Account/Login` |
| Browse & buy insurance policies | `/Policy/Buy` |
| View my policies, renew/edit/delete | `/Policy` |
| Submit insurance claim (12 documents) | `/Claim/Submit` |
| Raise a support ticket | `/Support/Create` |
| View color-coded notifications | `/NotificationMvc` |
| Use premium calculator (no login needed) | Dashboard |

### 🛡️ Admin
| Action | Where |
|--------|-------|
| Admin-only login (gold theme) | `/Account/AdminLogin` |
| View platform metrics dashboard | `/Admin/Dashboard` |
| Approve or reject pending policies | Admin Dashboard |
| Approve/deny claims + credit wallet | Admin Dashboard |
| Manage support ticket statuses | Admin Dashboard |
| **Create new policy types** (live preview) | `/Admin/CreatePolicyType` |
| **Edit existing policy types** (live preview) | `/Admin/EditPolicyType/{id}` |
| View all registered users | Admin Dashboard |

---

## ✨ Highlighted Features

- **Live Policy Type Preview** — Admin types while a card preview updates in real-time (no submit needed)
- **Auto DB Migration** — Database tables + seed data created automatically on first run
- **Color-coded Notifications** — 🟢 Green (approved), 🔴 Red (rejected), 🟡 Yellow (pending/action), 🔵 Blue (support update)
- **Wallet System** — Approved claims automatically credit INR to the customer's wallet
- **Dynamic Policy Forms** — Each policy type shows its own custom fields (nominee for life, reg. no. for vehicle, etc.)
- **12-Document Claim Tracker** — Live progress bar that turns green at 12/12 uploads
- **Premium Calculator** — Live JS calculation, no page reload, accessible without login
- **Custom ID Formats** — USR1001, POL2001, CLM4001, SUP3001, PAY5001

---

## 📋 Prerequisites (Install These First)

| Tool | Purpose | Check Command |
|------|---------|---------------|
| **.NET 8 SDK** | Run both projects | `dotnet --version` → `8.x.x` |
| **SQL Server** (LocalDB or Express) | Database | Part of Visual Studio install |
| **Visual Studio 2022** or **VS Code + C# Dev Kit** | IDE | — |
| **EF Core CLI** | Database migrations | `dotnet tool install --global dotnet-ef` |

---

## 🚀 How to Start the Project

### ⚡ FASTEST METHOD — One-Click PowerShell Script

Open PowerShell in the solution root folder and run:

```powershell
.\start-dev.ps1
```

This script will:
1. Kill any stale processes on the required ports
2. Open a **blue** PowerShell window → starts the **API** on port `7000`
3. Open a **magenta** PowerShell window → starts the **Web** on port `5101`

Then open your browser at: **https://localhost:5101**

> ⚠️ Wait for **both** windows to show `Now listening on:` before opening the browser.

---

### 🔧 METHOD 2 — Two Terminals (Manual)

**Terminal 1 (API):**
```bash
cd InsureTrust.API
dotnet run
```
Wait for: `Now listening on: https://localhost:7000`

**Terminal 2 (Web):**
```bash
cd InsureTrust.Web
dotnet run
```
Wait for: `Now listening on: https://localhost:7001` or `:5101`

Open browser: **https://localhost:7001**

---

### 🖥️ METHOD 3 — Visual Studio 2022

1. Open `InsureTrust.sln`
2. Right-click **Solution** → **Set Startup Projects**
3. Choose **Multiple startup projects**
4. Set both `InsureTrust.API` and `InsureTrust.Web` to **Start**
5. Press **F5**

---

## ⚙️ Configuration

### Database Connection (`InsureTrust.API/appsettings.json`)

Your project is **currently configured** for SQL Server Express on `msi\sqlexpress`:

```json
"DefaultConnection": "Server=msi\\sqlexpress;Database=InsureTrustDb;Trusted_Connection=True;Trust Server Certificate=True"
```

If you need to change it:

| SQL Server Type | Connection String |
|----------------|-------------------|
| LocalDB (default VS install) | `Server=(localdb)\\mssqllocaldb;Database=InsureTrustDb;Trusted_Connection=True;` |
| SQL Express | `Server=.\\SQLEXPRESS;Database=InsureTrustDb;Trusted_Connection=True;TrustServerCertificate=True` |
| Full SQL Server | `Server=localhost;Database=InsureTrustDb;User Id=sa;Password=YourPwd;TrustServerCertificate=True` |

> ✅ **Auto-migration is enabled** — the database and all tables are created automatically when the API starts for the first time. No manual `dotnet ef database update` needed!

---

## 🔑 Default Login Credentials

| Role | Email | Password |
|------|-------|----------|
| **Admin** | `admin@insuretrust.com` | `Admin@123` |
| **Customer** | Register at `/Account/Register` | Your choice |

---

## 🌐 Important URLs

| URL | What It Is |
|-----|-----------|
| `https://localhost:5101` | Main website (homepage) |
| `https://localhost:5101/Account/Login` | Customer login |
| `https://localhost:5101/Account/AdminLogin` | Admin login (gold theme) |
| `https://localhost:5101/Account/Register` | New customer registration |
| `https://localhost:7000/swagger` | API docs (Swagger UI) |

---

## 🧪 Quick Test Flow

### As a Customer:
1. Go to `/Account/Register` → fill name, email, PAN (`ABCDE1234F`), upload any PDF
2. Login → land on **Customer Dashboard**
3. Click **Buy Policy** → pick *Term Life* → fill form → submit
4. Check **Notifications** → see Yellow "pending approval" notification
5. Check **My Policies** → status is *Pending*

### As Admin (in incognito window):
1. Go to `/Account/AdminLogin` → login with `admin@insuretrust.com / Admin@123`
2. See **Admin Dashboard** → pending policies section
3. Click ✅ **Approve** on the customer's policy
4. Switch back to customer session — policy is now **Active**, Green notification received!

---

## 📁 Project Folder Map (Quick Reference)

```
InsureTrust/
├── start-dev.ps1              ← ONE-CLICK startup script
├── InsureTrust.sln            ← Visual Studio solution file
│
├── InsureTrust.API/           ← BACKEND (REST API)
│   ├── Controllers/           ← Auth, Policy, Claim, Support, Notification, Payment
│   ├── Services/              ← All business logic
│   ├── Repositories/          ← All database access
│   ├── Models/                ← Database entity classes
│   ├── DTOs/                  ← Request/response objects (never expose entities)
│   ├── Data/AppDbContext.cs   ← EF Core + seed data (admin + 7 policy types)
│   ├── Helpers/               ← JWT generation, ID number generators
│   ├── Exceptions/            ← Custom exceptions + global error middleware
│   └── appsettings.json       ← ⚠️ DB connection string is here
│
└── InsureTrust.Web/           ← FRONTEND (MVC Razor)
    ├── Controllers/           ← MVC controllers that call the API
    ├── Views/                 ← All Razor HTML pages
    │   ├── Home/              ← Landing page
    │   ├── Account/           ← Login, Register, AdminLogin
    │   ├── Dashboard/         ← Customer dashboard + calculator
    │   ├── Policy/            ← Buy, Purchase, My Policies
    │   ├── Claim/             ← Submit claim, My claims
    │   ├── Support/           ← Tickets
    │   ├── Admin/             ← Admin dashboard + policy type management
    │   └── NotificationMvc/   ← Notification center
    ├── Services/ApiClient.cs  ← All HTTP calls to the API centralized here
    └── wwwroot/               ← CSS design system + JavaScript
```

---

## 🎨 Design System

- **Neo-Brutalism aesthetic** with dark navy navbar and gold accents
- **Light / Dark theme toggle** with smooth CSS transitions
- Fonts: **DM Sans** (body) + **DM Serif Display** (headings)
- Defined in `InsureTrust.Web/wwwroot/css/site.css`
- Dynamic JS: live policy preview, premium calculator, document upload tracker

---

> [!TIP]
> Use the `start-dev.ps1` script — it's the fastest and most reliable way to launch both projects. It handles port cleanup automatically.

> [!IMPORTANT]
> The API must be running **before** the Web project, or the website will show API connection errors. The script handles the start order for you.
