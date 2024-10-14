using DoctorAvailabiltity.TimeAvailabilityManagment.Dto;

namespace DoctorAvailabiltity.TimeAvailabilityManagment.Services
{
    public interface IDoctorAvailabilityService
    {
        Task UpdateDoctorAvailabilityAsync(int doctorId, UpdateDoctorTimeAvailabilityDto availabilityDto);
    }
}
