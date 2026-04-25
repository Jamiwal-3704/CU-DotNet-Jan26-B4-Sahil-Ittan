using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using InsureTrust.API.Data;
using InsureTrust.API.Exceptions;
using InsureTrust.API.Helpers;
using InsureTrust.API.Mappings;
using InsureTrust.API.Repositories;
using InsureTrust.API.Services;
using InsureTrust.API.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

// ── Bootstrap Serilog (early – before the host is built) ─────────────────────
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console(outputTemplate:
        "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    .WriteTo.File(
        path: "Logs/insuretrust-.log",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

try
{
    Log.Information("Starting InsureTrust API...");

    var builder = WebApplication.CreateBuilder(args);

    // ── Replace built-in logging with Serilog ─────────────────────────────────
    builder.Host.UseSerilog();

    // ── Dynamic port binding (dev convenience) ────────────────────────────────
    var disableHttps = string.Equals(
        Environment.GetEnvironmentVariable("DISABLE_HTTPS"), "true",
        StringComparison.OrdinalIgnoreCase);

    int GetAvailablePort(int start)
    {
        for (int port = start; port < start + 1000; port++)
        {
            try
            {
                var listener = new System.Net.Sockets.TcpListener(System.Net.IPAddress.Loopback, port);
                listener.Start();
                listener.Stop();
                return port;
            }
            catch { }
        }
        return start;
    }

    if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("ASPNETCORE_URLS")))
    {
        var httpPort = GetAvailablePort(7000);
        if (!disableHttps)
        {
            var httpsPort = GetAvailablePort(7001);
            builder.WebHost.UseUrls($"http://localhost:{httpPort};https://localhost:{httpsPort}");
        }
        else
        {
            builder.WebHost.UseUrls($"http://localhost:{httpPort}");
        }
    }

    // ── Controllers ───────────────────────────────────────────────────────────
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();

    // ── FluentValidation ──────────────────────────────────────────────────────
    // Register all validators from the Validators assembly.
    // AspNetCore integration wires up automatic model-state validation.
    builder.Services.AddFluentValidationAutoValidation();
    builder.Services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();

    // ── AutoMapper ────────────────────────────────────────────────────────────
    builder.Services.AddAutoMapper(typeof(MappingProfile));

    // ── Swagger with JWT support ──────────────────────────────────────────────
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "InsureTrust API",
            Version = "v1",
            Description = ".NET 8 Web API — Clean Architecture: Repository Pattern, " +
                          "Fluent API (EF Core), FluentValidation, Service Layer, " +
                          "AutoMapper, Exception Middleware, Serilog, API Response Wrapper"
        });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header. Enter: Bearer {token}",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                },
                Array.Empty<string>()
            }
        });
    });

    // ── Database / EF Core ────────────────────────────────────────────────────
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    // ── JWT Authentication ────────────────────────────────────────────────────
    var jwtKey = builder.Configuration["Jwt:Key"]!;
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
            };
        });

    builder.Services.AddAuthorization();

    // ── CORS ──────────────────────────────────────────────────────────────────
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowMVC", policy =>
            policy.WithOrigins(
                    "https://localhost:7001", "http://localhost:7000",
                    "https://localhost:5101", "http://localhost:5100",
                    "https://localhost:5001", "http://localhost:5000")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials());
    });

    // ── Dependency Injection ──────────────────────────────────────────────────
    // Helpers
    builder.Services.AddScoped<JwtHelper>();

    // Repositories (Repository Pattern)
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IPolicyRepository, PolicyRepository>();
    builder.Services.AddScoped<IClaimRepository, ClaimRepository>();
    builder.Services.AddScoped<ISupportRepository, SupportRepository>();
    builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
    builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

    // Services (Business Logic Layer)
    builder.Services.AddScoped<IAuthService, AuthService>();
    builder.Services.AddScoped<IPolicyService, PolicyService>();
    builder.Services.AddScoped<IClaimService, ClaimService>();
    builder.Services.AddScoped<ISupportService, SupportService>();
    builder.Services.AddScoped<INotificationService, NotificationService>();
    builder.Services.AddScoped<ICalculatorService, CalculatorService>();
    builder.Services.AddScoped<IPaymentService, PaymentService>();

    // ── Build & configure pipeline ────────────────────────────────────────────
    var app = builder.Build();

    // Serilog request logging (structured HTTP request logs)
    app.UseSerilogRequestLogging(options =>
    {
        options.MessageTemplate =
            "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.000} ms";
    });

    // Dev-only auth bypass
    app.UseMiddleware<InsureTrust.API.Middleware.DevAuthMiddleware>();

    // Global exception → ApiResponse<T> mapper (must be early in pipeline)
    app.UseMiddleware<GlobalExceptionMiddleware>();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    if (!disableHttps) app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseCors("AllowMVC");
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    // ── Auto-migrate on startup ───────────────────────────────────────────────
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
    }

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "InsureTrust API terminated unexpectedly.");
}
finally
{
    Log.CloseAndFlush();
}
