using Microsoft.AspNetCore.Identity;

namespace Swaed.Models
{
    public class Admin : IdentityUser
    {
        public DateTime? LastLoginAt { get; set; }
        public string LastLoginIp { get; set; }
    }
}
