using COVID.Domain.Models;
using COVID.Resources;

namespace COVID.Services
{
    public interface IPatientHistoryService
    {
        Task<int> ADDPatientHistory(HistoryResource patientHistory);
    }
}