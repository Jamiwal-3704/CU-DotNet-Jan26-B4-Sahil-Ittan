using Microsoft.AspNetCore.Identity;

namespace IdentityService.Models
{
    public class LoginRequest : IdentityUser
    {
        public string? Role { get; set; }
    }
}
