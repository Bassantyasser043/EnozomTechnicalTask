using DoctorAvailabiltity.DoctorManagment.Dto;
using DoctorAvailabiltity.DoctorManagment.Entities;
using DoctorAvailabiltity.Repository.Entities;
using DoctorAvailabiltity.TimeAvailabilityManagment.Dto;
using DoctorAvailabiltity.TimeAvailabilityManagment.Repository;
using DoctorManagment.Repository.DoctorRepository;

namespace DoctorAvailabiltity.DoctorManagment.Services
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
    }
}
