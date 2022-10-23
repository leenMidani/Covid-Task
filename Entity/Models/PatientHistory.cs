namespace COVID.Domain.Models
{
    public class PatientHistory
    {
        public int Id { get; set; }
        public DateTime EntryCreatedDate { get; set; }
        public Patient Patient { get; set; }
        public Vacciene Vacciene { get; set; }
    }
}
