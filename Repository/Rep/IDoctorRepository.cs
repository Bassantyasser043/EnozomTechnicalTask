using DoctorAvailabiltity.Repository.Entities;

namespace DoctorAvailabiltity.Repository.Rep
{
    public interface IDoctorRepository
    {
        Task InsertDoctor(Doctor doctor);
        Task<Doctor> GetDoctorByIdAsync(int doctorId);
    }
}
