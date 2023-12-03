using ProfessorHelp.Shared.Comunication.Response.Dashboard;

namespace ProfessorHelp.UseCase.Dashboard.Interfaces;

public interface IDashboardUseCase
{
    public Task<ResponseDashboard> Execute();
}
