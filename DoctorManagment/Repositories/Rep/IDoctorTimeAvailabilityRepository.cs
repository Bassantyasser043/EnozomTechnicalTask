using Repositories.Entities;

namespace Repositories.Rep
{
    public interface IDoctorTimeAvailabilityRepository
    {
        Task AddDoctorAvailabilityAsync(DoctorAvailability doctorAvailability);
        Task UpdateDoctorAvailabilityAsync(DoctorAvailability doctorAvailability);
        Task<int> GetTimeRangeUsageCountAsync(int timeRangeId);

    }
}
