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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurer l'héritage pour les utilisateurs
            modelBuilder.Entity<Volunteer>().HasBaseType<ApplicationUser>();
            modelBuilder.Entity<Organization>().HasBaseType<ApplicationUser>();
            modelBuilder.Entity<Admin>().HasBaseType<ApplicationUser>();

            // Relations many-to-many entre Volunteer et Training
            modelBuilder.Entity<Volunteer>()
                .HasMany(v => v.Trainings)
                .WithMany(t => t.Volunteers)
                .UsingEntity(j => j.ToTable("VolunteerTraining"));

            // Relations many-to-many entre Volunteer et Opportunity
            modelBuilder.Entity<Volunteer>()
                .HasMany(v => v.Opportunities)
                .WithMany(o => o.Volunteers)
                .UsingEntity(j => j.ToTable("VolunteerOpportunity"));

            // Relation one-to-many entre Organization et Training
            modelBuilder.Entity<Organization>()
                .HasMany(o => o.CreatedTrainings)
                .WithOne(t => t.Organizer)
                .HasForeignKey(t => t.OrganizerId);

            // Relation one-to-many entre Organization et Opportunity
            modelBuilder.Entity<Organization>()
                .HasMany(o => o.CreatedOpportunities)
                .WithOne(o => o.Organizer)
                .HasForeignKey(o => o.OrganizerId);
        }
    }
}
