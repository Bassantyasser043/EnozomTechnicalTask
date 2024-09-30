using DoctorAvailabiltity.Repository.Entities;

namespace DoctorAvailabiltity.Repository.Rep
{
    public interface IDoctorTimeAvailabilityRepository
    {
        Task AddDoctorAvailabilityAsync(DoctorAvailability doctorAvailability);

       // Task<DoctorAvailability> GetDoctorAvailabilityAsync(int dayId, int timeRangeId);
    }
}
