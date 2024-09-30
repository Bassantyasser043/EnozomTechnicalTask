namespace DoctorAvailabiltity.Repository.Dto
{
    public class UpdateDoctorTimeAvailabilityDto
    {
        public int DayId { get; set; }
        public TimeRangeDto TimeRange { get; set; }
    }
}
