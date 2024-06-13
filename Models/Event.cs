using System.ComponentModel.DataAnnotations;

namespace Swaed.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Category { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string TitleArabic { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        [MaxLength(2000)]
        public string DescriptionArabic { get; set; }

        [MaxLength(2000)]
        public string Roles { get; set; }

        public string Pictures { get; set; }

        [MaxLength(500)]
        public string Location { get; set; }

        [MaxLength(500)]
        public string Address { get; set; }

        public string GoogleMap { get; set; }

        public int NumberOfVolunteers { get; set; }

        public decimal Fees { get; set; }

        [MaxLength(50)]
        public string Gender { get; set; }

        public int MinAge { get; set; }

        public int MaxAge { get; set; }

        [MaxLength(255)]
        public string Nationality { get; set; }

        [MaxLength(255)]
        public string Language { get; set; }

        [MaxLength(255)]
        public string City { get; set; }

        [MaxLength(255)]
        public string Author { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [MaxLength(50)]
        public string Status { get; set; }
    }
}