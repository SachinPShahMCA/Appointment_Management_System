using Microsoft.EntityFrameworkCore;
using Appointment_Management_System.Models;

namespace Appointment_Management_System.BL.ExtensionMethods
{
    public static class Allextensionmethods
    {
        public static void SoftdeleteFilter(this ModelBuilder modelBuilder) 
        {
            // Global query filter for soft delete We can write genric cde for all model
            modelBuilder.Entity<Appointment>().HasQueryFilter(a => !a.IsDeleted);
            modelBuilder.Entity<Doctor>().HasQueryFilter(a => !a.IsDeleted);
            modelBuilder.Entity<Patient>().HasQueryFilter(a => !a.IsDeleted);

        }
    }
}
