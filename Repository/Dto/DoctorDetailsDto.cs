namespace DoctorAvailabiltity.Repository.Dto
{
    public class DoctorDetailsDto
    {
        public string DoctorName { get; set; }
        public List<DoctorAvailabilityDetailsDto> Availabilities { get; set; }

    }


}
