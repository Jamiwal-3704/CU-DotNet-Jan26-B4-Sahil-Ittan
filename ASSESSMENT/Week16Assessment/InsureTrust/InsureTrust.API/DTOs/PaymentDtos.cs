using System;

namespace InsureTrust.API.DTOs.Payment
{
    public class InitiatePaymentDto
    {
        public int UserPolicyId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
    }

    public class PaymentDto
    {
        public int Id { get; set; }
        public string PaymentNumber { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string PolicyNumber { get; set; } = string.Empty;
    }
}