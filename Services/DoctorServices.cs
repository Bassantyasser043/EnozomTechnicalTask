using DoctorAvailabiltity.Repository.Dto;
using DoctorAvailabiltity.Repository.Entities;
using DoctorAvailabiltity.Repository.Rep;

namespace DoctorAvailabiltity.Services
{
    public class DoctorServices : IDoctorServices
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IDoctorTimeAvailabilityRepository _doctorTimeAvailabilityRepository;
        private readonly ITimeRangeRepository _timeRangeRepository;

        public DoctorServices(IDoctorRepository doctorRepository,
            IDoctorTimeAvailabilityRepository doctorTimeAvailabilityRepository,
            ITimeRangeRepository timeRangeRepository)
        {
            _doctorRepository = doctorRepository;
            _doctorTimeAvailabilityRepository = doctorTimeAvailabilityRepository;
            _timeRangeRepository = timeRangeRepository;
        }

        public async Task AddDoctorAsync(DoctorDto doctorDto)
        {
            var doctor = new Doctor
            {
                DoctorName = doctorDto.DoctorName,
                DoctorAvailabilities = new List<DoctorAvailability>()
            };

            foreach (var availabilityDto in doctorDto.Availabilities)
            {
                var doctorAvailability = new DoctorAvailability
                {
                    DayId = availabilityDto.DayId,
                    TimeRangeId = availabilityDto.TimeRangeId,
                    Doctor = doctor
                };

                doctor.DoctorAvailabilities.Add(doctorAvailability);
            }

            await _doctorRepository.AddDoctorAsync(doctor);

            Console.WriteLine($"Doctor created successfully! ID: {doctor.DoctorId}, Name: {doctor.DoctorName}");

            Console.WriteLine("Availabilities:");
            foreach (var availability in doctor.DoctorAvailabilities)
            {
                Console.WriteLine($" - Day ID: {availability.DayId}, Time Range ID: {availability.TimeRangeId}");
            }
        }
        public async Task<DoctorDetailsDto?> GetDoctorByIdAsync(int doctorId)
        {
            var doctor = await _doctorRepository.GetDoctorByIdAsync(doctorId);

            if (doctor == null) return null;

            var availabilities = doctor.DoctorAvailabilities
                .Where(da => da.DayId != null)
                .Select(da => new DoctorAvailabilityDetailsDto
                {
                    DayId = da.Day.DayId,
                    DayName = da.Day.DayName,
                    TimeRangeId = da.TimeRangeId,
                    TimeRange = $"{da.TimeRanges.From:hh\\:mm} - {da.TimeRanges.To:hh\\:mm}"
                })
                .ToList();

            return new DoctorDetailsDto
            {
                DoctorName = doctor.DoctorName,
                Availabilities = availabilities
            };
        }

       
        public async Task UpdateDoctorAvailabilityAsync(int doctorId, UpdateDoctorTimeAvailabilityDto availabilityDto)
        {
            var doctor = await _doctorRepository.GetDoctorByIdAsync(doctorId);
            if (doctor == null)
            {
                throw new Exception("Doctor not found.");
            }
            var existingAvailability = doctor.DoctorAvailabilities
                .FirstOrDefault(da => da.DayId == availabilityDto.DayId);

            // conversion of timeSpan from DTO entered.
            TimeSpan fromTime = TimeSpan.Parse(availabilityDto.TimeRange.From);
            TimeSpan toTime = TimeSpan.Parse(availabilityDto.TimeRange.To);

            if (existingAvailability == null)
            {
                var newTimeRange = await _timeRangeRepository.GetTimeRangeAsync(fromTime, toTime);
                if (newTimeRange == null)
                {
                    newTimeRange = new TimeRange { From = fromTime, To = toTime };
                    await _timeRangeRepository.AddTimeRangeAsync(newTimeRange);
                }

                var newAvailability = new DoctorAvailability
                {
                    DayId = availabilityDto.DayId,
                    DoctorId = doctorId,
                    TimeRangeId = newTimeRange.TimeRangeId
                };

                await _doctorTimeAvailabilityRepository.AddDoctorAvailabilityAsync(newAvailability);
            }
            else
            {
                var sharedCount = await _doctorTimeAvailabilityRepository.GetTimeRangeUsageCountAsync(existingAvailability.TimeRangeId);

                if (sharedCount > 1)
                {
                    var newTimeRange = await _timeRangeRepository.GetTimeRangeAsync(fromTime, toTime);
                    if (newTimeRange == null)
                    {
                        newTimeRange = new TimeRange { From = fromTime, To = toTime };
                        await _timeRangeRepository.AddTimeRangeAsync(newTimeRange);
                    }

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


        }
    }
}
