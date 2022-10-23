using COVID.Domain.Context;
using COVID.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace COVID.Services
{
    public class PatientService : IPatientService
    {
        private readonly PatientContext _patientContext;

        public PatientService(PatientContext patientContext)
        {
            _patientContext = patientContext;
        }
        public async Task<int> CreatePatient(Patient patient)
        {
            var oldpatient = await _patientContext.Patients
                .FirstOrDefaultAsync(x => x.PatientName == patient.PatientName);
            if (oldpatient != null)
            {
                return 0;
            }

            _patientContext.AddAsync(patient);
            await _patientContext.SaveChangesAsync();
            return patient.PatientId;

        }
        ///Get List of All patients
        public async Task<List<Patient>> GetPatients()
        {
            List<Patient> patients =  _patientContext.Patients.ToList();

            foreach(Patient patient in patients)
            {
                Updatestatus(patient.PatientId);
            }
            List<Patient> Upatients = await _patientContext.Patients.ToListAsync();

            return Upatients;

        }
        //Get Patient basic info with latest history record
        public async Task<PatientHistory> GetPatientLastStatus(int id)
        {
            Updatestatus(id);

            Patient pa = await _patientContext.Patients.FirstOrDefaultAsync(x => x.PatientId == id);

            if (pa != null)
            {
                
                PatientHistory patienthistory = await _patientContext.PatientHistories
               .Include(x => x.Patient)
               .Include(y => y.Vacciene)           
              .Where(p => p.Patient.PatientId == id)
              .OrderByDescending(p => p.EntryCreatedDate).FirstOrDefaultAsync();

              
                return patienthistory;
            }
            else
                return null;

        }


      
        public async Task<Patient> UpdatePatient(int id, Patient patient)
        {
            Patient pa = await _patientContext.Patients.FirstOrDefaultAsync(x => x.PatientId == id);
            if (pa != null)
            {
                pa.PatientName = patient.PatientName;
                pa.EmiratesID = patient.EmiratesID;
                pa.DateOfBirth= patient.DateOfBirth;
                pa.Nationality = patient.Nationality;
                pa.Status= patient.Status;
                pa.PhoneNumber= patient.PhoneNumber;
                _patientContext.Update(pa);
                await _patientContext.SaveChangesAsync();
                return pa;
            }
            else
                return null;
            

        }
        public async Task<String> DeletePatient(int id)
        {

            Patient pa = await _patientContext.Patients.FirstOrDefaultAsync(x => x.PatientId == id);
            if (pa != null)
            {
                _patientContext.Patients.Remove(pa);
                await _patientContext.SaveChangesAsync();
                return "Removed";
                
            };
            return null;

        }

    
        public async Task<List<PatientHistory>> GetPatientDeatailedHistory(int id)
        {
            Updatestatus(id);

            List<PatientHistory> patienthistory = await _patientContext.PatientHistories
               .Include(x => x.Patient)
               .Include(y => y.Vacciene)  
              .Where(p => p.Patient.PatientId == id)
              .OrderByDescending(p => p.EntryCreatedDate).ToListAsync();

            return patienthistory;

        }



        //this function update status to expired/Active we usually use scheduler to auto check 
        private void Updatestatus(int id)
        {
            Patient status = _patientContext.Patients
                          .FirstOrDefault(p => p.PatientId == id);
            PatientHistory patienthistory = _patientContext.PatientHistories
               .Include(x => x.Patient)
               .Include(y => y.Vacciene)
              .Where(p => p.Patient.PatientId == id)
              .OrderByDescending(p => p.EntryCreatedDate).FirstOrDefault();
            if (patienthistory != null)
            {
                if (DateTime.Now > patienthistory.EntryCreatedDate.AddMonths(patienthistory.Vacciene.ActiveMonths) )
                {
                    status.Status = "Expierd";                   
                }
                else
                {
                    status.Status = "Active";
                }
            }
            else
            { status.Status = "NotActive"; }

            _patientContext.Patients.Update(status);
            _patientContext.SaveChanges();
        }
    }
}
