using DoctorAvailabiltity.Repository.Dto;

namespace DoctorAvailabiltity.Services
{
    public interface IDoctorServices
    {
        Task AddDoctorAsync(DoctorDto doctorDto);
        Task<DoctorDetailsDto?> GetDoctorByIdAsync(int doctorId);
    }
}
