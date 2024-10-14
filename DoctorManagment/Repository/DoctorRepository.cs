using DoctorAvailabiltity.Data.Context;
using DoctorAvailabiltity.DoctorManagment.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DoctorManagment.Repository.DoctorRepository
{
    public class DoctorRepository: IDoctorRepository
    {
        private readonly MyContext _context;
        public DoctorRepository(MyContext myContext) {
            _context = myContext;
        }

        public async Task AddDoctorAsync(Doctor doctor)
        {
            Debug.WriteLine($"Adding new Doctor: {doctor.DoctorName}");
            try
            {
                await _context.Doctors.AddAsync(doctor);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) {
                Debug.WriteLine($"An error occurred while adding the doctor: {ex.Message}");
                throw new Exception("There was an error adding the doctor to the database.", ex);
            }

        }

        public async Task<Doctor?> GetDoctorByIdAsync(int doctorId)
        {
    
                var doctor = await _context.Doctors
                .Include(d => d.DoctorAvailabilities)
                    .ThenInclude(da => da.Day)
                .Include(d => d.DoctorAvailabilities)
                    .ThenInclude(da => da.TimeRanges)
                .FirstOrDefaultAsync(d => d.DoctorId == doctorId);
                if (doctor == null)
                    throw new Exception($"Doctor with ID {doctorId} was not found.");
                return doctor;

        }

    }
}
