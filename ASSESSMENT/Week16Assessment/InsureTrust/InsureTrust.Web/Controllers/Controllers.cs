using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using InsureTrust.Web.Models;
using InsureTrust.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace InsureTrust.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("Token") != null)
        {
            var role = HttpContext.Session.GetString("Role");
            return role == "Admin" ? RedirectToAction("Dashboard", "Admin") : RedirectToAction("Index", "Dashboard");
        }
        return View();
    }
}

public class AccountController : Controller
{
    private readonly ApiClient _api;
    public AccountController(ApiClient api) => _api = api;

    [HttpGet]
    public IActionResult Login() => HttpContext.Session.GetString("Token") != null ? RedirectToHome() : View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        try
        {
            var token = await _api.LoginAsync(model.Email, model.Password);
            if (token != null) { SetSession(token); return RedirectToAction("Index", "Dashboard"); }
            ModelState.AddModelError("", "Invalid credentials.");
        }
        catch (Exception ex) { ModelState.AddModelError("", ex.Message); }
        return View(model);
    }

    [HttpGet]
    public IActionResult AdminLogin() => View();

    [HttpPost]
    public async Task<IActionResult> AdminLogin(LoginViewModel model)
    {
        try
        {
            var token = await _api.AdminLoginAsync(model.Email, model.Password);
            if (token != null) { SetSession(token); return RedirectToAction("Dashboard", "Admin"); }
            ModelState.AddModelError("", "Invalid admin credentials.");
        }
        catch (Exception ex) { ModelState.AddModelError("", ex.Message); }
        return View(model);
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpGet]
    public IActionResult GoogleLogin(string? returnUrl = null)
    {
        var redirectUrl = Url.Action(nameof(GoogleResponse), new { returnUrl });
        var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet]
    public async Task<IActionResult> GoogleResponse(string? returnUrl = null)
    {
        var authResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        if (!authResult.Succeeded || authResult.Principal == null)
        {
            TempData["Error"] = "Google authentication failed.";
            return RedirectToAction(nameof(Login));
        }

        var email = authResult.Principal.FindFirstValue(ClaimTypes.Email);
        var name = authResult.Principal.FindFirstValue(ClaimTypes.Name) ?? string.Empty;

        if (string.IsNullOrWhiteSpace(email))
        {
            TempData["Error"] = "Email was not provided by Google.";
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

        try
        {
            var token = await _api.GoogleLoginAsync(email, name);
            if (string.IsNullOrWhiteSpace(token))
            {
                TempData["Error"] = "Google sign-in failed.";
                return RedirectToAction(nameof(Login));
            }

            SetSession(token);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Dashboard");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction(nameof(Login));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        try
        {
            await _api.RegisterAsync(model);
            TempData["Success"] = "Registration successful! Please log in.";
            return RedirectToAction("Login");
        }
        catch (Exception ex) { ModelState.AddModelError("", ex.Message); }
        return View(model);
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }

    private void SetSession(string token)
    {
        HttpContext.Session.SetString("Token", token);
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);
        var name = jwt.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value ?? "";
        var role = jwt.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value ?? "";
        var userId = jwt.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value ?? "";
        HttpContext.Session.SetString("UserName", name);
        HttpContext.Session.SetString("Role", role);
        HttpContext.Session.SetString("UserId", userId);
    }

    private IActionResult RedirectToHome()
    {
        var role = HttpContext.Session.GetString("Role");
        return role == "Admin" ? RedirectToAction("Dashboard", "Admin") : RedirectToAction("Index", "Dashboard");
    }
}

public class DashboardController : Controller
{
    private readonly ApiClient _api;
    public DashboardController(ApiClient api) => _api = api;

    public async Task<IActionResult> Index()
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        var user = await _api.GetProfileAsync();
        var policies = await _api.GetMyPoliciesAsync() ?? new();
        var claims = await _api.GetMyClaimsAsync() ?? new();
        var notifs = await _api.GetNotificationsAsync() ?? new();
        var unread = await _api.GetUnreadCountAsync();

        ViewBag.UnreadCount = unread;
        return View(new DashboardViewModel
        {
            User = user ?? new(),
            Policies = policies,
            Claims = claims,
            RecentNotifications = notifs.Take(5).ToList(),
            UnreadNotifications = unread
        });
    }

    private bool IsLoggedIn() => HttpContext.Session.GetString("Token") != null;
}

