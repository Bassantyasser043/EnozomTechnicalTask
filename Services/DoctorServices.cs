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

            await _doctorRepository.InsertDoctor(doctor);

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

        //public async Task UpdateDoctorAvailabilityAsync(UpdateDoctorAvailableDto updateDto)
        //{
        //    var existingAvailability = await _doctorAvailabilityRepository
        //        .GetDoctorAvailabilityAsync(updateDto.DayId, updateDto.TimeRangeId);

        //    if (existingAvailability == null)
        //        throw new Exception("Doctor availability not found.");

        //    var timeRange = await _timeRangeRepository.GetTimeRangeByIdAsync(updateDto.TimeRangeId);

        //    if (timeRange == null)
        //        throw new Exception("TimeRange not found.");

        //    // Check for shared TimeRanges
        //    var sharedTimeRanges = await _timeRangeRepository.GetSharedTimeRangesAsync(timeRange.From, timeRange.To);

        //    if (sharedTimeRanges.Count > 1 && !sharedTimeRanges.Any(tr => tr.TimeRangeId == timeRange.TimeRangeId))
        //    {
        //        throw new Exception("Cannot update shared TimeRange.");
        //    }

        //    if (sharedTimeRanges.Count == 1) // Means it's the only one
        //    {
        //        timeRange.From = TimeSpan.Parse(updateDto.TimeRange.From); // assuming you will pass these as strings
        //        timeRange.To = TimeSpan.Parse(updateDto.TimeRange.To);
        //        await _timeRangeRepository.AddAsync(timeRange);
        //    }

        //    existingAvailability.TimeRangeId = timeRange.TimeRangeId;
        //    await _doctorAvailabilityRepository.AddAsync(existingAvailability);


        //}


    }
}
