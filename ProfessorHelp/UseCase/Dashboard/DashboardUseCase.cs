using AutoMapper;
using ProfessorHelp.Interfaces;
using ProfessorHelp.Services.ProfessorLogin;
using ProfessorHelp.Shared.Comunication.Response.Dashboard;
using ProfessorHelp.UseCase.Dashboard.Interfaces;

namespace ProfessorHelp.UseCase.Dashboard;

public class DashboardUseCase : IDashboardUseCase
{
    private readonly IProfessorLogin _login;
    private readonly IMatterReadOnlyRepository _repo;
    private readonly IMapper _mapper;

    public DashboardUseCase(IProfessorLogin login, IMatterReadOnlyRepository repo, IMapper mapper)
    {
        _login = login;
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<ResponseDashboard> Execute()
    {
        Models.Entity.Professor professorLogin = await _login.IsLogin();
        

        List<Models.Entity.Matter> matters = await _repo.RecoverAll(professorLogin.Id);

        return new ResponseDashboard
        {
            Matters = _mapper.Map<List<ResponseAbstract>>(matters)
        };
    }
}