public class PolicyController : Controller
{
    private readonly ApiClient _api;
    public PolicyController(ApiClient api) => _api = api;

    public async Task<IActionResult> Index()
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        var policies = await _api.GetMyPoliciesAsync() ?? new();
        ViewBag.UnreadCount = await _api.GetUnreadCountAsync();
        return View(policies);
    }

    [HttpGet]
    public async Task<IActionResult> Buy()
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        var types = await _api.GetPolicyTypesAsync() ?? new();
        ViewBag.UnreadCount = await _api.GetUnreadCountAsync();
        return View(types);
    }

    [HttpGet]
    public async Task<IActionResult> Purchase(int typeId)
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        var type = await _api.GetPolicyTypeAsync(typeId);
        if (type == null) return NotFound();
        ViewBag.UnreadCount = await _api.GetUnreadCountAsync();
        return View(type);
    }

    [HttpPost]
    public async Task<IActionResult> Purchase(int typeId, int tenure, decimal packageAmount, string dynamicFields)
    {
        try
        {
            var fields = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(dynamicFields) ?? new();
            await _api.PurchasePolicyAsync(typeId, tenure, packageAmount, fields);
            TempData["Success"] = "Policy submitted for admin approval!";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("Purchase", new { typeId });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Renew(int id)
    {
        try { await _api.RenewPolicyAsync(id); TempData["Success"] = "Policy renewed successfully!"; }
        catch (Exception ex) { TempData["Error"] = ex.Message; }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, int tenure, decimal packageAmount)
    {
        try { await _api.EditPolicyAsync(id, tenure, packageAmount); TempData["Success"] = "Policy updated!"; }
        catch (Exception ex) { TempData["Error"] = ex.Message; }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        try { await _api.DeletePolicyAsync(id); TempData["Success"] = "Policy deleted."; }
        catch (Exception ex) { TempData["Error"] = ex.Message; }
        return RedirectToAction("Index");
    }

    private bool IsLoggedIn() => HttpContext.Session.GetString("Token") != null;
}

public class ClaimController : Controller
{
    private readonly ApiClient _api;
    public ClaimController(ApiClient api) => _api = api;

    public async Task<IActionResult> Index()
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        var claims = await _api.GetMyClaimsAsync() ?? new();
        ViewBag.UnreadCount = await _api.GetUnreadCountAsync();
        return View(claims);
    }

    [HttpGet]
    public async Task<IActionResult> Submit()
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        var policies = (await _api.GetMyPoliciesAsync() ?? new()).Where(p => p.Status == "Active").ToList();
        ViewBag.Policies = policies;
        ViewBag.UnreadCount = await _api.GetUnreadCountAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Submit(SubmitClaimViewModel model)
    {
        try
        {
            await _api.SubmitClaimAsync(model.PolicyId, model.Documents);
            TempData["Success"] = "Claim submitted successfully!";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("Submit");
        }
    }

    private bool IsLoggedIn() => HttpContext.Session.GetString("Token") != null;
}

public class SupportController : Controller
{
    private readonly ApiClient _api;
    public SupportController(ApiClient api) => _api = api;

    public async Task<IActionResult> Index()
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        var queries = await _api.GetMyQueriesAsync() ?? new();
        ViewBag.UnreadCount = await _api.GetUnreadCountAsync();
        return View(queries);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        ViewBag.UnreadCount = await _api.GetUnreadCountAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSupportViewModel model)
    {
        try { await _api.SubmitQueryAsync(model); TempData["Success"] = "Support ticket created!"; return RedirectToAction("Index"); }
        catch (Exception ex) { TempData["Error"] = ex.Message; }
        return View(model);
    }

    private bool IsLoggedIn() => HttpContext.Session.GetString("Token") != null;
}

public class NotificationMvcController : Controller
{
    private readonly ApiClient _api;
    public NotificationMvcController(ApiClient api) => _api = api;

    public async Task<IActionResult> Index()
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        var notifs = await _api.GetNotificationsAsync() ?? new();
        ViewBag.UnreadCount = await _api.GetUnreadCountAsync();
        return View(notifs);
    }

    [HttpPost]
    public async Task<IActionResult> MarkRead(int id)
    {
        await _api.MarkReadAsync(id);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> MarkAllRead()
    {
        await _api.MarkAllReadAsync();
        return RedirectToAction("Index");
    }

    private bool IsLoggedIn() => HttpContext.Session.GetString("Token") != null;
}

