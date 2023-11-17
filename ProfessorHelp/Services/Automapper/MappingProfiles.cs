using AutoMapper;
using ProfessorHelp.Dto;
using ProfessorHelp.Models.Entity;
using ProfessorHelp.Shared.Comunication.Request;

namespace ProfessorHelp.Services.Automapper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Matter, MatterDto>();
        CreateMap<RequestSignUpProfessor, Professor>();
    }
}
