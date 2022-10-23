namespace COVID.Resources
{
    public class PatientHistoryResource
    {
        public DateTime CreatedDate { get; set; }
        public int Id { get; set; }
        public PatientResource Patient { get; set; }
        public VaccieneResource Vacciene { get; set; }
    }
}
