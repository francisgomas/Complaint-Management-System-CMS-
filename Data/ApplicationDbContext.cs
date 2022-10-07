using CMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CMS.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Status>().HasData(
                new Status
                {
                    Id = 1,
                    Name = "Active",
                    CreatedAt = DateTime.Now,
                },
                new Status
                {
                    Id = 2,
                    Name = "Inactive",
                    CreatedAt = DateTime.Now,
                }
            );

            modelBuilder.Entity<Gender>().HasData(
                new Status
                {
                    Id = 1,
                    Name = "Male",
                },
                new Status
                {
                    Id = 2,
                    Name = "Female",
                }
            );

            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.FirstName)
                .HasMaxLength(250);

            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.LastName)
                .HasMaxLength(250);

            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.StatusId)
                .HasMaxLength(2);
        }


        public DbSet<ComplaintForm> ComplaintForms { get; set; }
        public DbSet<HealthFacility> HealthFacility { get; set; }
        public DbSet<ComplaintReason> ComplaintReason { get; set; }
        public DbSet<Hospital> Hospital { get; set; }
        public DbSet<HealthCenter> HealthCenter { get; set; }
        public DbSet<NursingStation> NursingStation { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<FormStatus> FormStatus { get; set; }
        public DbSet<Gender> Gender { get; set; }
    }
}