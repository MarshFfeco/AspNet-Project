using System.ComponentModel.DataAnnotations.Schema;

namespace ProfessorHelp.Models.Entity;

[Table("student_matter")]
public class StudentMatter
{
    public long Id { get; set; }

    #region Student
    public Student? Student { get; }
    public long Student_Id { get; set; }
    #endregion

    #region Matter
    public Matter? Matter { get; }
    public long Matter_Id { get; set; }
    #endregion
}
