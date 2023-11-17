using Microsoft.EntityFrameworkCore;
using ProfessorHelp.Data;
using ProfessorHelp.Dto;
using ProfessorHelp.Interfaces;
using ProfessorHelp.Models.Entity;

namespace ProfessorHelp.Repository;

public class MatterRepository : IMatterRepository
{
    private readonly DataContext _db;

    public MatterRepository(DataContext db)
    {
        _db = db ?? throw new ArgumentNullException("Erro no MatterRepository! _db");
    }

    public async Task<bool> CreateMatter(MatterDto matter, int professorId) 
    {
        var professorEntity = await _db.professor.Where(p => p.Id == professorId).FirstOrDefaultAsync();

        var newMatter = new Matter
        {
            Module = matter.Module,
            Classroom = matter.Classroom,
            Shift = matter.Shift,
            Code = matter.Code,
            Title = matter.Title,
            Professor_Id = professorId,
        };

        _db.Add(newMatter);

        return await Save();
    }

    public async Task<ICollection<Matter>> GetAll(int professorId)
    {
        return await _db.matter.OrderBy(m => m.Title).Where(m => m.Professor_Id == professorId).ToListAsync();
    }

    public async Task<ICollection<Matter>> GetMatters(string title, int professorId)
    {
        return await _db.matter.Where(m => m.Title == title && m.Professor_Id == professorId).ToListAsync();
    }

    public async Task<ICollection<Matter>> GetMatters(int id, int professorId)
    { 
        return await _db.matter.Where(m => m.Id == id && m.Professor_Id == professorId).ToListAsync();
    }

    public async Task<bool> Save()
    {
        int save =  await _db.SaveChangesAsync();

        return save > 0 ? true : false; 
    }
}
