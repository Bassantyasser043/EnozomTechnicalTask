namespace DoctorAvailabiltity.Repository.Entities
{
    public class Day
    {
        public int DayId { get; set; }
        public string DayName { get; set; }

        public List<DoctorAvailability> DoctorAvailabilities { get; set; }
    }
}
