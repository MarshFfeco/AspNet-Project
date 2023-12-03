using AutoMapper;
using HashidsNet;
using ProfessorHelp.Models.Entity;
using ProfessorHelp.Shared.Comunication.Request.Matter;
using ProfessorHelp.Shared.Comunication.Request.Professor;
using ProfessorHelp.Shared.Comunication.Response.Dashboard;
using ProfessorHelp.Shared.Comunication.Response.Matter;

namespace ProfessorHelp.Services.Automapper;

public class MappingProfiles : Profile
{
    private readonly IHashids _hashids;

    public MappingProfiles(IHashids hashids)
    {
        _hashids = hashids;

        CreateMap<RequestSignUpProfessor, Professor>();
        CreateMap<RequestMatterCreate, Matter>();

        CreateMap<Matter, ResponseMatterCreate>()
            .ForMember(dest => dest.Id, config => config.MapFrom(orig => _hashids.EncodeLong(orig.Id)));

        CreateMap<Matter, ResponseAbstract>()
            .ForMember(dest => dest.Id, config => config.MapFrom(orig => _hashids.EncodeLong(orig.Id)))
            .ForMember(dest => dest.Amoutstudent, config => config.MapFrom(ori => ori.GenerateStudentMatter.Count));
    }
}
