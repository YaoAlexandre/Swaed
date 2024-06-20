namespace Swaed.Models
{
    public class EventVolunteer
    {
        public int EventId { get; set; }
        public Event Event { get; set; }

        public string VolunteerId { get; set; }
        public Volunteer Volunteer { get; set; }
    }
}
