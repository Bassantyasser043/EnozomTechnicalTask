using Microsoft.EntityFrameworkCore;
using Repositories.Entities;

namespace DoctorManagment.Repositories.Data.DataSeeders
{
    public class TimeAvailabilitySeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DoctorAvailability>().HasData(
                // Mohamed
                new DoctorAvailability { DoctorAvailabilityId = 1, DoctorId = 1, TimeRangeId = 1, DayId = 2 },
                new DoctorAvailability { DoctorAvailabilityId = 2, DoctorId = 1, TimeRangeId = 2, DayId = 2 },

                new DoctorAvailability { DoctorAvailabilityId = 3, DoctorId = 1, TimeRangeId = 1, DayId = 3 },
                new DoctorAvailability { DoctorAvailabilityId = 4, DoctorId = 1, TimeRangeId = 2, DayId = 3 },
                new DoctorAvailability { DoctorAvailabilityId = 5, DoctorId = 1, TimeRangeId = 3, DayId = 3 },

                new DoctorAvailability { DoctorAvailabilityId = 6, DoctorId = 1, TimeRangeId = 1, DayId = 4 },
                new DoctorAvailability { DoctorAvailabilityId = 7, DoctorId = 1, TimeRangeId = 2, DayId = 4 },

                new DoctorAvailability { DoctorAvailabilityId = 8, DoctorId = 1, TimeRangeId = 2, DayId = 5 },
                new DoctorAvailability { DoctorAvailabilityId = 9, DoctorId = 1, TimeRangeId = 1, DayId = 6 },

                //Ahmed
                new DoctorAvailability { DoctorAvailabilityId = 10, DoctorId = 2, TimeRangeId = 4, DayId = 2 },

                new DoctorAvailability { DoctorAvailabilityId = 11, DoctorId = 2, TimeRangeId = 1, DayId = 3 },
                new DoctorAvailability { DoctorAvailabilityId = 12, DoctorId = 2, TimeRangeId = 3, DayId = 3 },

                new DoctorAvailability { DoctorAvailabilityId = 13, DoctorId = 2, TimeRangeId = 2, DayId = 5 },
                new DoctorAvailability { DoctorAvailabilityId = 14, DoctorId = 2, TimeRangeId = 1, DayId = 6 }

                );

        }
    }
}
