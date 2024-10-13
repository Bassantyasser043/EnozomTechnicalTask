using DoctorAvailabiltity.Repository.Entities;

namespace DoctorAvailabiltity.Repository.Rep
{
    public interface ITimeRangeRepository
    {
        Task<TimeRange> GetTimeRangeByIdAsync(int id);
        Task<TimeRange?> GetTimeRangeAsync(TimeSpan from, TimeSpan to);
        Task AddTimeRangeAsync(TimeRange timeRange);
        Task UpdateTimeRangeAsync(TimeRange timeRange);
        Task<int> GetTimeRangeUsageCountAsync(int timeRangeId);
    }
}
