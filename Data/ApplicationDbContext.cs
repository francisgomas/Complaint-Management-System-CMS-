using CMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CMS.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ComplaintForm> ComplaintForms { get; set; }
        public DbSet<HealthFacility> HealthFacility { get; set; }
        public DbSet<ComplaintReason> ComplaintReason { get; set; }
        public DbSet<Hospital> Hospital { get; set; }
        public DbSet<HealthCenter> HealthCenter { get; set; }
        public DbSet<NursingStation> NursingStation { get; set; }
    }
}