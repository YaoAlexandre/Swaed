using System.ComponentModel.DataAnnotations;

namespace Swaed.Models
{
    public class Training : Event
    {
        [Required]
        public string Trainer { get; set; }

        [Required]
        public string CourseTopics { get; set; }

        [Required]
        public string Objectives { get; set; }

        [Required]
        public string RegistrationConditions { get; set; }
    }

}
