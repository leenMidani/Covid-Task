using COVID.Domain.Models;
using COVID.Resources;

namespace ServicesLayer.Interfaces
{
    public interface IPatientService
    {
        Task<int> CreatePatient(PatientDto patientDto);
        Task<List<PatientHistoryDto>> GetPatientDeatailedHistory(int id);
        Task<PatientHistoryDto> GetPatientLastStatus(int id);
        Task<PatientDto> UpdatePatient(int id, PatientDto patient);
        Task<string> DeletePatient(int id);
        Task<List<PatientDto>> GetPatients();
    }
}