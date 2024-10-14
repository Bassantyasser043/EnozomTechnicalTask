namespace DoctorAvailabiltity.DoctorManagment.Dto
{
    public class DoctorDto
    {
        public string DoctorName { get; set; }
        public List<DoctorAvailabilityDto> Availabilities { get; set; }

    }
}
