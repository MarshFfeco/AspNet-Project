using Microsoft.EntityFrameworkCore;
using ProfessorHelp.Models.Entity;

namespace ProfessorHelp.Data;

public class DataContext : DbContext
{
    private IConfiguration _configuration;

    public DataContext(DbContextOptions options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<Professor> professor { get; set; }
    public DbSet<Student> student { get; set; }
    public DbSet<GenerateStudent> generateStudents { get; set; }
    public DbSet<GenerateStudentMatter> generateStudentMatters { get; set; }
    public DbSet<Exercise> exercise { get; set; }
    public DbSet<Response> response { get; set; }
    public DbSet<Matter> matter { get; set; }
    public DbSet<StudentMatter> studentMatter { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Professor>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.HasMany(p => p.Matters)
            .WithOne(m => m.Professor)
            .HasForeignKey(m => m.Professor_Id)
            .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Matter>(entity =>
        {
            entity.HasKey(m => m.Id);

            entity.HasMany(m => m.Exercises)
            .WithOne(e => e.Matter)
            .HasForeignKey(e => e.Matter_Id)
            .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(p => p.StudentMatters)
            .WithOne(sm => sm.Matter)
            .HasForeignKey(sm => sm.Matter_Id)
            .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(m => m.GenerateStudentMatter)
            .WithOne(gs => gs.Matter)
            .HasForeignKey(gs => gs.Matter_Id)
            .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasMany(r => r.Responses)
            .WithOne(e => e.Exercise)
            .HasForeignKey(r => r.Exercise_Id)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(s => s.Id);

            entity.HasMany(s => s.StudentMatters)
            .WithOne(sm => sm.Student)
            .HasForeignKey(sm => sm.Student_Id) 
            .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<GenerateStudent>(entity =>
        {
            entity.HasKey(g => g.Id);

            entity.HasMany(g => g.GenerateStudentMatter)
            .WithOne(gs => gs.GenerateStudent)
            .HasForeignKey(gs => gs.Generate_Student_Id)
            .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<GenerateStudentMatter>(entity =>
        {
            entity.HasKey(g => g.Id);
        });

        modelBuilder.Entity<StudentMatter>(entity =>
        {
            entity.HasKey(sm => sm.Id);
        });

        modelBuilder.Entity<Response>(entity =>
        {
            entity.HasKey(s => s.Id);
        });  
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connection = _configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException("Without Connection String!");
        optionsBuilder.UseMySQL(connection);
    }
}
