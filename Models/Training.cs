using System.ComponentModel.DataAnnotations;

namespace Swaed.Models
{
    public class Training : Event
    {
        [Required(ErrorMessage = "Training Type is required.")]
        public string TrainingType { get; set; }

        // Description
        public string Description { get; set; }

        // Course Topics
        public string[] CourseTopics { get; set; }

        // Trainer
        public string Trainer { get; set; }

        // Date
        public DateTime Date { get; set; }

        // Location
        public string Location { get; set; }

        // Timing
        public string Timing { get; set; }

        // Training Objectives
        public string[] TrainingObjectives { get; set; }

        // Registration Conditions
        public string[] RegistrationConditions { get; set; }

        // Contact Information
        public string ContactInformation { get; set; }

        // Selection Criteria
        public string GenderRequired { get; set; }
        public int VolunteersRequired { get; set; }
        public int MinimumHoursRequired { get; set; }
        public int AgeRequiredFrom { get; set; }
        public int AgeRequiredTo { get; set; }
        public string NationalityRequired { get; set; }
        public string LanguageRequired { get; set; }
        public bool CovidVaccinationRequired { get; set; }
        public bool AbuDhabiLicenseRequired { get; set; }
    }

}
