using System;
using System.Collections.Generic;

namespace InsureTrust.API.DTOs.Policy
{
    public class PolicyTypeDto
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

    public class CreatePolicyTypeDto
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

    public class CreatePolicyDto
    {
        public int PolicyTypeId { get; set; }
        public int Tenure { get; set; }
        public decimal PackageAmount { get; set; }
        public Dictionary<string, string> DynamicFields { get; set; } = new();
    }

    public class PolicyDto
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

    public class ApprovePolicyDto
    {
        public string Action { get; set; } = string.Empty;
        public string AdminRemarks { get; set; } = string.Empty;
    }

    public class EditPolicyDto
    {
        public int Tenure { get; set; }
        public decimal PackageAmount { get; set; }
    }
}