namespace COVID.Domain.Models
{
    public class Vacciene
    {
        public int VaccieneId { get; set; }
        public string VaccieneName { get; set; }

        ///number of Vacciene validity in months 
        public int ActiveMonths { get; set; }

        public ICollection<PatientHistory> PatientHistory { get; set; }
    }
}
