using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using InsureTrust.Web.Models;

namespace InsureTrust.Web.Services;

public class ApiClient
{
    private readonly HttpClient _http;
    private readonly IHttpContextAccessor _ctx;
    private readonly JsonSerializerOptions _json = new() { PropertyNameCaseInsensitive = true };

    public ApiClient(IHttpClientFactory factory, IHttpContextAccessor ctx)
    {
        _http = factory.CreateClient("API");
        _ctx = ctx;
    }

    private void SetAuth()
    {
        var token = _ctx.HttpContext?.Session.GetString("Token");
        if (!string.IsNullOrEmpty(token))
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        else
            _http.DefaultRequestHeaders.Authorization = null;
    }

    private async Task<T?> GetAsync<T>(string url)
    {
        SetAuth();
        var res = await _http.GetAsync(url);
        if (!res.IsSuccessStatusCode) return default;
        var json = await res.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(json, _json);
    }

    private async Task<T?> PostAsync<T>(string url, object data)
    {
        SetAuth();
        var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        var res = await _http.PostAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();
        if (!res.IsSuccessStatusCode) throw new Exception(ExtractMessage(json));
        return JsonSerializer.Deserialize<T>(json, _json);
    }

    private async Task<T?> PostFormAsync<T>(string url, MultipartFormDataContent form)
    {
        SetAuth();
        var res = await _http.PostAsync(url, form);
        var json = await res.Content.ReadAsStringAsync();
        if (!res.IsSuccessStatusCode) throw new Exception(ExtractMessage(json));
        return JsonSerializer.Deserialize<T>(json, _json);
    }

    private async Task<T?> PutAsync<T>(string url, object data)
    {
        SetAuth();
        var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        var res = await _http.PutAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();
        if (!res.IsSuccessStatusCode) throw new Exception(ExtractMessage(json));
        return JsonSerializer.Deserialize<T>(json, _json);
    }

    private async Task DeleteAsync(string url)
    {
        SetAuth();
        await _http.DeleteAsync(url);
    }

    private string ExtractMessage(string json)
    {
        try { var doc = JsonDocument.Parse(json); return doc.RootElement.GetProperty("message").GetString() ?? json; }
        catch { return json; }
    }

    // Auth
    public async Task<string?> LoginAsync(string email, string password)
    {
        var result = await PostAsync<JsonElement>("api/auth/login", new { email, password });
        if (result.ValueKind == JsonValueKind.Undefined) return null;
        return result.TryGetProperty("token", out var tokenEl) ? tokenEl.GetString() : null;
    }

    public async Task<string?> AdminLoginAsync(string email, string password)
    {
        var result = await PostAsync<JsonElement>("api/auth/admin-login", new { email, password });
        if (result.ValueKind == JsonValueKind.Undefined) return null;
        return result.TryGetProperty("token", out var tokenEl) ? tokenEl.GetString() : null;
    }

    public async Task<string?> GoogleLoginAsync(string email, string name)
    {
        var result = await PostAsync<JsonElement>("api/auth/google-login", new { email, name });
        if (result.ValueKind == JsonValueKind.Undefined) return null;
        return result.TryGetProperty("token", out var tokenEl) ? tokenEl.GetString() : null;
    }

    public async Task<UserViewModel?> RegisterAsync(RegisterViewModel model)
    {
        var form = new MultipartFormDataContent();
        form.Add(new StringContent(model.Name), "Name");
        form.Add(new StringContent(model.Email), "Email");
        form.Add(new StringContent(model.Password), "Password");
        form.Add(new StringContent(model.ConfirmPassword), "ConfirmPassword");
        form.Add(new StringContent(model.PhoneNo), "PhoneNo");
        form.Add(new StringContent(model.PanCard), "PanCard");
        if (model.KycDocument != null)
        {
            var stream = model.KycDocument.OpenReadStream();
            form.Add(new StreamContent(stream), "KycDocument", model.KycDocument.FileName);
        }
        var result = await PostFormAsync<JsonElement>("api/auth/register", form);
        if (result.ValueKind == JsonValueKind.Undefined) return null;
        if (result.TryGetProperty("user", out var userEl))
        {
            var userJson = userEl.GetRawText();
            return JsonSerializer.Deserialize<UserViewModel>(userJson, _json);
        }
        return null;
    }

    public async Task<UserViewModel?> GetProfileAsync() => await GetAsync<UserViewModel>("api/auth/profile");
    public async Task<List<UserViewModel>?> GetAllUsersAsync() => await GetAsync<List<UserViewModel>>("api/auth/users");

    // Policy Types
    public async Task<List<PolicyTypeViewModel>?> GetPolicyTypesAsync() => await GetAsync<List<PolicyTypeViewModel>>("api/policy/types");
    public async Task<PolicyTypeViewModel?> GetPolicyTypeAsync(int id) => await GetAsync<PolicyTypeViewModel>($"api/policy/types/{id}");

    public async Task<PolicyTypeViewModel?> CreatePolicyTypeAsync(CreatePolicyTypeViewModel model)
    {
        var result = await PostAsync<JsonElement>("api/policy/types", new
        {
            model.Name, model.Category, model.Description, model.Icon,
            model.BaseMonthlyPremium, model.MinTenureMonths, model.MaxTenureMonths, model.CoverageDetails
        });
        if (result.ValueKind == JsonValueKind.Undefined) return null;
        if (result.TryGetProperty("policyType", out var pEl))
        {
            var raw = pEl.GetRawText();
            return JsonSerializer.Deserialize<PolicyTypeViewModel>(raw, _json);
        }
        return null;
    }

