namespace DoctorAvailabiltity.Repository.Entities
{
    public class DoctorAvailability
    {
        public int DoctorAvailabilityId { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public int DayId { get; set; }
        public Day Day { get; set; }

        public int TimeRangeId { get; set; }
        public TimeRange TimeRanges { get; set; }
    }
}
