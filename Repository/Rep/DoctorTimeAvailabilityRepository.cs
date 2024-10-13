using DoctorAvailabiltity.Repository.Context;
using DoctorAvailabiltity.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoctorAvailabiltity.Repository.Rep
{
    public class DoctorTimeAvailabilityRepository: IDoctorTimeAvailabilityRepository
    {
        private readonly MyContext _context;
        public DoctorTimeAvailabilityRepository(MyContext context)
        {
            _context = context;
        }

        public async Task AddDoctorAvailabilityAsync(DoctorAvailability doctorAvailability)
        {
            await _context.DoctorAvailabilities.AddAsync(doctorAvailability);
            await _context.SaveChangesAsync();
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
