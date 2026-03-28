using System.ComponentModel.DataAnnotations;

namespace IdentityService.DTOs
{
    public class RegisterDto
    {

        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        //[Required]
        //public string? FullName { get; set; }

        //[Required]
        //[EmailAddress]
        //public string? Email { get; set; }

        //[Required]
        //[MinLength(6)]
        //public string? Password { get; set; }

        //[Required]
        //[Compare("Password", ErrorMessage = "Passwords do not match")]
        //public string? ConfirmPassword { get; set; }

        //[Required]
        //public string? Role { get; set; } // Manager or Driver
    }
}
