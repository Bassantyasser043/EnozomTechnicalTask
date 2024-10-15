using Microsoft.EntityFrameworkCore;
using Repositories.Entities;

namespace DoctorManagment.Repositories.Data.DataSeeders
{
    public class DoctorSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { DoctorId = 1, DoctorName = "Mohamed" },
                new Doctor { DoctorId = 2, DoctorName = "Ahmed" }
                );

        }
    }
}