    public async Task<PolicyTypeViewModel?> UpdatePolicyTypeAsync(int id, CreatePolicyTypeViewModel model)
    {
        var result = await PutAsync<JsonElement>($"api/policy/types/{id}", new
        {
            model.Name, model.Category, model.Description, model.Icon,
            model.BaseMonthlyPremium, model.MinTenureMonths, model.MaxTenureMonths, model.CoverageDetails
        });
        if (result.ValueKind == JsonValueKind.Undefined) return null;
        if (result.TryGetProperty("policyType", out var pEl))
        {
            var raw = pEl.GetRawText();
            return JsonSerializer.Deserialize<PolicyTypeViewModel>(raw, _json);
        }
        return null;
    }

    // Policies
    public async Task<List<PolicyViewModel>?> GetMyPoliciesAsync() => await GetAsync<List<PolicyViewModel>>("api/policy/my-policies");
    public async Task<List<PolicyViewModel>?> GetAllPoliciesAsync() => await GetAsync<List<PolicyViewModel>>("api/policy/all");
    public async Task<List<PolicyViewModel>?> GetPendingPoliciesAsync() => await GetAsync<List<PolicyViewModel>>("api/policy/pending");

    public async Task<PolicyViewModel?> PurchasePolicyAsync(int typeId, int tenure, decimal package, Dictionary<string, string> fields)
    {
        var result = await PostAsync<JsonElement>("api/policy/purchase", new { policyTypeId = typeId, tenure, packageAmount = package, dynamicFields = fields });
        if (result.ValueKind == JsonValueKind.Undefined) return null;
        if (result.TryGetProperty("policy", out var policyEl))
        {
            var raw = policyEl.GetRawText();
            return JsonSerializer.Deserialize<PolicyViewModel>(raw, _json);
        }
        return null;
    }

    public async Task ApprovePolicyAsync(int id, string action, string remarks = "")
        => await PutAsync<JsonElement>($"api/policy/approve/{id}", new { action, adminRemarks = remarks });

    public async Task EditPolicyAsync(int id, int tenure, decimal package)
        => await PutAsync<JsonElement>($"api/policy/edit/{id}", new { tenure, packageAmount = package });

    public async Task RenewPolicyAsync(int id) => await PostAsync<JsonElement>($"api/policy/renew/{id}", new { });

    public async Task DeletePolicyAsync(int id) => await DeleteAsync($"api/policy/{id}");

    // Claims
    public async Task<List<ClaimViewModel>?> GetMyClaimsAsync() => await GetAsync<List<ClaimViewModel>>("api/claim/my-claims");
    public async Task<List<ClaimViewModel>?> GetAllClaimsAsync() => await GetAsync<List<ClaimViewModel>>("api/claim/all");

    public async Task<ClaimViewModel?> SubmitClaimAsync(int policyId, List<IFormFile> docs)
    {
        var form = new MultipartFormDataContent();
        foreach (var doc in docs)
        {
            var stream = doc.OpenReadStream();
            form.Add(new StreamContent(stream), "Documents", doc.FileName);
        }
        var result = await PostFormAsync<JsonElement>($"api/claim/submit/{policyId}", form);
        if (result.ValueKind == JsonValueKind.Undefined) return null;
        if (result.TryGetProperty("claim", out var claimEl))
        {
            var raw = claimEl.GetRawText();
            return JsonSerializer.Deserialize<ClaimViewModel>(raw, _json);
        }
        return null;
    }

    public async Task UpdateClaimAsync(int id, string action, decimal amount, string remarks)
        => await PutAsync<JsonElement>($"api/claim/update/{id}", new { action, maturityAmount = amount, adminRemarks = remarks });

    // Support
    public async Task<List<SupportViewModel>?> GetMyQueriesAsync() => await GetAsync<List<SupportViewModel>>("api/support/my-queries");
    public async Task<List<SupportViewModel>?> GetAllQueriesAsync() => await GetAsync<List<SupportViewModel>>("api/support/all");

    public async Task<SupportViewModel?> SubmitQueryAsync(CreateSupportViewModel model)
    {
        var form = new MultipartFormDataContent();
        form.Add(new StringContent(model.Subject), "Subject");
        form.Add(new StringContent(model.Description), "Description");
        if (model.Attachment != null)
        {
            var stream = model.Attachment.OpenReadStream();
            form.Add(new StreamContent(stream), "Attachment", model.Attachment.FileName);
        }
        var result = await PostFormAsync<JsonElement>("api/support/submit", form);
        if (result.ValueKind == JsonValueKind.Undefined) return null;
        if (result.TryGetProperty("query", out var qEl))
        {
            var raw = qEl.GetRawText();
            return JsonSerializer.Deserialize<SupportViewModel>(raw, _json);
        }
        return null;
    }

    public async Task UpdateTicketStatusAsync(int id, string status)
        => await PutAsync<JsonElement>($"api/support/update/{id}", new { status });

    // Notifications
    public async Task<List<NotificationViewModel>?> GetNotificationsAsync() => await GetAsync<List<NotificationViewModel>>("api/notification");
    public async Task<int> GetUnreadCountAsync()
    {
        var result = await GetAsync<JsonElement>("api/notification/unread-count");
        if (result.ValueKind == JsonValueKind.Undefined) return 0;
        if (result.TryGetProperty("count", out var countEl) && countEl.ValueKind == JsonValueKind.Number)
            return countEl.GetInt32();
        return 0;
    }
    public async Task MarkReadAsync(int id) => await PutAsync<JsonElement>($"api/notification/mark-read/{id}", new { });
    public async Task MarkAllReadAsync() => await PutAsync<JsonElement>("api/notification/mark-all-read", new { });
}
