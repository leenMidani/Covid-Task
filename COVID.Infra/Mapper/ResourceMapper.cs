using AutoMapper;
using COVID.Domain.Models;
using COVID.Resources;

namespace COVID.Mapper
{
    public class ResourceMapper : Profile
    {
        public ResourceMapper() {

            CreateMap<Patient, PatientResource>().ReverseMap();
            CreateMap<Vacciene, VaccieneResource>().ReverseMap();
            CreateMap< PatientResource, Patient>();
            CreateMap<PatientHistory, PatientHistoryResource>().ReverseMap();
            CreateMap<PatientHistory, HistoryResource>().ReverseMap();


        }

    
    }
}
