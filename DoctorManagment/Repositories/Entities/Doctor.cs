namespace Repositories.Entities
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }

        //Navigation with Availability Table
        public List<DoctorAvailability> DoctorAvailabilities { get; set; }
    }
}
