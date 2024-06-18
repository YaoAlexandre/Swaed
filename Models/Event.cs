using System.ComponentModel.DataAnnotations;

namespace Swaed.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Location { get; set; }

        public string OrganizerId { get; set; }
        public Organization Organizer { get; set; }
        public ICollection<EventVolunteer> EventVolunteers { get; set; } = new List<EventVolunteer>();
    }

}