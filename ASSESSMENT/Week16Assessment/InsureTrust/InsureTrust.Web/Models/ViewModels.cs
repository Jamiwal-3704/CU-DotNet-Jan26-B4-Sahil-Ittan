namespace InsureTrust.Web.Models;

public class LoginViewModel
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class RegisterViewModel
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public string PhoneNo { get; set; } = string.Empty;
    public string PanCard { get; set; } = string.Empty;
    public IFormFile? KycDocument { get; set; }
}

public class UserViewModel
{
    public int Id { get; set; }
    public string UserNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNo { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string KycStatus { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class PolicyTypeViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public decimal BaseMonthlyPremium { get; set; }
    public int MinTenureMonths { get; set; }
    public int MaxTenureMonths { get; set; }
    public string CoverageDetails { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreatePolicyTypeViewModel
{
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public decimal BaseMonthlyPremium { get; set; } = 5000;
    public int MinTenureMonths { get; set; } = 12;
    public int MaxTenureMonths { get; set; } = 120;
    public string CoverageDetails { get; set; } = string.Empty;
}

public class PolicyViewModel
{
    public int Id { get; set; }
    public string PolicyNumber { get; set; } = string.Empty;
    public string PolicyTypeName { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime PurchaseDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public int Tenure { get; set; }
    public decimal PackageAmount { get; set; }
    public int DaysLeft { get; set; }
    public string AdminRemarks { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
}

public class PurchasePolicyViewModel
{
    public int PolicyTypeId { get; set; }
    public int Tenure { get; set; } = 24;
    public decimal PackageAmount { get; set; } = 10000;
    public Dictionary<string, string> DynamicFields { get; set; } = new();
}

public class ClaimViewModel
{
    public int Id { get; set; }
    public string ClaimNumber { get; set; } = string.Empty;
    public string PolicyNumber { get; set; } = string.Empty;
    public string PolicyTypeName { get; set; } = string.Empty;
    public string ClaimStatus { get; set; } = string.Empty;
    public DateTime ClaimDate { get; set; }
    public decimal MaturityAmount { get; set; }
    public int DocumentsCount { get; set; }
    public string AdminRemarks { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
}

public class SubmitClaimViewModel
{
    public int PolicyId { get; set; }
    public List<IFormFile> Documents { get; set; } = new();
}

public class SupportViewModel
{
    public int Id { get; set; }
    public string TicketNumber { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
}

public class CreateSupportViewModel
{
    public string Subject { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IFormFile? Attachment { get; set; }
}

public class NotificationViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string ColorCode { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
    public string RelatedFeature { get; set; } = string.Empty;
}

public class DashboardViewModel
{
    public UserViewModel User { get; set; } = new();
    public List<PolicyViewModel> Policies { get; set; } = new();
    public List<ClaimViewModel> Claims { get; set; } = new();
    public List<NotificationViewModel> RecentNotifications { get; set; } = new();
    public int UnreadNotifications { get; set; }
}

public class AdminDashboardViewModel
{
    public List<UserViewModel> Users { get; set; } = new();
    public List<PolicyViewModel> PendingPolicies { get; set; } = new();
    public List<ClaimViewModel> AllClaims { get; set; } = new();
    public List<SupportViewModel> AllTickets { get; set; } = new();
    public List<PolicyTypeViewModel> PolicyTypes { get; set; } = new();
    public int TotalUsers { get; set; }
    public int TotalPolicies { get; set; }
    public int PendingCount { get; set; }
    public int OpenTickets { get; set; }
}
