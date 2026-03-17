using FinanceTrackerPro.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceTrackerPro.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string? Description { get; set; }
        public double Amount { get; set; }
        public string? Category { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int AccountId { get; set; }
        [ValidateNever]
        public Account? account { get; set; }
    }
}