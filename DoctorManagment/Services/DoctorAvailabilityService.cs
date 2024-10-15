
using Repositories.Dto;
using Repositories.Entities;
using Repositories.Rep;

namespace Services
{
    public class DoctorAvailabilityService : IDoctorAvailabilityService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IDoctorTimeAvailabilityRepository _doctorTimeAvailabilityRepository;
        private readonly ITimeRangeRepository _timeRangeRepository;

        public DoctorAvailabilityService(IDoctorRepository doctorRepository,
            IDoctorTimeAvailabilityRepository doctorTimeAvailabilityRepository,
            ITimeRangeRepository timeRangeRepository)
        {
            _doctorRepository = doctorRepository;
            _doctorTimeAvailabilityRepository = doctorTimeAvailabilityRepository;
            _timeRangeRepository = timeRangeRepository;
        }
        public async Task UpdateDoctorAvailabilityAsync(int doctorId, UpdateDoctorTimeAvailabilityDto availabilityDto)
        {

            var doctor = await _doctorRepository.GetDoctorByIdAsync(doctorId);

            var existingAvailability = doctor.DoctorAvailabilities
                .FirstOrDefault(da => da.DayId == availabilityDto.DayId);

            // conversion of timeSpan from DTO entered.
            TimeSpan fromTime = ConvertToTimeSpan(availabilityDto.From);
            TimeSpan toTime = ConvertToTimeSpan(availabilityDto.To);

            if (existingAvailability == null)
            {
                await AddNewTimeAvailabiltyAsync(doctor, availabilityDto);

            }
            else
            {
                await UpdateExistingAvailability(existingAvailability, fromTime, toTime);
            }
        }
        private async Task<TimeRange> CreateTimeRangeAsync(TimeSpan fromTime, TimeSpan toTime)
        {
            var newTimeRange = new TimeRange { From = fromTime, To = toTime };
            await _timeRangeRepository.AddTimeRangeAsync(newTimeRange);
            return newTimeRange;
        }

        private async Task AddNewTimeAvailabiltyAsync(Doctor doctor, UpdateDoctorTimeAvailabilityDto updateDoctorTimeAvailabilityDto)
        {

            TimeSpan fromTime = ConvertToTimeSpan(updateDoctorTimeAvailabilityDto.From);
            TimeSpan toTime = ConvertToTimeSpan(updateDoctorTimeAvailabilityDto.To);
            var newTimeRange = await _timeRangeRepository.GetTimeRangeAsync(fromTime, toTime)
                              ?? await CreateTimeRangeAsync(fromTime, toTime);

            var newAvailability = new DoctorAvailability
            {
                DayId = updateDoctorTimeAvailabilityDto.DayId,
                DoctorId = doctor.DoctorId,
                TimeRangeId = newTimeRange.TimeRangeId
            };

            await _doctorTimeAvailabilityRepository.AddDoctorAvailabilityAsync(newAvailability);
        }

        private async Task UpdateExistingAvailability(DoctorAvailability existingAvailability, TimeSpan fromTime, TimeSpan toTime)
        {
            var sharedCount = await _doctorTimeAvailabilityRepository.GetTimeRangeUsageCountAsync(existingAvailability.TimeRangeId);

            if (sharedCount > 1)
            {
                // Add new Availability
                var newTimeRange = await _timeRangeRepository.GetTimeRangeAsync(fromTime, toTime)
                               ?? await CreateTimeRangeAsync(fromTime, toTime);

                existingAvailability.TimeRangeId = newTimeRange.TimeRangeId;
                await _doctorTimeAvailabilityRepository.UpdateDoctorAvailabilityAsync(existingAvailability);
            }
            else
            {
                var timeRange = await _timeRangeRepository.GetTimeRangeByIdAsync(existingAvailability.TimeRangeId);
                if (timeRange != null)
                {
                    timeRange.From = fromTime;
                    timeRange.To = toTime;
                    await _timeRangeRepository.UpdateTimeRangeAsync(timeRange);
                }
            }
        }
        private TimeSpan ConvertToTimeSpan(string givenTime)
        {
            try
            {
                return TimeSpan.Parse(givenTime);
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Unable to parse the time, Invalid Time Format,{ex.Message}");
                throw new ArgumentException("Invalid time format.", ex);
            }

        }


    }
}
