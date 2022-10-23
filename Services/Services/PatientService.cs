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

        public async Task<List<Patient>> GetPatients()
        {
            List<Patient> patients = await _patientContext.Patients.ToListAsync();

            foreach(Patient patient in patients)
            {
                Updatestatus(patient.PatientId);
            }


            return patients;

        }

        public async Task<PatientHistory> GetPatientLastStatus(int id)
        {
            PatientHistory patienthistory = await _patientContext.PatientHistories
               .Include(x => x.Patient)
               .Include(y => y.Vacciene)
              //.Join(_patientContext.Patient,
              //p => p.PatientId,
              //h => h.Patient.PatientId, (p, h) => new { p, h })
              .Where(p => p.Patient.PatientId == id)
              .OrderByDescending(p => p.EntryCreatedDate).FirstOrDefaultAsync();

           // if (DateTime.Now > patienthistory.EntryCreatedDate.AddMonths(patienthistory.Vacciene.ActiveMonths) && patienthistory.Patient.Status == "Active")
            //{
                Updatestatus(id);
           // }
            return patienthistory;

        }


      
        public async Task<Patient> UpdateEmployee(int id, Patient patient)
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

                return null;
            };
            _patientContext.Patients.Remove(pa);
            await _patientContext.SaveChangesAsync();
            return "Removed";

        }

    
    
        public async Task<List<PatientHistory>> GetPatientDeatailedHistory(int id)
        {
            Updatestatus(id);

            List<PatientHistory> patienthistory = await _patientContext.PatientHistories
               .Include(x => x.Patient)
               .Include(y => y.Vacciene)
             
              .Where(p => p.Patient.PatientId == id)
              .OrderByDescending(p => p.EntryCreatedDate).ToListAsync();

            
            // }
            return patienthistory;

        }



        //this function update status to expired we usually use scheduler to auto check 
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
                if (DateTime.Now > patienthistory.EntryCreatedDate.AddMonths(patienthistory.Vacciene.ActiveMonths) && patienthistory.Patient.Status == "Active")
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
