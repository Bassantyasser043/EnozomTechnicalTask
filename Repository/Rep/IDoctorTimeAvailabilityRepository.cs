using DoctorAvailabiltity.Repository.Entities;

namespace DoctorAvailabiltity.Repository.Rep
{
    public interface IDoctorTimeAvailabilityRepository
    {
        Task AddDoctorAvailabilityAsync(DoctorAvailability doctorAvailability);
        Task UpdateDoctorAvailabilityAsync(DoctorAvailability doctorAvailability);
        Task<int> GetTimeRangeUsageCountAsync(int timeRangeId);

    }
}
