using DoctorAvailabiltity.Data.Context;
using DoctorAvailabiltity.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DoctorAvailabiltity.TimeAvailabilityManagment.Repository
{
    public class DoctorTimeAvailabilityRepository : IDoctorTimeAvailabilityRepository
    {
        private readonly MyContext _context;
        public DoctorTimeAvailabilityRepository(MyContext context) => _context = context;

        public async Task AddDoctorAvailabilityAsync(DoctorAvailability doctorAvailability)
        {
            try
            {
                await _context.DoctorAvailabilities.AddAsync(doctorAvailability);
                await _context.SaveChangesAsync();

            }
            catch(DbUpdateException ex)
            {
                Debug.WriteLine($"An error occurred while adding the time availability: {ex.Message}");
                throw new Exception("There was an error adding DoctorAvailabiltity to the database.", ex);
            }
        }

        public async Task UpdateDoctorAvailabilityAsync(DoctorAvailability doctorAvailability)
        {
            _context.DoctorAvailabilities.Update(doctorAvailability);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetTimeRangeUsageCountAsync(int timeRangeId)
        {
            return await _context.DoctorAvailabilities
                .CountAsync(da => da.TimeRangeId == timeRangeId);
        }
    }
}
