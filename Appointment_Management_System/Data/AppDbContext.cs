using Microsoft.EntityFrameworkCore;
using Appointment_Management_System.Models;
using Appointment_Management_System.BL.ExtensionMethods;

namespace Appointment_Management_System.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }


        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SoftdeleteFilter();
            base.OnModelCreating(modelBuilder);
        }
    }
}
