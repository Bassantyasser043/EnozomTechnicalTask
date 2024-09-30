using DoctorAvailabiltity.Repository.Entities;

namespace DoctorAvailabiltity.Repository.Rep
{
    public interface ITimeRangeRepository
    {
        Task<TimeRange> GetTimeRangeByIdAsync(int id);
        //Task<List<TimeRange>> GetSharedTimeRangesAsync(TimeSpan from, TimeSpan to);
        //Task AddAsync(TimeRange timeRange);
    }
}
