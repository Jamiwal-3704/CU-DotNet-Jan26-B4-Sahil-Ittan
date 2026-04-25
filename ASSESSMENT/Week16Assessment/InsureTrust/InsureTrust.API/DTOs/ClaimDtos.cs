using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace InsureTrust.API.DTOs.Claim
{
    public class SubmitClaimDto
    {
        public List<IFormFile> Documents { get; set; } = new();
    }

    public class ClaimDto
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

    public class UpdateClaimDto
    {
        public string Action { get; set; } = string.Empty;
        public decimal MaturityAmount { get; set; }
        public string AdminRemarks { get; set; } = string.Empty;
    }
}