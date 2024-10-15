using Microsoft.EntityFrameworkCore;

namespace DoctorManagment.Repositories.Data.DataSeeders
{
    public class DataSeeder
    {
        public static void DataSeedingToDatabase(ModelBuilder modelBuilder)
        {
            DaySeeder.Seed(modelBuilder);
            TimeRangeSeeder.Seed(modelBuilder);
            DoctorSeeder.Seed(modelBuilder);
            TimeAvailabilitySeeder.Seed(modelBuilder);
        }
    }
}
