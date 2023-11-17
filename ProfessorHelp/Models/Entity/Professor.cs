using System.ComponentModel.DataAnnotations.Schema;

namespace ProfessorHelp.Models.Entity;

[Table("professor")]
public class Professor : BaseModel
{
    #region Link Matter
    public ICollection<Matter>? Matters { get; }
    #endregion
}
