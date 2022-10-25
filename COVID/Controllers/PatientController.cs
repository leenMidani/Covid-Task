using AutoMapper;
using COVID.Domain.Context;
using COVID.Domain.Models;
using COVID.Mapper;
using COVID.Resources;
using COVID.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServicesLayer.Interfaces;
using System.Net;

namespace COVID.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Create(PatientDto patientResource)
        {
            try
            {
                var result = await _patientService.CreatePatient(patientResource);
                if (result == -1)
                {
                   
                    return BadRequest("Name already exists.");
                }
                if(result == 0)
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


        [HttpGet]
        [Route("history/{id}")]
        public async Task<IActionResult> ViewPatientHistory(int id)
        {
            try
            {
                var pa = await _patientService.GetPatientLastStatus(id);

                if (pa != null)
                {

                    return Ok(pa);
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
              
                return Ok(pa);
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
               List <PatientHistoryDto> pa = await _patientService.GetPatientDeatailedHistory(id);
               
                return Ok(pa);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("update/{id}")]

        public async Task<IActionResult> UpdatePatient(int id, PatientDto patient)
        {
            try
            {

                var result = await _patientService.UpdatePatient(id, patient);
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
