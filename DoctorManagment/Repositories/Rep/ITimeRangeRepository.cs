

using Repositories.Entities;

namespace Repositories.Rep
{
    public interface ITimeRangeRepository
    {
        Task<TimeRange> GetTimeRangeByIdAsync(int id);
        Task<TimeRange?> GetTimeRangeAsync(TimeSpan from, TimeSpan to);
        Task AddTimeRangeAsync(TimeRange timeRange);
        Task UpdateTimeRangeAsync(TimeRange timeRange);

    }
}
