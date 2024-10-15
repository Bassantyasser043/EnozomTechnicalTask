using Repositories.Dto;

namespace Services
{
    public interface IDoctorAvailabilityService
    {
        Task UpdateDoctorAvailabilityAsync(int doctorId, UpdateDoctorTimeAvailabilityDto availabilityDto);
    }
}
