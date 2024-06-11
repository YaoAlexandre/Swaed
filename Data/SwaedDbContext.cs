using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Swaed.Models;

namespace Swaed.Data
{
    public class SwaedDbContext : IdentityDbContext<IdentityUser>
    {
        public SwaedDbContext(DbContextOptions<SwaedDbContext> options)
        : base(options)
        {
        }

        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        //public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurer l'héritage pour les utilisateurs
            modelBuilder.Entity<Volunteer>()
             .HasBaseType<ApplicationUser>();

            modelBuilder.Entity<Organization>()
                .HasBaseType<ApplicationUser>();

            modelBuilder.Entity<Admin>()
               .HasBaseType<ApplicationUser>();

        }
    }
}