public class AdminController : Controller
{
    private readonly ApiClient _api;
    public AdminController(ApiClient api) => _api = api;

    private bool IsAdmin() => HttpContext.Session.GetString("Role") == "Admin";

    public async Task<IActionResult> Dashboard()
    {
        if (!IsAdmin()) return RedirectToAction("AdminLogin", "Account");
        var users = await _api.GetAllUsersAsync() ?? new();
        var pending = await _api.GetPendingPoliciesAsync() ?? new();
        var allPolicies = await _api.GetAllPoliciesAsync() ?? new();
        var claims = await _api.GetAllClaimsAsync() ?? new();
        var tickets = await _api.GetAllQueriesAsync() ?? new();
        var types = await _api.GetPolicyTypesAsync() ?? new();
        ViewBag.UnreadCount = 0;

        return View(new AdminDashboardViewModel
        {
            Users = users,
            PendingPolicies = pending,
            AllClaims = claims,
            AllTickets = tickets,
            PolicyTypes = types,
            TotalUsers = users.Count(u => u.Role == "Customer"),
            TotalPolicies = allPolicies.Count,
            PendingCount = pending.Count,
            OpenTickets = tickets.Count(t => t.Status != "Resolved")
        });
    }

    [HttpPost]
    public async Task<IActionResult> ApprovePolicy(int id, string action, string remarks = "")
    {
        if (!IsAdmin()) return Forbid();
        try { await _api.ApprovePolicyAsync(id, action, remarks); TempData["Success"] = $"Policy {action}ed."; }
        catch (Exception ex) { TempData["Error"] = ex.Message; }
        return RedirectToAction("Dashboard");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateClaim(int id, string action, decimal amount, string remarks = "")
    {
        if (!IsAdmin()) return Forbid();
        try { await _api.UpdateClaimAsync(id, action, amount, remarks); TempData["Success"] = "Claim updated."; }
        catch (Exception ex) { TempData["Error"] = ex.Message; }
        return RedirectToAction("Dashboard");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateTicket(int id, string status)
    {
        if (!IsAdmin()) return Forbid();
        try { await _api.UpdateTicketStatusAsync(id, status); TempData["Success"] = "Ticket updated."; }
        catch (Exception ex) { TempData["Error"] = ex.Message; }
        return RedirectToAction("Dashboard");
    }

    // --- Policy Type Management (NEW FEATURE) ---

    [HttpGet]
    public async Task<IActionResult> CreatePolicyType()
    {
        if (!IsAdmin()) return RedirectToAction("AdminLogin", "Account");
        ViewBag.UnreadCount = 0;
        return View(new CreatePolicyTypeViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> CreatePolicyType(CreatePolicyTypeViewModel model)
    {
        if (!IsAdmin()) return Forbid();
        try
        {
            await _api.CreatePolicyTypeAsync(model);
            TempData["Success"] = $"Policy type '{model.Name}' created and now visible to all customers!";
            return RedirectToAction("Dashboard");
        }
        catch (Exception ex) { ModelState.AddModelError("", ex.Message); }
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> EditPolicyType(int id)
    {
        if (!IsAdmin()) return RedirectToAction("AdminLogin", "Account");
        var pt = await _api.GetPolicyTypeAsync(id);
        if (pt == null) return NotFound();
        var vm = new CreatePolicyTypeViewModel
        {
            Name = pt.Name, Category = pt.Category, Description = pt.Description,
            Icon = pt.Icon, BaseMonthlyPremium = pt.BaseMonthlyPremium,
            MinTenureMonths = pt.MinTenureMonths, MaxTenureMonths = pt.MaxTenureMonths,
            CoverageDetails = pt.CoverageDetails
        };
        ViewBag.PolicyTypeId = id;
        ViewBag.UnreadCount = 0;
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> EditPolicyType(int id, CreatePolicyTypeViewModel model)
    {
        if (!IsAdmin()) return Forbid();
        try
        {
            await _api.UpdatePolicyTypeAsync(id, model);
            TempData["Success"] = "Policy type updated and reflected for all users!";
            return RedirectToAction("Dashboard");
        }
        catch (Exception ex) { ModelState.AddModelError("", ex.Message); }
        ViewBag.PolicyTypeId = id;
        return View(model);
    }
}
