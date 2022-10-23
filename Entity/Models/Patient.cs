namespace COVID.Domain.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality  { get; set; }

        public string EmiratesID  { get; set; }

        public string Status { get; set; } = "Active";

        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public DateTime CreatedDate { get; set; }
        
        public ICollection<PatientHistory> PatientHistories { get; set; }
    }
}
