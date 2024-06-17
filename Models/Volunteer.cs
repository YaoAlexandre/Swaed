using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Swaed.Models
{
    public class Volunteer : ApplicationUser
    {
        [Required]
        [MaxLength(255)]
        public string FirstNameEn { get; set; }

        [Required]
        [MaxLength(255)]
        public string FirstNameAr { get; set; }

        [Required]
        [MaxLength(255)]
        public string LastNameEn { get; set; }

        [Required]
        [MaxLength(255)]
        public string LastNameAr { get; set; }

        public string? RefNumber { get; set; }
        public string? Photo { get; set; }
        public string EmirateId { get; set; }
        public DateTime? EmirateIdExpiryDate { get; set; }
        public string? FrontId { get; set; }
        public string Phone { get; set; }
        public string? BackId { get; set; }
        public string? PassportNumber { get; set; }
        public DateTime? PassportExpiryDate { get; set; }
        public string Nationality { get; set; }
        public string Residency { get; set; }
        public string Address { get; set; }
        public DateTime? Dob { get; set; }
        public string Gender { get; set; }
        public string? Languages { get; set; }
        public string? Skills { get; set; }
        public string? Profession { get; set; }
        public string? Company { get; set; }
        public string? EmailVerificationCode { get; set; }
        public bool? EmailVerificationStatus { get; set; }
        public string? PhoneVerificationCode { get; set; }
        public bool? PhoneVerificationStatus { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public string? LastLoginIp { get; set; }
        public ICollection<Training> Trainings { get; set; }
        public ICollection<Opportunity> Opportunities { get; set; }


    }
}
