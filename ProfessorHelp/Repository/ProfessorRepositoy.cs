using Microsoft.EntityFrameworkCore;
using ProfessorHelp.Data;
using ProfessorHelp.Interfaces;
using ProfessorHelp.Models.Entity;

namespace ProfessorHelp.Repository;

public class ProfessorRepositoy : IProfessorWriteOnlyRepository, IProfessorReadOnlyRepository
{
    private readonly DataContext _db;

    public ProfessorRepositoy(DataContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public async Task CreateProfessor(Professor professor)
    {
        await _db.professor.AddAsync(professor);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> isExistProfessorWithEmail(string email)
    {
        return await _db.professor.AnyAsync(p => p.Email.Equals(email));
    }

    public async Task<Professor> Login(string email, string password)
    {
        return await _db.professor
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Email.Equals(email) && p.Password.Equals(password));
    }
}
