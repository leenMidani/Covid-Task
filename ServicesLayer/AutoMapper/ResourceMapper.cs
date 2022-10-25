using AutoMapper;
using COVID.Domain.Models;
using COVID.Resources;

namespace COVID.Mapper
{
    public class ResourceMapper : Profile
    {
        public ResourceMapper() {

            CreateMap<Patient, PatientDto>();
            CreateMap<Vacciene, VaccieneDto>().ReverseMap();
            CreateMap< PatientDto, Patient>();
            CreateMap<PatientHistory, PatientHistoryDto>().ReverseMap();
            CreateMap<PatientHistory, HistoryDto>().ReverseMap();


        }

    
    }
}
