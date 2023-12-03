using ProfessorHelp.Models.Entity;

namespace ProfessorHelp.Interfaces;

public interface IMatterWriteOnlyRepository
{
    Task CreateMatter(Matter matter);
}
