using DoctorAvailabiltity.Repository.Dto;

namespace DoctorAvailabiltity.Services
{
    public interface IDoctorAvailabilityService
    {
        Task UpdateDoctorAvailabilityAsync(int doctorId, UpdateDoctorTimeAvailabilityDto availabilityDto);
    }
}
