using DoctorAvailabiltity.Repository.Context;
using DoctorAvailabiltity.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoctorAvailabiltity.Repository.Rep
{
    public class TimeRangeRepository: ITimeRangeRepository
    {
        private readonly MyContext _context;
        public TimeRangeRepository(MyContext myContext) {
           _context = myContext;
        }

        public async Task<TimeRange> GetTimeRangeByIdAsync(int id)
        {
                return await _context.TimeRanges.FindAsync(id);   
        }

        //public async Task<List<TimeRange>> GetSharedTimeRangesAsync(TimeSpan from, TimeSpan to)
        //{
        //    return await _context.TimeRanges
        //        .Where(tr => (tr.From < to && tr.To > from)) // Check for overlaps
        //        .ToListAsync();
        //}
        //public async Task AddAsync(TimeRange timeRange)
        //{
        //    await _context.TimeRanges.AddAsync(timeRange);
        //}

    }
}
