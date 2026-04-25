# InsureTrust API — Clean Architecture Implementation

> **Status: ✅ Build Succeeded — 0 Errors, 0 Warnings**

---

## 1. Repository Pattern ✅ (Pre-existing, unchanged)

**File:** `Repositories/Repositories.cs`

Each entity has a dedicated interface + implementation:

| Interface | Implementation |
|---|---|
| `IUserRepository` | `UserRepository` |
| `IPolicyRepository` | `PolicyRepository` |
| `IClaimRepository` | `ClaimRepository` |
| `ISupportRepository` | `SupportRepository` |
| `INotificationRepository` | `NotificationRepository` |
| `IPaymentRepository` | `PaymentRepository` |

All registered as `AddScoped<IXxx, Xxx>()` in `Program.cs`.

---

## 2. Fluent API (EF Core) ✅ UPGRADED

**File:** `Data/AppDbContext.cs`

Every entity now has **explicit full Fluent API configuration** inside `OnModelCreating`:

```csharp
modelBuilder.Entity<User>(entity =>
{
    entity.ToTable("Users");
    entity.Property(u => u.Email).IsRequired().HasMaxLength(150);
    entity.Property(u => u.Balance).HasPrecision(18, 2).HasDefaultValue(0m);
    entity.HasIndex(u => u.Email).IsUnique();
    entity.HasMany(u => u.UserPolicies).WithOne(up => up.User)
          .HasForeignKey(up => up.UserId).OnDelete(DeleteBehavior.Cascade);
});
```

Config covers: table names, required fields, max lengths, precision, defaults, unique indexes, and all relationships with explicit `DeleteBehavior`.

---

## 3. FluentValidation ✅ NEW

**Files:** `Validators/` (5 new files)

| Validator File | DTOs Covered |
|---|---|
| `AuthValidators.cs` | `RegisterDto`, `LoginDto`, `GoogleLoginDto` |
| `PolicyValidators.cs` | `CreatePolicyTypeDto`, `CreatePolicyDto`, `ApprovePolicyDto`, `EditPolicyDto` |
| `ClaimValidators.cs` | `SubmitClaimDto`, `UpdateClaimDto` |
| `SupportValidators.cs` | `CreateSupportQueryDto`, `UpdateSupportStatusDto` |
| `PaymentValidators.cs` | `InitiatePaymentDto` |

Registered via `AddFluentValidationAutoValidation()` — validation runs **automatically** before the controller action. Invalid requests return 400 with a full error list via `GlobalExceptionMiddleware`.

**Example rule:**
```csharp
RuleFor(x => x.PanCard)
    .Matches(@"^[A-Z]{5}\d{4}[A-Z]$")
    .WithMessage("Invalid PAN card format. Expected: ABCDE1234F.");
```

---

## 4. Service Layer ✅ (Pre-existing, UPGRADED with AutoMapper)

**File:** `Services/Services.cs`

Business rules live entirely here — controllers are thin. Examples:
- `PackageAmount` must be one of `₹5,000 / ₹10,000 / ₹15,000`
- PAN card format validation (registration)
- Balance credit/debit on claim approval
- Calculated fields: `DaysLeft`, `DocumentsCount` (via AutoMapper)

---

## 5. AutoMapper ✅ NEW

**File:** `Mappings/MappingProfile.cs`

Replaces all manual `MapXxxDto()` static helpers. All calculated fields are declared in one place:

```csharp
// Calculated field: days remaining until policy expires
.ForMember(d => d.DaysLeft,
    o => o.MapFrom(s => Math.Max(0, (s.ExpiryDate - DateTime.UtcNow).Days)));

// Calculated field: count of submitted claim documents (via IValueConverter)
.ForMember(d => d.DocumentsCount,
    o => o.ConvertUsing(new DocumentsCountConverter(), s => s));
```

All services now inject `IMapper` instead of using static helpers.

---

## 6. Exception Middleware ✅ UPGRADED

**File:** `Exceptions/Exceptions.cs`

`GlobalExceptionMiddleware` now:
- ✅ Catches **FluentValidation** `ValidationException` → 400 + full errors list
- ✅ Returns **`ApiResponse<T>`** JSON (not raw anonymous objects)
- ✅ Logs via **Serilog** (`Log.Warning` for business errors, `Log.Error` for 500s)
- ✅ Hides internals on 500 — safe public message vs detailed internal log

```json
// FluentValidation failure response example
{
  "success": false,
  "message": "One or more validation errors occurred.",
  "statusCode": 400,
  "errors": ["PAN card format invalid", "Password must be >= 6 chars"],
  "timestamp": "2026-04-25T06:18:00Z"
}
```

---

## 7. Serilog ✅ NEW

**File:** `Program.cs`

Bootstrap logger created **before** the host is built (catches fatal startup errors):

```csharp
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}...")
    .WriteTo.File("Logs/insuretrust-.log", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30)
    .CreateLogger();
```

- **Console sink** — coloured, human-readable for development
- **File sink** — daily rolling log files, 30-day retention in `InsureTrust.API/Logs/`
- **Request logging** — `UseSerilogRequestLogging()` logs every HTTP request with method, path, status, elapsed ms
- **Override filters** — `Microsoft.*` only logged at Warning or above

---

## 8. Standard API Response Wrapper ✅ NEW

**File:** `Common/ApiResponse.cs`

All endpoints now return the same envelope:

```json
{
  "success": true,
  "message": "Login successful.",
  "data": { "token": "eyJ..." },
  "statusCode": 200,
  "timestamp": "2026-04-25T06:18:00Z",
  "errors": null
}
```

Static factory methods:

| Method | HTTP | Use |
|---|---|---|
| `ApiResponse<T>.Ok(data, msg)` | 200 | Standard success |
| `ApiResponse<T>.Created(data, msg)` | 201 | Resource created |
| `ApiResponse<T>.NoContent(msg)` | 204 | Delete / mark-read |
| `ApiResponse<T>.Fail(msg, code, errors)` | 4xx/5xx | Thrown via middleware |

---

## New File Tree

```
InsureTrust.API/
├── Common/
│   └── ApiResponse.cs          ← NEW: Standard response wrapper
├── Mappings/
│   └── MappingProfile.cs       ← NEW: AutoMapper profile (calculated fields)
├── Validators/
│   ├── AuthValidators.cs       ← NEW: Register/Login/Google validation
│   ├── PolicyValidators.cs     ← NEW: Policy CRUD validation
│   ├── ClaimValidators.cs      ← NEW: Claim validation
│   ├── SupportValidators.cs    ← NEW: Support ticket validation
│   └── PaymentValidators.cs    ← NEW: Payment validation
├── Data/
│   └── AppDbContext.cs         ← UPGRADED: Full Fluent API
├── Exceptions/
│   └── Exceptions.cs           ← UPGRADED: ApiResponse + Serilog + FluentValidation
├── Services/
│   └── Services.cs             ← UPGRADED: AutoMapper injection
├── Controllers/                ← ALL UPGRADED: ApiResponse<T> wrapper
└── Program.cs                  ← UPGRADED: Serilog + FluentValidation + AutoMapper DI
```

> [!TIP]
> Log files are written to `InsureTrust.API/Logs/insuretrust-YYYYMMDD.log` with 30-day rolling retention.

> [!NOTE]
> The `ConvertUsing` pattern in MappingProfile (for `DocumentsCount`) is required because C# expression trees cannot use methods with optional parameters — a clean `IValueConverter<Claim, int>` solves this.
