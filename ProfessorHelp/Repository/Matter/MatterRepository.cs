using Microsoft.EntityFrameworkCore;
using ProfessorHelp.Data;
using ProfessorHelp.Interfaces;

namespace ProfessorHelp.Repository.Matter;

public class MatterRepository : IMatterWriteOnlyRepository, IMatterReadOnlyRepository
{
    private readonly DataContext _db;
    public MatterRepository(DataContext db)
    {
        _db = db;
    }

    public async Task CreateMatter(Models.Entity.Matter matter)
    {
        await _db.matter.AddAsync(matter);
        await _db.SaveChangesAsync();
    }

    public async Task<List<Models.Entity.Matter>> RecoverAll(long professorId)
    {
        return await _db.matter.AsNoTracking().Where(m => m.Id == professorId).ToListAsync(); 
    }
}
