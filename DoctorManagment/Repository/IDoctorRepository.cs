using DoctorAvailabiltity.DoctorManagment.Entities;

namespace DoctorManagment.Repository.DoctorRepository
{
    public interface IDoctorRepository
    {
        Task AddDoctorAsync(Doctor doctor);
        Task<Doctor> GetDoctorByIdAsync(int doctorId);
    }
}
