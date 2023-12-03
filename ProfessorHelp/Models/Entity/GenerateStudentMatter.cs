using System.ComponentModel.DataAnnotations.Schema;

namespace ProfessorHelp.Models.Entity;

[Table("generate_student_matter")]
public class GenerateStudentMatter
{
    public required long Id { get; set; }

    #region Link GenerateStudent
    public GenerateStudent GenerateStudent { get; } = null!;
    public required long Generate_Student_Id { get; set; }

    #endregion

    #region Link Matter
    public Matter Matter { get; } = null!;
    public required long Matter_Id { get; set; }
    #endregion
}
