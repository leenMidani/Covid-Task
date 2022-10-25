using COVID.Domain.Context;
using COVID.Domain.Models;
using COVID.Resources;
using ServicesLayer.Interfaces;

namespace COVID.Services
{
    public class PatientHistoryService : IPatientHistoryService
    {
       
        private readonly PatientContext _patientContext;

        public PatientHistoryService(PatientContext patientContext)
        {
            _patientContext = patientContext;
        }
        public async Task<int> ADDPatientHistory(HistoryDto History)
        {
            PatientHistory patientHistory = new PatientHistory
            {
                Patient = _patientContext.Patients.FirstOrDefault(x => x.PatientId == History.PatientId),

                EntryCreatedDate = History.CreatedDate,
                Vacciene = _patientContext.Vacciene.FirstOrDefault(x => x.VaccieneId == History.VaccieneId)

            };

            _patientContext.AddAsync(patientHistory);
            await _patientContext.SaveChangesAsync();
            return patientHistory.Id;

        }


    }
}
