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

        // Ajoutez d'autres propriétés spécifiques au cours de bénévolat si nécessaire

        //j'ai une classe ApplicationUser dans mon projet dont deux classe Volontaire et Organization hérite, maintenant j'ai une autre classe Event qui qui est lié à Une Oganization et plusieurs Volontaire peuvent participer à ce Event comment faire la ça en C# .net core 8 Identity

    }
}
