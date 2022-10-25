using AutoMapper;
using COVID.Domain.Models;
using COVID.Resources;
using COVID.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesLayer.Interfaces;

namespace COVID.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientHistoryController : ControllerBase
    {
        private readonly IPatientHistoryService _patientHistoryService;
        private readonly IMapper _mapper;


        public PatientHistoryController(IPatientHistoryService patientHistoryService)
        {
            _patientHistoryService = patientHistoryService;
        }
        [HttpPost]
        [Route("AddHistory")]
        public async Task<IActionResult> Create(HistoryDto historyDto)
        {
            try
            {

                var result = await _patientHistoryService.ADDPatientHistory(historyDto);
                
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
