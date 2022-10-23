using COVID.Domain.Models;

namespace COVID.Services
{
    public interface IPatientService
    {
        Task<int> CreatePatient(Patient patient);
        Task<List<PatientHistory>> GetPatientDeatailedHistory(int id);
        Task<PatientHistory> GetPatientLastStatus(int id);
        Task<Patient> UpdatePatient(int id, Patient patient);
        Task<String> DeletePatient(int id);
        Task<List<Patient>> GetPatients();
    }
}