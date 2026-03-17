using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace FinanceTrackerPro.Models
{
    public class Account
    {
        public int AccountId { get; set; }

        [Required]
        public string? AccountName { get; set; }

        public double Balance { get; set; }

        [ValidateNever]
        public List<Transaction>? Transactions { get; set; }
    }
}