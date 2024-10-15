namespace Repositories.Dto
{
    public class UpdateDoctorTimeAvailabilityDto
    {
        public int DayId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}
