namespace ProfessorHelp.Models.Entity;

public class Student : BaseModel
{
    #region Link StudentMatter
    public ICollection<StudentMatter>? StudentMatters { get;}
    #endregion
}
