using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Swaed.Models;

namespace Swaed.Data
{
    public class SwaedDbContext : IdentityDbContext<ApplicationUser>
    {
        public SwaedDbContext(DbContextOptions<SwaedDbContext> options)
            : base(options)
        {
        }

        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Opportunity> Opportunities { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventVolunteer> EventVolunteers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurer l'héritage pour les utilisateurs
            modelBuilder.Entity<Volunteer>().HasBaseType<ApplicationUser>();
            modelBuilder.Entity<Organization>().HasBaseType<ApplicationUser>();
            modelBuilder.Entity<Admin>().HasBaseType<ApplicationUser>();

            // Configurer l'héritage pour les événements
            modelBuilder.Entity<Training>().HasBaseType<Event>();
            modelBuilder.Entity<Opportunity>().HasBaseType<Event>();

            // Configurer la relation many-to-many entre Volunteer et Event via EventVolunteer
            modelBuilder.Entity<EventVolunteer>()
                .HasKey(ev => new { ev.EventId, ev.VolunteerId });

            modelBuilder.Entity<EventVolunteer>()
                .HasOne(ev => ev.Event)
                .WithMany(e => e.EventVolunteers)
                .HasForeignKey(ev => ev.EventId);

            modelBuilder.Entity<EventVolunteer>()
                .HasOne(ev => ev.Volunteer)
                .WithMany(v => v.EventVolunteers)
                .HasForeignKey(ev => ev.VolunteerId);

            // Relation one-to-many entre Organization et Event
            modelBuilder.Entity<Organization>()
                .HasMany(o => o.Events)
                .WithOne(e => e.Organizer)
                .HasForeignKey(e => e.OrganizerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
