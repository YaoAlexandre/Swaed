using System.ComponentModel.DataAnnotations;

namespace Swaed.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Opportunity : Event
    {
        [Required]
        public string InitiativeName { get; set; }

        [Required]
        public string InitiativeDescription { get; set; }

        [Required]
        public string InitiativeObjectives { get; set; }

        [Required]
        public string InitiativeDuration { get; set; }

        [Required]
        public string SelectionCriteria { get; set; }

        [Required]
        public string RoleDescription { get; set; }

        [Required]
        public string SelectionProcessDetails { get; set; }

        [Required]
        public string AdditionalInformation { get; set; }

        // Criteria properties
        public int RequiredVolunteers { get; set; }

        public int MinimumVolunteeringHours { get; set; }

        public int MinimumVolunteeringOpportunities { get; set; }

        public int MinimumAge { get; set; }

        public string NationalityRequirement { get; set; }

        public string LanguageRequirement { get; set; }

        public DateTime ApplicationDeadline { get; set; }

        public DateTime ReviewPeriodStart { get; set; }

        public DateTime ReviewPeriodEnd { get; set; }

        // Contact details
        [Required]
        public string ContactName { get; set; }

        [Required]
        public string ContactPhoneNumber { get; set; }

        [Required]
        public string ContactEmail { get; set; }

        // Additional specific properties if needed
    }


}
