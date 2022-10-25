using COVID.Domain.Models;
using COVID.Resources;

namespace ServicesLayer.Interfaces
{
    public interface IPatientHistoryService
    {
        Task<int> ADDPatientHistory(HistoryDto patientHistory);
    }
}