using System;
using Microsoft.AspNetCore.Http;

namespace InsureTrust.API.DTOs.Support
{
    public class CreateSupportQueryDto
    {
        public string Subject { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IFormFile? Attachment { get; set; }
    }

    public class SupportQueryDto
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

    public class UpdateSupportStatusDto
    {
        public string Status { get; set; } = string.Empty;
    }
}