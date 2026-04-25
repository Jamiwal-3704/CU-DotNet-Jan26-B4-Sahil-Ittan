# 🏆 Insure Trust — Complete Insurance Management System

> **ASP.NET Core 8 | C# | MVC | Web API | Entity Framework Core | SQL Server | JWT Authentication**

A full-stack, production-grade **Insurance Management Platform** built with clean N-tier architecture.
The system manages the complete insurance lifecycle — registration, policy purchase, admin approval,
renewals, claims, support tickets, and real-time notifications.

---

## 📋 Table of Contents

1. [Project Overview](#1-project-overview)
2. [Technology Stack](#2-technology-stack)
3. [Architecture](#3-architecture)
4. [Complete Folder Structure](#4-complete-folder-structure)
5. [All Features](#5-all-features)
6. [Database Schema](#6-database-schema)
7. [API Endpoints](#7-api-endpoints)
8. [Prerequisites](#8-prerequisites)
9. [Step-by-Step Setup](#9-step-by-step-setup)
10. [Running the Application](#10-running-the-application)
11. [Default Credentials](#11-default-credentials)
12. [Testing Guide](#12-testing-guide)
13. [Business Rules](#13-business-rules)
14. [Notification System](#14-notification-system)
15. [ID Generation Logic](#15-id-generation-logic)
16. [Exception Handling](#16-exception-handling)
17. [Seeded Data](#17-seeded-data)
18. [NuGet Packages](#18-nuget-packages)
19. [Troubleshooting](#19-troubleshooting)

---

## 1. Project Overview

**Insure Trust** is a complete insurance management system for two actor types:

| Actor        | Capabilities                                                                 |
|-------------|------------------------------------------------------------------------------|
| **Customer** | Register, buy policies, renew/edit/delete, submit claims, support tickets, notifications |
| **Admin**    | Create/edit policy types (real-time visible to customers), approve/reject policies, manage claims, support tickets, view all users |

---

## 2. Technology Stack

| Layer              | Technology                              |
|--------------------|-----------------------------------------|
| Backend Framework  | ASP.NET Core 8 Web API                  |
| Frontend Framework | ASP.NET Core 8 MVC (Razor Views)        |
| Language           | C# 12                                   |
| ORM                | Entity Framework Core 8                 |
| Database           | SQL Server / SQL Server LocalDB         |
| Authentication     | JWT Bearer Tokens                       |
| Password Hashing   | BCrypt.Net-Next                         |
| API Documentation  | Swagger / OpenAPI (Swashbuckle)         |
| HTTP Client        | IHttpClientFactory                      |
| Session Storage    | ASP.NET Core Session (server-side)      |
| Fonts              | DM Sans + DM Serif Display (Google)     |

---

## 3. Architecture

```
┌──────────────────────────────────────────────────────────┐
│               InsureTrust.Web (MVC)                       │
│            ASP.NET Core MVC — Port 7001                   │
│                                                           │
│   Razor Views → MVC Controllers → ApiClient (HttpClient) │
│                                        │                  │
│                    JWT Token stored in Session            │
└────────────────────────────┬─────────────────────────────┘
                             │  HTTPS + Bearer Token
                             ▼
┌──────────────────────────────────────────────────────────┐
│               InsureTrust.API (REST)                      │
│            ASP.NET Core Web API — Port 7000               │
│                                                           │
│   Controller → Service → Repository → DbContext → SQL DB  │
└──────────────────────────────────────────────────────────┘
```

### Design Patterns Used
- **Repository Pattern** — all DB access isolated in repository classes
- **Service Layer** — all business logic in services, zero logic in controllers
- **DTO Separation** — entities never directly exposed through API
- **Dependency Injection** — all components registered via DI container
- **Global Exception Middleware** — consistent JSON error responses
- **N-Tier Architecture** — Controller → Service → Repository → DbContext

---

## 4. Complete Folder Structure

```
InsureTrust/
├── InsureTrust.sln
├── README.md
│
├── InsureTrust.API/                       ← Backend REST API
│   ├── InsureTrust.API.csproj
│   ├── Program.cs                         ← DI, JWT config, CORS, Swagger, auto-migrate
│   ├── appsettings.json                   ← DB connection string + JWT settings
│   ├── Properties/launchSettings.json     ← Runs on port 7000
│   │
│   ├── Controllers/Controllers.cs         ← AuthController
│   │                                         PolicyController
│   │                                         ClaimController
│   │                                         SupportController
│   │                                         NotificationController
│   │                                         PaymentController
│   │                                         CalculatorController
│   │
│   ├── Models/Models.cs                   ← User, PolicyType, PolicyTerm,
│   │                                         PolicyRequiredField, UserPolicy,
│   │                                         Claim, SupportQuery,
│   │                                         Notification, Payment
│   │
│   ├── DTOs/AllDTOs.cs                    ← RegisterDto, LoginDto, UserDto,
│   │                                         PolicyTypeDto, CreatePolicyTypeDto,
│   │                                         CreatePolicyDto, PolicyDto,
│   │                                         ApprovePolicyDto, EditPolicyDto,
│   │                                         SubmitClaimDto, ClaimDto, UpdateClaimDto,
│   │                                         CreateSupportQueryDto, SupportQueryDto,
│   │                                         UpdateSupportStatusDto, NotificationDto,
│   │                                         CalculatorRequestDto, CalculatorResultDto,
│   │                                         InitiatePaymentDto, PaymentDto
│   │
│   ├── Data/AppDbContext.cs               ← EF Core DbContext + seed data
│   │                                         (admin user + 7 policy types + terms + fields)
│   │
│   ├── Repositories/Repositories.cs       ← IUserRepository + UserRepository
│   │                                         IPolicyRepository + PolicyRepository
│   │                                         IClaimRepository + ClaimRepository
│   │                                         ISupportRepository + SupportRepository
│   │                                         INotificationRepository + NotificationRepository
│   │                                         IPaymentRepository + PaymentRepository
│   │
│   ├── Services/Services.cs               ← IAuthService + AuthService
│   │                                         IPolicyService + PolicyService
│   │                                         IClaimService + ClaimService
│   │                                         ISupportService + SupportService
│   │                                         INotificationService + NotificationService
│   │                                         ICalculatorService + CalculatorService
│   │                                         IPaymentService + PaymentService
│   │
│   ├── Exceptions/Exceptions.cs           ← NotFoundException (404)
│   │                                         BadRequestException (400)
│   │                                         UnauthorizedException (401)
│   │                                         ForbiddenException (403)
│   │                                         GlobalExceptionMiddleware
│   │
│   └── Helpers/Helpers.cs                 ← JwtHelper (token generation)
│                                             NumberGenerators (USR/POL/CLM/SUP/PAY)
│
└── InsureTrust.Web/                       ← Frontend MVC Application
    ├── InsureTrust.Web.csproj
    ├── Program.cs                         ← Session, HttpClient, DI setup
    ├── appsettings.json                   ← ApiBaseUrl = https://localhost:7000/
    ├── Properties/launchSettings.json     ← Runs on port 7001
    │
    ├── Controllers/Controllers.cs         ← HomeController
    │                                         AccountController (Login/Register/AdminLogin)
    │                                         DashboardController
    │                                         PolicyController
    │                                         ClaimController
    │                                         SupportController
    │                                         NotificationMvcController
    │                                         AdminController (⭐ includes Create/Edit PolicyType)
    │
    ├── Models/ViewModels.cs               ← All ViewModel classes
    │
    ├── Services/ApiClient.cs              ← Full HttpClient wrapper
    │                                         (all API calls centralized here)
    │
    ├── Views/
    │   ├── _ViewImports.cshtml            ← Tag helpers
    │   ├── _ViewStart.cshtml
    │   ├── Shared/
    │   │   └── _Layout.cshtml             ← Master page: navbar, notification bell,
    │   │                                     flash messages, footer
    │   ├── Home/
    │   │   └── Index.cshtml               ← Landing page with hero section
    │   ├── Account/
    │   │   ├── Login.cshtml               ← Customer login form
    │   │   ├── AdminLogin.cshtml          ← Admin-only login (gold theme)
    │   │   └── Register.cshtml            ← Registration with KYC file upload
    │   ├── Dashboard/
    │   │   └── Index.cshtml               ← Customer metrics, policies,
    │   │                                     notifications, premium calculator
    │   ├── Policy/
    │   │   ├── Buy.cshtml                 ← Browse all policy types (real-time from DB)
    │   │   ├── Purchase.cshtml            ← Purchase form (dynamic fields per type)
    │   │   └── Index.cshtml               ← My policies with renew/edit/delete
    │   ├── Claim/
    │   │   ├── Index.cshtml               ← Claims list
    │   │   └── Submit.cshtml              ← Submit with 12-document upload tracker
    │   ├── Support/
    │   │   ├── Index.cshtml               ← Support tickets list
    │   │   └── Create.cshtml              ← New ticket form
    │   ├── Admin/
    │   │   ├── Dashboard.cshtml           ← Full admin control panel
    │   │   ├── CreatePolicyType.cshtml    ← ⭐ NEW: Create policy with live preview
    │   │   └── EditPolicyType.cshtml      ← ⭐ NEW: Edit policy with live preview
    │   └── NotificationMvc/
    │       └── Index.cshtml               ← All notifications with mark read
    │
    └── wwwroot/
        ├── css/site.css                   ← Full premium design system
        │                                     (DM Sans + DM Serif Display fonts,
        │                                      CSS variables, dark navy navbar,
        │                                      gold accents, responsive grid)
        └── js/site.js                     ← Live preview updater,
                                              premium calculator,
                                              document upload tracker,
                                              modal helpers
```

---

## 5. All Features

### Customer Features

#### Registration (`/Account/Register`)
- Fields: Full name, email, password (min 6 chars), confirm password, phone number, PAN card
- PAN card format validation: 5 uppercase letters + 4 digits + 1 uppercase letter (e.g. ABCDE1234F)
- KYC document upload: PDF, JPG, or PNG
- File stored in `wwwroot/uploads/kyc/` with GUID filename
- Auto-generated UserNumber: `USR1001`, `USR1002`, ...
- Role auto-set to `Customer`, KycStatus set to `Verified` if document uploaded

#### Login (`/Account/Login`)
- JWT token issued on success
- Token decoded to extract Name, Role, UserId — stored in server Session
- Role-based redirect: Customer → `/Dashboard`, Admin → `/Admin/Dashboard`

#### Customer Dashboard (`/Dashboard`)
- **4 metric cards**: Total policies, Active policies, Wallet balance, Pending approvals
- Recent policies list (up to 4, with "View all" link)
- Recent notifications panel (last 5, color-coded dots)
- Quick action buttons: Buy Policy, Submit Claim, Get Support, Calculator
- Built-in **Premium Calculator** (see below)

#### Premium Calculator
- Available on customer dashboard — no separate page needed
- Inputs: Insurance type, age, coverage amount (₹), tenure (months)
- Live JavaScript calculation — updates as you type without page reload
- Formula: `monthly = coverageAmount × typeRate × ageFactor / 12`
- Age factor: `1 + (age - 30) × 0.02` — older = higher premium
- Shows both monthly premium and total premium
- **Accessible without login** for public estimation

#### Browse & Buy Policies (`/Policy/Buy`)
- Fetches all active policy types **live from the database** on every page load
- No caching — new policy types created by admin appear immediately
- Divided into **Personal Plans** and **Business Plans** sections
- Each card shows: icon, name, category, description, base premium, first coverage item
- Business plans highlighted with gold border

#### Purchase Policy (`/Policy/Purchase/{typeId}`)
- Dynamic form fields based on the selected policy type:
  - **Term Life**: Nominee name, nominee relation, date of birth
  - **Vehicle**: Registration number, make & model
  - **Home / Property**: Property address, property value
  - **Health**: Date of birth, pre-existing conditions
  - All types: Full name, email
- Package selection: ₹5,000 / ₹10,000 / ₹15,000 per month
- Tenure: 12, 24, 36, 48, or 60 months
- Payment method: UPI, Net Banking, Credit Card, Debit Card
- On submit: policy created with status **Pending**
- PolicyNumber generated: `POL2001`, `POL2002`, ...
- Dynamic fields saved as JSON in `DynamicFieldsJson` column
- Yellow notification sent: "Your {type} policy is pending admin approval"

#### My Policies (`/Policy`)
- Lists all policies for the logged-in user
- Status badge color: Pending=Yellow, Active=Green, Rejected=Red, Expired=Gray
- Shows: policy number, type, category, premium, tenure, purchase date, expiry date, days remaining
- **Renew**: One-click — extends expiry date by same tenure, Yellow notification sent
- **Edit**: Modal popup — change tenure or package amount, Yellow notification
- **Delete**: JavaScript confirm dialog — policy permanently removed

#### Submit Insurance Claim (`/Claim/Submit`)
- Only **Active** policies appear in the dropdown
- 12 mandatory documents with individual file upload buttons:
  1. Policy Number + Original Policy Document
  2. Duly Filled Claim Form (signed)
  3. Immediate Notification to Insurer
  4. KYC Documents
  5. Address Proof
  6. Bank Details
  7. Incident Evidence
  8. Police FIR
  9. Medical / Death Records
  10. Original Bills & Receipts
  11. Repair Estimates
  12. Witness Statements
- Upload tracker: "X of 12 documents uploaded" — turns green at 12
- Files stored in `wwwroot/uploads/claim-documents/`
- ClaimNumber generated: `CLM4001`, `CLM4002`, ...
- Yellow notification: "Claim submitted and under review"

#### My Claims (`/Claim`)
- Table showing: Claim #, Policy, Type, Status, Filed Date, Docs count, Payout amount
- Status: Pending / Approved / Denied

#### Support Queries (`/Support`)
- Create ticket: subject, description, optional file attachment
- TicketNumber: `SUP3001`, `SUP3002`, ...
- Status tracked: Pending → In Progress → Resolved
- Blue notification on each status update by admin

#### Notifications (`/NotificationMvc`)
- Bell icon in navbar with live unread count badge
- Color-coded dot per notification type
- **Mark individual** notification as read
- **Mark all** as read button
- Sorted newest first

---

### Admin Features

#### Admin Login (`/Account/AdminLogin`)
- Separate dedicated page with gold theme
- Only Role = `Admin` users can use this endpoint
- JWT issued and stored in session same as customer flow

#### Admin Dashboard (`/Admin/Dashboard`)
- **4 metric cards**: Total customers, Total policies, Pending approvals, Open tickets
- **Pending Policy Approvals** section: each entry shows policy #, type, customer name, email, package, one-click Approve/Reject buttons
- **Support Tickets** panel: shows up to 8 open tickets; inline status dropdown auto-submits on change
- **Claims Management** table: full list, Approve (enter ₹ amount) or Deny buttons for Pending claims
- **Policy Types Panel**: card grid of all policy types with Edit button on each
- **All Users** table: full list with UserNumber, name, email, role badge, KYC badge, join date, wallet balance

#### Create New Policy Type (`/Admin/CreatePolicyType`) ⭐ NEW
- **Full creation form**:
  - Policy name (shown to customers)
  - Category: Personal or Business
  - Description (shown on buy page card)
  - Coverage details (comma-separated list)
  - Icon picker: 14 options — shield, heart, car, home, building, users, settings, plane, umbrella, phone, laptop, money, fire, water
  - Base monthly premium (₹)
  - Min and max tenure months
- **Live Preview panel** (right side, sticky):
  - Updates in real-time as you type — no submit needed
  - Shows exactly the card customers will see on the Buy page
  - Icon, name, category, description, premium, first coverage item
- **Immediate effect**: once saved, policy type appears instantly on every customer's Buy Insurance page
- No app restart, no cache clearing required

#### Edit Existing Policy Type (`/Admin/EditPolicyType/{id}`) ⭐ NEW
- Pre-filled with existing values
- Same live preview panel
- Save updates the database and the change is immediately visible to all customers

#### Policy Approval / Rejection
- **Grant**: policy.Status → `Active`, Green notification to customer
- **Reject**: policy.Status → `Rejected`, admin remarks saved, Red notification to customer

#### Claim Management
- **Approve**: enter maturity payout amount → credited directly to customer's wallet balance, Green notification
- **Deny**: claim denied with admin remarks, Red notification

#### Support Ticket Management
- Status dropdown per ticket: Pending / In Progress / Resolved
- Auto-submits on dropdown change
- Blue notification sent to customer on every update

---

## 6. Database Schema

### Users
| Column | Type | Notes |
|--------|------|-------|
| Id | int PK | Auto-increment |
| UserNumber | string | Unique — USR1001 |
| Name | string | Full name |
| Email | string | Unique |
| PasswordHash | string | BCrypt hashed, never plain text |
| PhoneNo | string | |
| PanCard | string | Stored uppercase |
| KycDocumentPath | string | Relative path to uploaded file |
| KycStatus | string | Pending / Verified |
| Role | string | Customer / Admin |
| Balance | decimal(18,2) | Wallet — default 0 |
| CreatedAt | DateTime | UTC |

### PolicyTypes
| Column | Type | Notes |
|--------|------|-------|
| Id | int PK | |
| Name | string | e.g. "Term Life" |
| Category | string | Personal / Business |
| Description | string | Buy page text |
| Icon | string | Icon key (shield, heart, etc.) |
| IsActive | bool | False = hidden from customers |
| BaseMonthlyPremium | decimal(18,2) | |
| MinTenureMonths | int | |
| MaxTenureMonths | int | |
| CoverageDetails | string | Comma-separated |
| CreatedAt | DateTime | UTC |

### PolicyTerms
| Column | Type |
|--------|------|
| Id | int PK |
| PolicyTypeId | int FK → PolicyTypes |
| TermText | string |

### PolicyRequiredFields
| Column | Type | Notes |
|--------|------|-------|
| Id | int PK | |
| PolicyTypeId | int FK → PolicyTypes | |
| FieldName | string | e.g. "Nominee Name" |
| FieldType | string | text / date / file |
| IsMandatory | bool | |

### UserPolicies
| Column | Type | Notes |
|--------|------|-------|
| Id | int PK | |
| PolicyNumber | string | Unique — POL2001 |
| UserId | int FK → Users | |
| PolicyTypeId | int FK → PolicyTypes | |
| Status | string | Pending/Active/Rejected/Expired |
| PurchaseDate | DateTime | UTC |
| ExpiryDate | DateTime | UTC |
| Tenure | int | Months |
| PackageAmount | decimal(18,2) | 5000/10000/15000 |
| AdminRemarks | string | Set on rejection |
| DynamicFieldsJson | string | JSON — form data per type |

### Claims
| Column | Type | Notes |
|--------|------|-------|
| Id | int PK | |
| ClaimNumber | string | Unique — CLM4001 |
| UserPolicyId | int FK → UserPolicies | |
| ClaimStatus | string | Pending/Approved/Denied |
| ClaimDate | DateTime | UTC |
| MaturityAmount | decimal(18,2) | Credited to wallet on approval |
| DocumentsSubmitted | string | JSON array of file paths |
| AdminRemarks | string | |

### SupportQueries
| Column | Type | Notes |
|--------|------|-------|
| Id | int PK | |
| TicketNumber | string | Unique — SUP3001 |
| UserId | int FK → Users | |
| Subject | string | |
| Description | string | |
| AttachmentPath | string | Optional uploaded file |
| Status | string | Pending/InProgress/Resolved |
| CreatedAt | DateTime | UTC |
| UpdatedAt | DateTime | UTC |

### Notifications
| Column | Type | Notes |
|--------|------|-------|
| Id | int PK | |
| UserId | int FK → Users | |
| Title | string | |
| Message | string | |
| ColorCode | string | Green/Red/Yellow/Blue |
| IsRead | bool | |
| CreatedAt | DateTime | UTC |
| RelatedFeature | string | Policy/Claim/Support |

### Payments
| Column | Type | Notes |
|--------|------|-------|
| Id | int PK | |
| PaymentNumber | string | Unique — PAY5001 |
| UserId | int FK → Users | |
| UserPolicyId | int FK → UserPolicies | |
| Amount | decimal(18,2) | |
| PaymentDate | DateTime | UTC |
| PaymentMethod | string | UPI/NetBanking/Card |
| Status | string | Success/Failed/Pending |

---

## 7. API Endpoints

### Auth — `/api/auth`
| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| POST | `/register` | Public | Register with KYC file (multipart) |
| POST | `/login` | Public | Customer login → JWT |
| POST | `/admin-login` | Public | Admin login → JWT |
| GET | `/profile` | JWT | Current user profile |
| GET | `/users` | Admin | All users list |

### Calculator — `/api/calculator`
| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| POST | `/estimate` | Public | Monthly premium estimate |

### Policy — `/api/policy`
| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| GET | `/types` | Public | All active policy types |
| GET | `/types/{id}` | Public | Single policy type |
| POST | `/types` | Admin | Create new policy type |
| PUT | `/types/{id}` | Admin | Update policy type |
| GET | `/my-policies` | JWT | Logged-in user's policies |
| GET | `/all` | Admin | All policies (all users) |
| GET | `/pending` | Admin | Pending approval policies |
| POST | `/purchase` | JWT | Purchase new policy |
| PUT | `/approve/{policyId}` | Admin | Grant or Reject policy |
| POST | `/renew/{policyId}` | JWT | Renew active policy |
| PUT | `/edit/{policyId}` | JWT | Edit tenure or package |
| DELETE | `/{policyId}` | JWT | Delete policy |

### Claim — `/api/claim`
| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| GET | `/my-claims` | JWT | User's claims |
| GET | `/all` | Admin | All claims |
| POST | `/submit/{policyId}` | JWT | Submit claim with documents (multipart) |
| PUT | `/update/{claimId}` | Admin | Approve or Deny claim |

### Support — `/api/support`
| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| GET | `/my-queries` | JWT | User's tickets |
| GET | `/all` | Admin | All tickets |
| POST | `/submit` | JWT | New support ticket (multipart) |
| PUT | `/update/{ticketId}` | Admin | Update ticket status |

### Notification — `/api/notification`
| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| GET | `/` | JWT | All user notifications |
| GET | `/unread-count` | JWT | Count of unread |
| PUT | `/mark-read/{id}` | JWT | Mark one as read |
| PUT | `/mark-all-read` | JWT | Mark all as read |

### Payment — `/api/payment`
| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| POST | `/initiate` | JWT | Record payment |
| GET | `/history` | JWT | Payment history |

---

## 8. Prerequisites

1. **.NET 8 SDK** — https://dotnet.microsoft.com/download/dotnet/8
   - Check: `dotnet --version` → should show `8.x.x`

2. **SQL Server** — choose one:
   - **SQL Server LocalDB** (recommended — free, bundled with Visual Studio)
   - **SQL Server Express** — https://www.microsoft.com/sql-server/sql-server-downloads
   - **SQL Server Developer** — free for dev/test

3. **Visual Studio 2022** (recommended) or **VS Code** with C# Dev Kit

4. **EF Core CLI Tools**:
   ```
   dotnet tool install --global dotnet-ef
   ```

---

## 9. Step-by-Step Setup

### Step 1 — Extract the ZIP

Extract to any folder, for example `C:\Projects\InsureTrust\`

Confirm structure:
```
InsureTrust/
├── InsureTrust.sln
├── README.md
├── InsureTrust.API/
└── InsureTrust.Web/
```

### Step 2 — Configure Database

Edit `InsureTrust.API/appsettings.json`:

**SQL Server LocalDB** (Visual Studio default — recommended):
```json
"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=InsureTrustDb;Trusted_Connection=True;MultipleActiveResultSets=true"
```

**SQL Server Express**:
```json
"DefaultConnection": "Server=.\\SQLEXPRESS;Database=InsureTrustDb;Trusted_Connection=True;TrustServerCertificate=True"
```

**Full SQL Server with credentials**:
```json
"DefaultConnection": "Server=localhost;Database=InsureTrustDb;User Id=sa;Password=YourPassword;TrustServerCertificate=True"
```

### Step 3 — Restore Packages

Open terminal in the `InsureTrust/` folder:
```bash
dotnet restore
```

### Step 4 — Run Database Migration

```bash
cd InsureTrust.API
dotnet ef migrations add InitialCreate --output-dir Data/Migrations
dotnet ef database update
cd ..
```

This creates the database with all tables + seed data (admin user + 7 policy types).

> **Note:** If you skip this, migration runs automatically on first API startup.

### Step 5 — Trust Dev Certificate (once)

```bash
dotnet dev-certs https --trust
```

---

## 10. Running the Application

### Option A — Two Terminals

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
Wait for: `Now listening on: https://localhost:7001`

Open browser: **https://localhost:7001**

### Option B — Visual Studio 2022

1. Open `InsureTrust.sln`
2. Right-click Solution → **Set Startup Projects**
3. Select **Multiple startup projects**
4. Set both to **Start**
5. Press **F5**

### Useful URLs

| URL | Description |
|-----|-------------|
| https://localhost:7001 | Web application homepage |
| https://localhost:7001/Account/Login | Customer login |
| https://localhost:7001/Account/AdminLogin | Admin login |
| https://localhost:7001/Account/Register | New customer |
| https://localhost:7000/swagger | API Swagger UI |

---

## 11. Default Credentials

| Role | Email | Password |
|------|-------|----------|
| **Admin** | admin@insuretrust.com | Admin@123 |
| **Customer** | Register at `/Account/Register` | Your choice |

---

## 12. Testing Guide

### Customer Flow

1. Register at `/Account/Register`:
   - Name: `Rahul Sharma`
   - Email: `rahul@example.com`
   - Password: `Password@123`
   - PAN: `ABCDE1234F`
   - Upload any PDF as KYC

2. Login → redirected to **Customer Dashboard**

3. Click **"Buy Policy"** → browse all 7 (or more) policy types

4. Click **Term Life** → fill form → submit → see **Pending** status

5. Go to **Notifications** → see Yellow notification

6. Go to **Claims** → Submit Claim → upload 12 files

7. Go to **Support** → Create Ticket → fill subject/description

### Admin Flow

1. Open private/incognito window

2. Go to `/Account/AdminLogin` → login as `admin@insuretrust.com`

3. See **Dashboard** with pending policy count

4. Click **✓ Approve** on the customer's pending policy

5. Customer's policy status changes to **Active**, Green notification sent

6. Click **"+ Create New Policy Type"** in navbar:
   - Name: `Cyber Insurance`
   - Category: `Business`
   - Description: `Protect against cyber attacks`
   - Coverage: `Data breach, Ransomware, Business interruption`
   - Icon: laptop (💻)
   - Premium: `8000`
   - Watch live preview update as you type
   - Click **"Create & Publish"**

7. Switch to customer session → Buy Policy → new **Cyber Insurance** appears

8. Go back to Admin Dashboard → Claims section → approve with payout amount

---

## 13. Business Rules

| Rule | Detail |
|------|--------|
| Policy approval required | All new purchases start as Pending and need admin Grant |
| Valid package amounts | Only ₹5,000, ₹10,000, or ₹15,000 per month |
| Renewal eligibility | Only policies with Status = Active can be renewed |
| Claim eligibility | Only policies with Status = Active can be claimed |
| Claim documents | 12 files mandatory for claim submission |
| Maturity payout | On claim Approve, `maturityAmount` added to user.Balance |
| Support flow | Status moves: Pending → InProgress → Resolved |
| PAN format | Must be: 5 uppercase letters + 4 digits + 1 uppercase letter |
| Password minimum | At least 6 characters |
| Admin isolation | Admin cannot login via customer endpoint; customer cannot use admin endpoint |
| Delete authorization | Customer can only delete own policy; Admin can delete any |

---

## 14. Notification System

Notifications are created automatically inside service methods:

| Trigger | Color | Message |
|---------|-------|---------|
| Policy purchased | 🟡 Yellow | "Your {type} policy {number} is pending admin approval." |
| Policy approved | 🟢 Green | "Your {type} policy {number} is now active!" |
| Policy rejected | 🔴 Red | "Your {type} policy {number} was rejected. {remarks}" |
| Policy renewed | 🟡 Yellow | "Your {type} policy {number} renewed successfully." |
| Policy edited | 🟡 Yellow | "Your policy {number} has been updated." |
| Claim submitted | 🟡 Yellow | "Your claim {number} is under review." |
| Claim approved | 🟢 Green | "Claim {number} approved. ₹{amount} credited." |
| Claim denied | 🔴 Red | "Claim {number} denied. {remarks}" |
| Support updated | 🔵 Blue | "Ticket {number} status changed to {status}." |

---

## 15. ID Generation Logic

Generated after `SaveChanges()` so the database-assigned `Id` is known:

| Entity | Code | Example |
|--------|------|---------|
| User | `USR{(id + 1000):D4}` | USR1001, USR1002 |
| UserPolicy | `POL{(id + 2000):D4}` | POL2001, POL2002 |
| SupportQuery | `SUP{(id + 3000):D4}` | SUP3001, SUP3002 |
| Claim | `CLM{(id + 4000):D4}` | CLM4001, CLM4002 |
| Payment | `PAY{(id + 5000):D4}` | PAY5001, PAY5002 |

---

## 16. Exception Handling

`GlobalExceptionMiddleware` catches all unhandled exceptions and returns JSON:

```json
{
  "statusCode": 404,
  "message": "Policy not found.",
  "timestamp": "2026-04-06T10:30:00Z"
}
```

| Exception | HTTP Code |
|-----------|-----------|
| NotFoundException | 404 |
| BadRequestException | 400 |
| UnauthorizedException | 401 |
| ForbiddenException | 403 |
| Any other | 500 |

---

## 17. Seeded Data

Available immediately after running migrations:

### Admin User
| Field | Value |
|-------|-------|
| UserNumber | ADMIN001 |
| Email | admin@insuretrust.com |
| Password | Admin@123 |
| Role | Admin |
| KYC | Verified |

### 7 Policy Types
| # | Name | Category | Base Premium |
|---|------|----------|-------------|
| 1 | Term Life | Personal | ₹5,000/mo |
| 2 | Health | Personal | ₹3,000/mo |
| 3 | Vehicle | Personal | ₹2,000/mo |
| 4 | Home | Personal | ₹1,500/mo |
| 5 | Property | Business | ₹8,000/mo |
| 6 | Employee Group Benefits | Business | ₹15,000/mo |
| 7 | Engineering | Business | ₹12,000/mo |

### Policy Terms (seeded for Term Life, Health, Vehicle)
- Each type has 2–3 sample terms

### Required Fields (seeded)
- Term Life: Nominee Name, Nominee Relation, Date of Birth
- Health: Date of Birth, Existing Medical Conditions
- Vehicle: Registration Number, Make & Model
- Home: Property Address, Property Value

---

## 18. NuGet Packages

### InsureTrust.API
| Package | Version | Purpose |
|---------|---------|---------|
| Microsoft.EntityFrameworkCore.SqlServer | 8.0.0 | EF Core SQL Server provider |
| Microsoft.EntityFrameworkCore.Tools | 8.0.0 | Migration CLI |
| Microsoft.EntityFrameworkCore.Design | 8.0.0 | Design-time tools |
| Microsoft.AspNetCore.Authentication.JwtBearer | 8.0.0 | JWT middleware |
| BCrypt.Net-Next | 4.0.3 | Password hashing |
| System.IdentityModel.Tokens.Jwt | 7.0.3 | JWT token creation |
| Swashbuckle.AspNetCore | 6.5.0 | Swagger UI |

### InsureTrust.Web
| Package | Version | Purpose |
|---------|---------|---------|
| System.IdentityModel.Tokens.Jwt | 7.0.3 | Decode JWT claims for session |

---

## 19. Troubleshooting

### SSL certificate error in browser
```bash
dotnet dev-certs https --clean
dotnet dev-certs https --trust
```

### `dotnet ef` command not found
```bash
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef
```

### Migration already exists
```bash
cd InsureTrust.API
dotnet ef migrations remove
dotnet ef migrations add InitialCreate --output-dir Data/Migrations
dotnet ef database update
```

### Database schema mismatch / want fresh start
```bash
cd InsureTrust.API
dotnet ef database drop --force
dotnet ef database update
```

### Web app cannot reach API (HttpRequestException)
- Confirm API is running on port 7000
- Check `InsureTrust.Web/appsettings.json`:
  ```json
  "ApiBaseUrl": "https://localhost:7000/"
  ```
- Make sure both projects are running simultaneously

### Port already in use
Edit `Properties/launchSettings.json` in the affected project and change port numbers.
Update `ApiBaseUrl` in Web's `appsettings.json` if you change API port.

### File uploads not saving
Manually create the upload directories:
```
InsureTrust.API/wwwroot/uploads/kyc/
InsureTrust.API/wwwroot/uploads/claim-documents/
InsureTrust.API/wwwroot/uploads/support-attachments/
```

### Session not working (keeps logging out)
Ensure in `InsureTrust.Web/Program.cs` that `app.UseSession()` appears **before** `app.UseAuthorization()` — it already does in the provided code.

---

*Built with ASP.NET Core 8 · Entity Framework Core 8 · SQL Server · JWT Bearer Auth*

*🏆 Insure Trust — Protecting What Matters Most*
