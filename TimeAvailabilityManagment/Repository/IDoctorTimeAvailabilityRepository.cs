using DoctorAvailabiltity.Repository.Entities;

namespace DoctorAvailabiltity.TimeAvailabilityManagment.Repository
{
    public interface IDoctorTimeAvailabilityRepository
    {
        Task AddDoctorAvailabilityAsync(DoctorAvailability doctorAvailability);
        Task UpdateDoctorAvailabilityAsync(DoctorAvailability doctorAvailability);
        Task<int> GetTimeRangeUsageCountAsync(int timeRangeId);

    }
}
