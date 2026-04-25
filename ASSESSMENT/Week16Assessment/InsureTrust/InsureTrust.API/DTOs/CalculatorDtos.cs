using System;

namespace InsureTrust.API.DTOs.Calculator
{
    public class CalculatorRequestDto
    {
        public string InsuranceType { get; set; } = string.Empty;
        public int Age { get; set; }
        public decimal CoverageAmount { get; set; }
        public int TenureMonths { get; set; }
    }

    public class CalculatorResultDto
    {
        public decimal EstimatedMonthlyPremium { get; set; }
        public decimal TotalPremium { get; set; }
        public string Note { get; set; } = string.Empty;
    }
}