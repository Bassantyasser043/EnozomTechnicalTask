using DoctorAvailabiltity.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoctorAvailabiltity.Data.DataSeeders
{
    public class TimeRangeSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TimeRange>().HasData(
                new TimeRange { TimeRangeId = 1, From = new TimeSpan(9, 0, 0), To = new TimeSpan(13, 0, 0) },
                new TimeRange { TimeRangeId = 2, From = new TimeSpan(14, 0, 0), To = new TimeSpan(18, 0, 0) },
                new TimeRange { TimeRangeId = 3, From = new TimeSpan(20, 0, 0), To = new TimeSpan(21, 0, 0) },
                new TimeRange { TimeRangeId = 4, From = new TimeSpan(9, 0, 0), To = new TimeSpan(17, 0, 0) }
            );
        }
    }
}
