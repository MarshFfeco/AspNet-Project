namespace ProfessorHelp.Interfaces;

public interface IMatterReadOnlyRepository
{
    Task<List<Models.Entity.Matter>> RecoverAll(long professorId);
}
