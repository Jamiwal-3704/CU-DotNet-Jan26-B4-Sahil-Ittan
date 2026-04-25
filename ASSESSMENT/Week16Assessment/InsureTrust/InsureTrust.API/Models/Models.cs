namespace InsureTrust.API.Models;

public class User
{
    public int Id { get; set; }
    public string UserNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string PhoneNo { get; set; } = string.Empty;
    public string PanCard { get; set; } = string.Empty;
    public string KycDocumentPath { get; set; } = string.Empty;
    public string KycStatus { get; set; } = "Pending";
    public string Role { get; set; } = "Customer";
    public decimal Balance { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<UserPolicy> UserPolicies { get; set; } = new List<UserPolicy>();
    public ICollection<SupportQuery> SupportQueries { get; set; } = new List<SupportQuery>();
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    //public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}

public class PolicyType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public decimal BaseMonthlyPremium { get; set; } = 5000;
    public int MinTenureMonths { get; set; } = 12;
    public int MaxTenureMonths { get; set; } = 120;
    public string CoverageDetails { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<PolicyTerm> PolicyTerms { get; set; } = new List<PolicyTerm>();
    public ICollection<PolicyRequiredField> RequiredFields { get; set; } = new List<PolicyRequiredField>();
    public ICollection<UserPolicy> UserPolicies { get; set; } = new List<UserPolicy>();
}

public class PolicyTerm
{
    public int Id { get; set; }
    public int PolicyTypeId { get; set; }
    public string TermText { get; set; } = string.Empty;
    public PolicyType PolicyType { get; set; } = null!;
}

public class PolicyRequiredField
{
    public int Id { get; set; }
    public int PolicyTypeId { get; set; }
    public string FieldName { get; set; } = string.Empty;
    public string FieldType { get; set; } = "text";
    public bool IsMandatory { get; set; } = true;
    public PolicyType PolicyType { get; set; } = null!;
}

public class UserPolicy
{
    public int Id { get; set; }
    public string PolicyNumber { get; set; } = string.Empty;
    public int UserId { get; set; }
    public int PolicyTypeId { get; set; }
    public string Status { get; set; } = "Pending";
    public DateTime PurchaseDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public int Tenure { get; set; }
    public decimal PackageAmount { get; set; }
    public string AdminRemarks { get; set; } = string.Empty;
    public string DynamicFieldsJson { get; set; } = "{}";
    public User User { get; set; } = null!;
    public PolicyType PolicyType { get; set; } = null!;
    public ICollection<Claim> Claims { get; set; } = new List<Claim>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}

public class Claim
{
    public int Id { get; set; }
    public string ClaimNumber { get; set; } = string.Empty;
    public int UserPolicyId { get; set; }
    public string ClaimStatus { get; set; } = "Pending";
    public DateTime ClaimDate { get; set; }
    public decimal MaturityAmount { get; set; }
    public string DocumentsSubmitted { get; set; } = "[]";
    public string AdminRemarks { get; set; } = string.Empty;
    public UserPolicy UserPolicy { get; set; } = null!;
}

public class SupportQuery
{
    public int Id { get; set; }
    public string TicketNumber { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string AttachmentPath { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending";
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public User User { get; set; } = null!;
}

public class Notification
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string ColorCode { get; set; } = "Blue";
    public bool IsRead { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    public string RelatedFeature { get; set; } = string.Empty;
    public User User { get; set; } = null!;
}

public class Payment
{
    public int Id { get; set; }
    public string PaymentNumber { get; set; } = string.Empty;
    public int UserId { get; set; }
    public int UserPolicyId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public string Status { get; set; } = "Success";
    //public User User { get; set; } = null!;
    public UserPolicy UserPolicy { get; set; } = null!;
}
