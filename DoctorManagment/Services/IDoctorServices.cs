using DoctorAvailabiltity.DoctorManagment.Dto;

namespace DoctorAvailabiltity.DoctorManagment.Services
{
    public interface IDoctorServices
    {
        Task AddDoctorAsync(DoctorDto doctorDto);
        Task<DoctorDetailsDto?> GetDoctorByIdAsync(int doctorId);
    }
}
