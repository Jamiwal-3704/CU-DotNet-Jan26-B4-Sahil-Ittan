using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace InsureTrust.API.Middleware;

// Development-only middleware to inject a test user when running in Development.
// Usage:
// - Add query string ?devUser=admin or ?devUser=user
// - Or add header `X-Dev-User: admin` or `Authorization: Bearer dev-admin`
// This should only be used locally for manual testing.
public sealed class DevAuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHostEnvironment _env;

    public DevAuthMiddleware(RequestDelegate next, IHostEnvironment env)
    {
        _next = next;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (_env.IsDevelopment())
        {
            string? dev = null;

            var auth = context.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(auth) && auth.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                dev = auth.Substring("Bearer ".Length).Trim();

            if (string.IsNullOrEmpty(dev))
                dev = context.Request.Query["devUser"].FirstOrDefault();

            if (string.IsNullOrEmpty(dev))
                dev = context.Request.Headers["X-Dev-User"].FirstOrDefault();

            if (!string.IsNullOrEmpty(dev))
            {
                var isAdmin = dev.Contains("admin", StringComparison.OrdinalIgnoreCase) || dev.Equals("dev-admin", StringComparison.OrdinalIgnoreCase);
                var id = isAdmin ? "1" : "2";
                var role = isAdmin ? "Admin" : "Customer";

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, id),
                    new Claim(ClaimTypes.Name, isAdmin ? "Dev Admin" : "Dev User"),
                    new Claim(ClaimTypes.Email, isAdmin ? "admin@dev" : "user@dev"),
                    new Claim(ClaimTypes.Role, role)
                };

                context.User = new ClaimsPrincipal(new ClaimsIdentity(claims, "DevAuth"));
            }
        }

        await _next(context);
    }
}
