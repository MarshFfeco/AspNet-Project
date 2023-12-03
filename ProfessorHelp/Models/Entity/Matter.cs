using System.ComponentModel.DataAnnotations.Schema;

namespace ProfessorHelp.Models.Entity;

[Table("matter")]
public class Matter
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public int? Module { get; set; }
    public string? Classroom { get; set; }
    public string? Shift { get; set; }
    public string? Code { get; set; }

    #region Link Professor
    public Professor Professor { get; } = null!;
    public required long Professor_Id { get; set; }
    #endregion

    #region Link Exercise
    public ICollection<Exercise>? Exercises { get; }
    #endregion

    #region Link StudentMatter
    public ICollection<StudentMatter>? StudentMatters { get; }
    #endregion

    #region Link GenerateStudentMatter
    public ICollection<GenerateStudentMatter>? GenerateStudentMatter { get; }
    #endregion
}
