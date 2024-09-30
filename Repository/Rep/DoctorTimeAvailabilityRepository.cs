using DoctorAvailabiltity.Repository.Context;
using DoctorAvailabiltity.Repository.Entities;

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

        //public async Task<DoctorAvailability> GetDoctorAvailabilityAsync(int dayId, int timeRangeId)
        //{
        //    return await _context.DoctorAvailabilities
        //        .FirstOrDefaultAsync(da => da.DayId == dayId && da.TimeRangeId == timeRangeId);
        //}
    }
}
