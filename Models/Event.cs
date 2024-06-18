using System.ComponentModel.DataAnnotations;

namespace Swaed.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Program is required.")]
        public string Program { get; set; }

        [Required(ErrorMessage = "Event Duration is required.")]
        public string EventDuration { get; set; }

        [Required(ErrorMessage = "Event Type is required.")]
        public string EventType { get; set; }

        [Required(ErrorMessage = "You must specify if this is a private event.")]
        public bool IsPrivateEvent { get; set; }

        public string Note { get; set; } = "Purpose of private event is to share private link with volunteers and not be published on volunteers.ae";

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Location { get; set; }

        public string OrganizerId { get; set; }
        public Organization Organizer { get; set; }
        public ICollection<EventVolunteer> EventVolunteers { get; set; } = new List<EventVolunteer>();
    }

}