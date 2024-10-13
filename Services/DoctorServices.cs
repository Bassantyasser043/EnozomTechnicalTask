using DoctorAvailabiltity.Repository.Dto;
using DoctorAvailabiltity.Repository.Entities;
using DoctorAvailabiltity.Repository.Rep;

namespace DoctorAvailabiltity.Services
{
    public class DoctorServices : IDoctorServices
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IDoctorTimeAvailabilityRepository _timeAvailabilityRepository;
        private readonly ITimeRangeRepository _timeRangeRepository;

        public DoctorServices(IDoctorRepository doctorRepository,
            IDoctorTimeAvailabilityRepository timeAvailabilityRepository,
            ITimeRangeRepository timeRangeRepository)
        {
            _doctorRepository = doctorRepository;
            _timeAvailabilityRepository = timeAvailabilityRepository;
            _timeRangeRepository = timeRangeRepository;
        }

        public async Task InsertDoctor(DoctorDto doctorDto)
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
        /*in the update part mainly we see if the doctorId reference to that day 
         * or not if it isn't exist then we have to the add the time range 
         * if not existed or shared by other doctors in TimeRange Entity
         and update the references in all entities except the doctor Entity,
        the other case if it isn't shared by any other doctors 
        then update the original time range, if the day reference exists then we just try to match the previous cases*/
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

                await _timeAvailabilityRepository.AddDoctorAvailabilityAsync(newAvailability);
            }
            else
            {
                var sharedCount = await _timeRangeRepository.GetTimeRangeUsageCountAsync(existingAvailability.TimeRangeId);

                if (sharedCount > 1)
                {
                    var newTimeRange = await _timeRangeRepository.GetTimeRangeAsync(fromTime, toTime);
                    if (newTimeRange == null)
                    {
                        newTimeRange = new TimeRange { From = fromTime, To = toTime };
                        await _timeRangeRepository.AddTimeRangeAsync(newTimeRange);
                    }

                    existingAvailability.TimeRangeId = newTimeRange.TimeRangeId;
                    await _timeAvailabilityRepository.UpdateDoctorAvailabilityAsync(existingAvailability);
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
