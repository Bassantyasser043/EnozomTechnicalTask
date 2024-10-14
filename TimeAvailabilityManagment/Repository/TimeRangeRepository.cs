using DoctorAvailabiltity.Data.Context;
using DoctorAvailabiltity.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoctorAvailabiltity.TimeAvailabilityManagment.Repository
{
    public class TimeRangeRepository : ITimeRangeRepository
    {
        private readonly MyContext _context;
        public TimeRangeRepository(MyContext myContext)
        {
            _context = myContext;
        }

        public async Task<TimeRange?> GetTimeRangeAsync(TimeSpan from, TimeSpan to)
        {
            return await _context.TimeRanges
                .FirstOrDefaultAsync(tr => tr.From == from && tr.To == to);
        }
        public async Task<TimeRange> GetTimeRangeByIdAsync(int id)
        {
            return await _context.TimeRanges.FindAsync(id);
        }

        public async Task AddTimeRangeAsync(TimeRange timeRange)
        {
            await _context.TimeRanges.AddAsync(timeRange);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateTimeRangeAsync(TimeRange timeRange)
        {
            _context.TimeRanges.Update(timeRange);
            await _context.SaveChangesAsync();
        }
    }
}
