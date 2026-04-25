# 📂 Professor Demo Guide — InsureTrust Clean Architecture

> Open **Visual Studio / VS Code** and show these files one by one.

---

## 1. 🗄️ Repository Pattern

> **"Separates data access from business logic using interfaces."**

### Files to Open:
| File | What to Show |
|---|---|
| `Repositories/Repositories.cs` | **Lines 8–67** — all 6 interfaces (`IUserRepository`, `IPolicyRepository`, etc.) |
| `Repositories/Repositories.cs` | **Lines 69–149** — all 6 concrete implementations (`UserRepository`, `PolicyRepository`, etc.) |
| `Program.cs` | Look for `// Repositories (Repository Pattern)` section — `AddScoped<IXxx, Xxx>()` |

### What to Say:
> *"Every entity has its own interface and implementation. The service layer only depends on the interface — never the concrete class. This is the Repository Pattern."*

---

## 2. 🏗️ Fluent API (EF Core)

> **"Configures the database schema in C# code instead of data annotations."**

### File to Open:
| File | What to Show |
|---|---|
| `Data/AppDbContext.cs` | **Entire `OnModelCreating` method** (lines ~20–170) |

### Key Lines to Highlight:
```csharp
// Table name
entity.ToTable("Users");

// Constraints
entity.Property(u => u.Email).IsRequired().HasMaxLength(150);
entity.Property(u => u.Balance).HasPrecision(18, 2).HasDefaultValue(0m);

// Unique index
entity.HasIndex(u => u.Email).IsUnique();

// Relationship with delete behavior
entity.HasMany(u => u.UserPolicies).WithOne(up => up.User)
      .HasForeignKey(up => up.UserId).OnDelete(DeleteBehavior.Cascade);
```

### What to Say:
> *"All 8 entities are configured here — table names, column lengths, nullable fields, precision for decimals, unique indexes, and foreign key relationships with explicit delete behavior. No `[DataAnnotations]` used anywhere."*

---

## 3. ✅ FluentValidation

> **"Validates incoming request data using a dedicated validator class per DTO."**

### Files to Open (show any 2):
| File | What to Show |
|---|---|
| `Validators/AuthValidators.cs` | `RegisterDtoValidator` — PAN card regex, password match, email format |
| `Validators/PolicyValidators.cs` | `CreatePolicyDtoValidator` — package amount must-rule |
| `Validators/ClaimValidators.cs` | `UpdateClaimDtoValidator` — conditional rule with `When()` |
| `Program.cs` | `AddFluentValidationAutoValidation()` + `AddValidatorsFromAssemblyContaining<>()` |

### Key Lines to Highlight (`AuthValidators.cs`):
```csharp
RuleFor(x => x.PanCard)
    .NotEmpty()
    .Matches(@"^[A-Z]{5}\d{4}[A-Z]$")
    .WithMessage("Invalid PAN card format. Expected: ABCDE1234F.");
```

### Key Lines to Highlight (`ClaimValidators.cs`):
```csharp
When(x => x.Action == "Deny", () =>
{
    RuleFor(x => x.AdminRemarks)
        .NotEmpty().WithMessage("Admin remarks are required when denying a claim.");
});
```

### What to Say:
> *"Each DTO has its own validator class. FluentValidation runs automatically before the controller action is called. If validation fails, the middleware catches it and returns a 400 with the full list of errors."*

---

## 4. 🧠 Service Layer (Business Rules + Calculated Fields)

> **"Contains all business logic. Controllers are kept thin."**

### File to Open:
| File | What to Show |
|---|---|
| `Services/Services.cs` | **Lines 26–78** — all 7 service interfaces |
| `Services/Services.cs` | `PurchaseAsync` method — business rule: only 3 allowed package amounts |
| `Services/Services.cs` | `UpdateClaimAsync` method — balance credit logic (`user.Balance += dto.MaturityAmount`) |
| `Services/Services.cs` | `CalculatorService.Estimate` — pure calculation, age factor, rate lookup |

### Key Lines to Highlight (`PolicyService.PurchaseAsync`):
```csharp
// Business Rule: only specific amounts allowed
if (!new[] { 5000m, 10000m, 15000m }.Contains(dto.PackageAmount))
    throw new BadRequestException("Package amount must be ₹5,000, ₹10,000, or ₹15,000.");
```

### What to Say:
> *"All business rules live in the service layer. Controllers only call services — they never contain logic. Calculated fields like DaysLeft and DocumentsCount are derived here via AutoMapper, not in the controller."*

---

## 5. 🗺️ AutoMapper

> **"Maps domain models to DTOs automatically, including calculated fields."**

### File to Open:
| File | What to Show |
|---|---|
| `Mappings/MappingProfile.cs` | Entire file — all `CreateMap<>` declarations |

### Key Lines to Highlight:
```csharp
// Calculated field: days remaining on policy
.ForMember(d => d.DaysLeft,
    o => o.MapFrom(s => Math.Max(0, (s.ExpiryDate - DateTime.UtcNow).Days)));

// Calculated field: count of claim documents (via IValueConverter)
.ForMember(d => d.DocumentsCount,
    o => o.ConvertUsing(new DocumentsCountConverter(), s => s));

// Nested navigation property flattening
.ForMember(d => d.PolicyTypeName,
    o => o.MapFrom(s => s.PolicyType != null ? s.PolicyType.Name : string.Empty));
```

