using AutoMapper;
using COVID.Domain.Models;
using COVID.Resources;
using COVID.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace COVID.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientHistoryController : ControllerBase
    {
        private readonly IPatientHistoryService _patientHistoryService;
        private readonly IMapper _mapper;


        public PatientHistoryController(IPatientHistoryService patientHistoryService, IMapper mapper)
        {
            _patientHistoryService = patientHistoryService;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("AddHistory")]
        public async Task<IActionResult> Create(HistoryResource historyResource)
        {
            try
            {

                var result = await _patientHistoryService.ADDPatientHistory(historyResource);
                
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
