using Microsoft.EntityFrameworkCore;
using Appointment_Management_System.Observability.Entities;

namespace Appointment_Management_System.Data
{
    public sealed class ObservabilityDbContext : DbContext
    {
        public ObservabilityDbContext(
            DbContextOptions<ObservabilityDbContext> options)
            : base(options) { }

        public DbSet<SystemExceptionLog> SystemExceptionLogs => Set<SystemExceptionLog>();
        public DbSet<PerformanceLog> PerformanceLogs => Set<PerformanceLog>();
        public DbSet<TelemetryEvent> TelemetryEvents => Set<TelemetryEvent>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SystemExceptionLog>().ToTable("SystemExceptionLog");
            modelBuilder.Entity<PerformanceLog>().ToTable("PerformanceLog");
            modelBuilder.Entity<TelemetryEvent>().ToTable("TelemetryEvent");
        }
    }

}