### Also Show:
- `Services/Services.cs` — any service method (e.g. `GetMyPoliciesAsync`) shows `_mapper.Map<>()` call replacing old manual mapping.

### What to Say:
> *"Before AutoMapper was added, every service had a private static MapXxxDto() method duplicating mapping logic. Now a single MappingProfile handles all mappings in one place, including calculated fields like DaysLeft."*

---

## 6. ⚠️ Exception Middleware

> **"Catches all unhandled exceptions globally and returns a consistent error response."**

### File to Open:
| File | What to Show |
|---|---|
| `Exceptions/Exceptions.cs` | **Lines 1–24** — 4 custom exception types |
| `Exceptions/Exceptions.cs` | `GlobalExceptionMiddleware` class — `InvokeAsync` method |

### Key Lines to Highlight:
```csharp
// Catch FluentValidation errors → return full error list
catch (ValidationException vex)
{
    var errors = vex.Errors.Select(e => e.ErrorMessage);
    await WriteResponseAsync(context, 400,
        ApiResponse<object>.Fail("One or more validation errors occurred.", 400, errors));
}

// Map exception types to HTTP status codes
var (statusCode, logLevel) = ex switch
{
    NotFoundException    => (404, "warning"),
    BadRequestException  => (400, "warning"),
    UnauthorizedException => (401, "warning"),
    ForbiddenException   => (403, "warning"),
    _                    => (500, "error")
};
```

### What to Say:
> *"No try-catch in controllers or services. All exceptions bubble up and are caught here. The middleware maps exception types to HTTP status codes, logs them via Serilog, and returns the standard ApiResponse envelope."*

---

## 7. 📋 Serilog

> **"Structured logging to console and rolling log files."**

### Files to Open:
| File | What to Show |
|---|---|
| `Program.cs` | Top of file — `Log.Logger = new LoggerConfiguration()...` bootstrap section |
| `Program.cs` | `builder.Host.UseSerilog()` line |
| `Program.cs` | `app.UseSerilogRequestLogging(...)` line |
| `Exceptions/Exceptions.cs` | `Log.Warning(...)` and `Log.Error(...)` calls inside middleware |

### Key Lines to Highlight (`Program.cs`):
```csharp
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}...")
    .WriteTo.File(
        path: "Logs/insuretrust-.log",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30)
    .CreateLogger();
```

### Also Show (live if API is running):
- **`InsureTrust.API/Logs/`** folder — open the `.log` file to show actual log entries

### What to Say:
> *"Serilog replaces the default ASP.NET logger. Every HTTP request is logged with method, path, status code and response time. Errors are written to both the console and a daily rolling log file stored for 30 days."*

---

## 8. 📦 Standard API Response Wrapper

> **"Every endpoint returns the exact same JSON shape — success, message, data, statusCode, timestamp, errors."**

### Files to Open:
| File | What to Show |
|---|---|
| `Common/ApiResponse.cs` | Entire file — the generic class and factory methods |
| `Controllers/AuthController.cs` | Show 2–3 action methods using `ApiResponse<T>.Ok()`, `.Created()` |
| `Controllers/PolicyController.cs` | Show `Delete` method using `ApiResponse<T>.NoContent()` |

### Key Lines to Highlight (`ApiResponse.cs`):
```csharp
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public int StatusCode { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public IEnumerable<string>? Errors { get; set; }

    public static ApiResponse<T> Ok(T data, string message = "Request successful.")
        => new() { Success = true, Message = message, Data = data, StatusCode = 200 };

    public static ApiResponse<T> Created(T data, string message = "Resource created successfully.")
        => new() { Success = true, Message = message, Data = data, StatusCode = 201 };
}
```

### Example Response to Show (Swagger / Postman):
```json
{
  "success": true,
  "message": "Login successful.",
  "data": { "token": "eyJhbGciOiJIUzI1NiIs..." },
  "statusCode": 200,
  "timestamp": "2026-04-25T06:18:00Z",
  "errors": null
}
```

### What to Say:
> *"Before this, every controller was returning different shapes — some returned `{ token }`, others `{ message, user }`. Now every single endpoint returns the same ApiResponse<T> envelope. The frontend always knows what to expect."*

---

## 🗂️ Quick Reference Card

| # | Feature | Primary File(s) |
|---|---|---|
| 1 | Repository Pattern | `Repositories/Repositories.cs` |
| 2 | Fluent API (EF Core) | `Data/AppDbContext.cs` |
| 3 | FluentValidation | `Validators/AuthValidators.cs`, `PolicyValidators.cs`, `ClaimValidators.cs` |
| 4 | Service Layer | `Services/Services.cs` |
| 5 | AutoMapper | `Mappings/MappingProfile.cs` |
| 6 | Exception Middleware | `Exceptions/Exceptions.cs` |
| 7 | Serilog | `Program.cs` (top) + `Logs/` folder |
| 8 | API Response Wrapper | `Common/ApiResponse.cs` + any Controller |

> [!TIP]
> **Best demo order:** Start with `ApiResponse.cs` (easiest to understand visually), then `AppDbContext.cs` (Fluent API), then `MappingProfile.cs`, then `Validators/`, then `Exceptions.cs`, then `Services.cs`.
