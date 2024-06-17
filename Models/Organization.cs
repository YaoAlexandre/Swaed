using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Swaed.Models
{
    public class Organization: ApplicationUser
    {
        [Required]
        [MaxLength(255)]
        public string FullNameEn { get; set; }
        [Required]
        [MaxLength(255)]
        public string FullNameAr { get; set; }
        public string Phone { get; set; }
        public string? RefNumber { get; set; }
        public string Logo { get; set; }
        public string Sector { get; set; }
        public string Description { get; set; }
        public string ContactNameEn { get; set; }
        public string ContactNameAr { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? Website { get; set; }
        public string? Categories { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public string? LastLoginIp { get; set; }
    }
}
