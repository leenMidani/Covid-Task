using AutoMapper;
using COVID.Domain.Context;
using COVID.Domain.Models;
using COVID.Mapper;
using COVID.Resources;
using COVID.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COVID.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;


        public PatientController(IPatientService patientService, PatientContext patientContext, IMapper mapper)
        {
            _patientService = patientService;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Create(PatientResource patientResource)
        {
            try
            {
                var resources = _mapper.Map<PatientResource,Patient >(patientResource);

                
                var result = await _patientService.CreatePatient(resources);
                if (result == 0)
                {
                    return BadRequest();
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpGet]
        //public async Task<IActionResult> ViewPatient(int id)
        //{
        //    try
        //    {
        //        var pa = await _patientService.GetPatientLastStatus(id);

        //        var mapperd = _mapper.Map<Patient, PatientResource>(pa);


        //        var patient = await _patientContext.Patients
        //      // .Include(x => x.PatientHistories)
        //      .Join(_patientContext.PatientHistories,
        //      p => p.PatientId,
        //      h => h.Patient.PatientId, (p, h) => new { p, h })
        //      .Where(p => p.p.PatientId == id)
        //      .Take(1)
        //      .OrderByDescending(p => p.h.EntryCreatedDate).FirstOrDefaultAsync();

        //        return Ok(patient);
        //    }

        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        [HttpGet]
        [Route("history/{id}")]
        public async Task<IActionResult> ViewPatientHistory(int id)
        {
            try
            {
                var pa = await _patientService.GetPatientLastStatus(id);

                if (pa != null)
                {
                    PatientHistoryResource mapperd = _mapper.Map<PatientHistory, PatientHistoryResource>(pa);

                    return Ok(mapperd);
                }
                else
                    return NotFound();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> ViewAllPatients()
        {
            try
            {
                var pa = await _patientService.GetPatients();
                List<PatientResource> mapperd = new List<PatientResource>();
                
                foreach (var patients in pa)
                {
                    mapperd.Add(_mapper.Map<Patient, PatientResource>(patients));


                }
                

                return Ok(mapperd);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("histories/{id}")]
        public async Task<IActionResult> ViewPatientDetailedHistory(int id)
        {
            try
            {
               List<PatientHistoryResource> mapperd = new List<PatientHistoryResource>();
                var pa = await _patientService.GetPatientDeatailedHistory(id);
                foreach (var patientHistory in pa)
                {
                     mapperd.Add (_mapper.Map<PatientHistory, PatientHistoryResource>(patientHistory)) ;

                    
                }
                return Ok(mapperd);

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("update/{id}")]

        public async Task<IActionResult> UpdatePatient(int id, PatientResource patient)
        {
            try
            {
                Patient mapperd = _mapper.Map< PatientResource, Patient>(patient);


                var result = await _patientService.UpdatePatient(id, mapperd);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("remove/{id}")]
        public async Task<IActionResult> RemovePatient(int id)
        {
            try
            {
                var result = await _patientService.DeletePatient(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
