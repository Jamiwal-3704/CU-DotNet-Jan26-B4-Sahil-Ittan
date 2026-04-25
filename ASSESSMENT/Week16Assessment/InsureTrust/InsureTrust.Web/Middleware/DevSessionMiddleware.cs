using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace InsureTrust.Web.Middleware;

// Development-only middleware to populate session for MVC during local testing.
// Usage: visit any MVC URL with ?devUser=admin or ?devUser=user or add header `X-Dev-User: admin`.
public sealed class DevSessionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHostEnvironment _env;

    public DevSessionMiddleware(RequestDelegate next, IHostEnvironment env)
    {
        _next = next;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (_env.IsDevelopment())
        {
            var dev = context.Request.Query["devUser"].FirstOrDefault() ?? context.Request.Headers["X-Dev-User"].FirstOrDefault();
            if (!string.IsNullOrEmpty(dev))
            {
                var isAdmin = dev.Contains("admin", StringComparison.OrdinalIgnoreCase) || dev.Equals("dev-admin", StringComparison.OrdinalIgnoreCase);
                var role = isAdmin ? "Admin" : "Customer";
                var userId = isAdmin ? "1" : "2";
                var userName = isAdmin ? "Dev Admin" : "Dev User";

                // Set session values expected by the MVC controllers
                context.Session.SetString("Token", "dev-token");
                context.Session.SetString("Role", role);
                context.Session.SetString("UserId", userId);
                context.Session.SetString("UserName", userName);
            }
        }

        await _next(context);
    }
}
