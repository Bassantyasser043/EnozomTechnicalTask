namespace DoctorAvailabiltity.Repository.Entities
{
    public class TimeRange
    {
        public int TimeRangeId { get; set; }
        public TimeSpan From { get; set; }  
        public TimeSpan To { get; set; }      
        public List<DoctorAvailability> DoctorAvailabilities { get; set; }
    }
}
