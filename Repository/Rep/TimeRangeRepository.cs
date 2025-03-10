﻿using DoctorAvailabiltity.Repository.Context;
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

        public async Task<TimeRange?> GetTimeRangeAsync(TimeSpan from, TimeSpan to)
        {
            return await _context.TimeRanges
                .FirstOrDefaultAsync(tr => tr.From == from && tr.To == to);
        }
        public async Task<TimeRange> GetTimeRangeByIdAsync(int id)
        {
                return await _context.TimeRanges.FindAsync(id);   
        }
        /*The part related to the update of the availability*/
        public async Task AddAsync(TimeRange timeRange)
        {
            await _context.TimeRanges.AddAsync(timeRange);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(TimeRange timeRange)
        {
            _context.TimeRanges.Update(timeRange);
            await _context.SaveChangesAsync();
        }
        public async Task<int> GetTimeRangeUsageCountAsync(int timeRangeId)
        {
            return await _context.DoctorAvailabilities
                .CountAsync(da => da.TimeRangeId == timeRangeId);
        }
    }
}
