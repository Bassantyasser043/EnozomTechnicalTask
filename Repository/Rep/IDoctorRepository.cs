using DoctorAvailabiltity.Repository.Entities;

namespace DoctorAvailabiltity.Repository.Rep
{
    public interface IDoctorRepository
    {
        Task AddDoctorAsync(Doctor doctor);
        Task<Doctor> GetDoctorByIdAsync(int doctorId);
    }
}
