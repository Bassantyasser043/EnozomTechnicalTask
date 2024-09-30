using DoctorAvailabiltity.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoctorAvailabiltity.Repository.DataSeeders
{
    public class DaySeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Day>().HasData(
                new Day { DayId = 1, DayName = "Sunday" },
                new Day { DayId = 2, DayName = "Munday" },
                new Day { DayId = 3, DayName = "TuesDay" },
                new Day { DayId = 4, DayName = "Wednesday" },
                new Day { DayId = 5, DayName = "Thursday" },
                new Day { DayId = 6, DayName = "Friday" },
                new Day { DayId = 7, DayName = "Saturday" }
                );

        }
    }
}
