using ProfessorHelp.Dto;
using ProfessorHelp.Models.Entity;

namespace ProfessorHelp.Interfaces;

public interface IMatterRepository
{
    Task<ICollection<Matter>> GetAll(int professorId);
    Task<ICollection<Matter>> GetMatters(int id, int professorId);
    Task<ICollection<Matter>> GetMatters(string title, int professorId);

    Task<bool> CreateMatter(MatterDto matter, int professorId);
    Task<bool> Save();
}
