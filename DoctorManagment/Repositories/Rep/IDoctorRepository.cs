using Repositories.Entities;

namespace Repositories.Rep
{
    public interface IDoctorRepository
    {
        Task<Doctor> AddDoctorAsync(Doctor doctor);
        Task<Doctor> GetDoctorByIdAsync(int doctorId);
    }
}
