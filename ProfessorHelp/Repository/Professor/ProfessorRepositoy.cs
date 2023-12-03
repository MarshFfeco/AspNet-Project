using Microsoft.EntityFrameworkCore;
using ProfessorHelp.Data;
using ProfessorHelp.Interfaces;

namespace ProfessorHelp.Repository.Professor;

public class ProfessorRepositoy : IProfessorWriteOnlyRepository, IProfessorReadOnlyRepository
{
    private readonly DataContext _db;

    public ProfessorRepositoy(DataContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public async Task CreateProfessor(Models.Entity.Professor professor)
    {
        await _db.professor.AddAsync(professor);
        await _db.SaveChangesAsync();
    }

    public async Task<Models.Entity.Professor> FindByEmail(string email)
    {
        return await _db.professor.AsNoTracking().FirstOrDefaultAsync(p => p.Email.Equals(email));
    }

    public async Task<Models.Entity.Professor> FindById(long id)
    {
        return await _db.professor.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<bool> isExistProfessorWithEmail(string email)
    {
        return await _db.professor.AnyAsync(p => p.Email.Equals(email));
    }

    public async Task<Models.Entity.Professor> Login(string email, string password)
    {
        return await _db.professor
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Email.Equals(email) && p.Password.Equals(password));
    }

    public void Udpate(Models.Entity.Professor professor)
    {
        _db.Update(professor);
        _db.SaveChanges();
    }
}
