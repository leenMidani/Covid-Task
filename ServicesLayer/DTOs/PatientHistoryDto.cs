namespace COVID.Resources
{
    public class PatientHistoryDto
    {
        public DateTime CreatedDate { get; set; }
        public int Id { get; set; }
        public PatientDto Patient { get; set; }
        public VaccieneDto Vacciene { get; set; }
    }
}
