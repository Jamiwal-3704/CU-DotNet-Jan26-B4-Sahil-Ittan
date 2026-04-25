using InsureTrust.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);

// Allow disabling HTTPS locally for testing (set env var DISABLE_HTTPS=true)
var disableHttps = string.Equals(Environment.GetEnvironmentVariable("DISABLE_HTTPS"), "true", StringComparison.OrdinalIgnoreCase);

// Ensure web app picks free ports when default ports are occupied (helps running API + Web locally)
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
    return start; // fallback
}

// If ASPNETCORE_URLS not set, choose defaults and ensure they are available
if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("ASPNETCORE_URLS")))
{
    var httpPort = GetAvailablePort(5000);
    var httpsPort = GetAvailablePort(5001);
    builder.WebHost.UseUrls($"http://localhost:{httpPort};https://localhost:{httpsPort}");
}

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? string.Empty;
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? string.Empty;
    options.CallbackPath = "/signin-google";
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(8);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]!);
    client.Timeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddScoped<ApiClient>();

var app = builder.Build();

// Register development session middleware to enable quick MVC login during local testing
if (app.Environment.IsDevelopment())
{
    app.UseMiddleware<InsureTrust.Web.Middleware.DevSessionMiddleware>();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//app.UseHttpsRedirection();
if (!disableHttps)
{
    app.UseHttpsRedirection();
}
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
