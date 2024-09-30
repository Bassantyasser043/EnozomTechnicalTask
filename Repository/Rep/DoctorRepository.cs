using DoctorAvailabiltity.Repository.Context;
using DoctorAvailabiltity.Repository.Dto;
using DoctorAvailabiltity.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DoctorAvailabiltity.Repository.Rep
{
    public class DoctorRepository: IDoctorRepository
    {
        private readonly MyContext _context;
        public DoctorRepository(MyContext myContext) {
            _context = myContext;
        }

        public async Task InsertDoctor(Doctor doctor)
        {
            Debug.WriteLine($"Adding new Doctor: {doctor.DoctorName}");
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task<Doctor?> GetDoctorByIdAsync(int doctorId)
        {
            return await _context.Doctors
                .Include(d => d.DoctorAvailabilities)
                    .ThenInclude(da => da.Day)
                .Include(d => d.DoctorAvailabilities)
                    .ThenInclude(da => da.TimeRanges)
                .FirstOrDefaultAsync(d => d.DoctorId == doctorId);
        }




    }
}
