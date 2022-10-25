using AutoMapper;
using COVID.Domain.Context;
using COVID.Domain.Models;
using COVID.Resources;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ServicesLayer.Interfaces;

namespace COVID.Services
{
    public class PatientService : IPatientService
    {
        private readonly PatientContext _patientContext;
        private readonly IMapper _mapper;
        private readonly IValidator<PatientDto> _validator;

        public PatientService(PatientContext patientContext, IMapper mapper, IValidator<PatientDto> validator)
        {
            _patientContext = patientContext;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<int> CreatePatient(PatientDto patient)
        {
             await _validator.ValidateAndThrowAsync(patient);
            var resources = _mapper.Map<PatientDto, Patient>(patient);
            var oldpatient = await _patientContext.Patients
                .FirstOrDefaultAsync(x => x.PatientName == patient.PatientName);
            if (oldpatient != null)
            {
                return -1;
            }

            await _patientContext.AddAsync(resources);
            await _patientContext.SaveChangesAsync();
            return patient.PatientId;

        }
        ///Get List of All patients
        public async Task<List<PatientDto>> GetPatients()
        {
            List<Patient> patients =  _patientContext.Patients.ToList();

            foreach(Patient patient in patients)
            {
                Updatestatus(patient.PatientId);
            }
            List<Patient> Upatients = await _patientContext.Patients.ToListAsync();
            List<PatientDto> mapped = new List<PatientDto>();
            
            foreach (var p in Upatients)
            {
                mapped.Add(_mapper.Map<Patient, PatientDto>(p));


            }

            return mapped;

        }
        //Get Patient basic info with latest history record
        public async Task<PatientHistoryDto> GetPatientLastStatus(int id)
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

                PatientHistoryDto mapped = _mapper.Map<PatientHistory, PatientHistoryDto>(patienthistory);


                return mapped;
            }
            else
                return null;

        }


      
        public async Task<PatientDto> UpdatePatient(int id, PatientDto patient)
        {
            await _validator.ValidateAndThrowAsync(patient);
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
                PatientDto mapped = _mapper.Map< Patient, PatientDto> (pa);

                return mapped;
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
    
        public async Task<List<PatientHistoryDto>> GetPatientDeatailedHistory(int id)
        {
            Updatestatus(id);

            List<PatientHistory> patienthistory = await _patientContext.PatientHistories
               .Include(x => x.Patient)
               .Include(y => y.Vacciene)  
              .Where(p => p.Patient.PatientId == id)
              .OrderByDescending(p => p.EntryCreatedDate).ToListAsync();


            List<PatientHistoryDto> mapperd = new List<PatientHistoryDto>();
           
            foreach (var History in patienthistory)
            {
                mapperd.Add(_mapper.Map<PatientHistory, PatientHistoryDto>(History));


            }


            return mapperd;

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
