using Repositories.Dto;
using Repositories.Entities;

namespace Services
{
    public interface IDoctorServices
    {
        Task<Doctor> AddDoctorAsync(DoctorDto doctorDto);
        Task<DoctorDetailsDto?> GetDoctorByIdAsync(int doctorId);
    }
}
