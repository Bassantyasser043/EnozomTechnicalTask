using Microsoft.EntityFrameworkCore;
using Repositories.Context;
using Repositories.Entities;
using System.Diagnostics;

namespace Repositories.Rep
{
    public class DoctorRepository: IDoctorRepository
    {
        private readonly MyContext _context;
        public DoctorRepository(MyContext myContext) {
            _context = myContext;
        }

        public async Task<Doctor> AddDoctorAsync(Doctor doctor)
        {
            Debug.WriteLine($"Adding new Doctor: {doctor.DoctorName}");
            try
            {
                await _context.Doctors.AddAsync(doctor);
                await _context.SaveChangesAsync();
                return doctor;
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
