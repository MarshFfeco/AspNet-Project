using ProfessorHelp.Models.Entity;

namespace ProfessorHelp.Interfaces;

public interface IProfessorWriteOnlyRepository
{
    Task CreateProfessor(Professor professor);

    void Udpate(Professor professor);
}
