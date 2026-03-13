using System.ComponentModel.DataAnnotations;

namespace QuickLoanTracker.Models
{
    public class Loan
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Borrow Name")]
        public string BorrowerName { get; set; }

        public string LenderName { get; set; }

        [Range(1,500000, ErrorMessage = "Amount must be between 1 and 500000")]
        public double Amount { get; set; }

        public bool IsSettled { get; set; }
    }
}
