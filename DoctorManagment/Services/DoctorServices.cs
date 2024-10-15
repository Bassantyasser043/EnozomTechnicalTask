
using Repositories.Dto;
using Repositories.Entities;
using Repositories.Rep;

namespace Services
{
    public class DoctorServices : IDoctorServices
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorServices(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<Doctor> AddDoctorAsync(DoctorDto doctorDto)
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

            return  await _doctorRepository.AddDoctorAsync(doctor);
        }
        public async Task<DoctorDetailsDto?> GetDoctorByIdAsync(int doctorId)
        {
            var doctor = await _doctorRepository.GetDoctorByIdAsync(doctorId);

            if (doctor == null) return null;

            var availabilities = doctor.DoctorAvailabilities
                .Where(da => da.DayId != null)
                .Select(da => new DoctorAvailabilityDetailsDto
                {
                    DayName = da.Day.DayName,
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
