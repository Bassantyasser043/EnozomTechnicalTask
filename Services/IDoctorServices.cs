using DoctorAvailabiltity.Repository.Dto;

namespace DoctorAvailabiltity.Services
{
    public interface IDoctorServices
    {
        Task InsertDoctor(DoctorDto doctorDto);
        Task<DoctorDetailsDto?> GetDoctorByIdAsync(int doctorId);

        Task UpdateDoctorAvailabilityAsync(int doctorId, UpdateDoctorTimeAvailabilityDto availabilityDto);
    }
}
