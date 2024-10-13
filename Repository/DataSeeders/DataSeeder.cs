using Microsoft.EntityFrameworkCore;

namespace DoctorAvailabiltity.Repository.DataSeeders
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
