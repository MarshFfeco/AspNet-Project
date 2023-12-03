using ProfessorHelp.Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfessorHelp.Models.Entity;

[Table("generate_student")]
public class GenerateStudent
{
    public long Id { get; set; }
    public required string First_Name { get; set; }
    public required string Last_Name { get; set; }
    public required Sex Sex { get; set; }

    #region Link Matter
    public ICollection<GenerateStudentMatter> GenerateStudentMatter { get; } = null!;

    #endregion
}
