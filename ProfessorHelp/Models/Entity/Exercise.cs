using System.ComponentModel.DataAnnotations;

namespace ProfessorHelp.Models.Entity;

public class Exercise
{

    public long Id { get; set; }
    public required string Description { get; set; }
    public required string Title { get; set; }
    public string? File_Url { get; set; }

    #region Link Response
    public ICollection<Response>? Responses { get; set; }
    #endregion

    #region Link Matter
    public Matter Matter { get; } = null!;
    public required long Matter_Id { get; set; }
    #endregion

    public required DateTime Created_At { get; set; } = DateTime.UtcNow;
    public required DateTime Updated_At { get; set; } = DateTime.UtcNow;
}
