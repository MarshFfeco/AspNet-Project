namespace ProfessorHelp.Models.Entity;

public class Response
{
    public long Id { get; set; }
    public required string Description { get; set; }
    public required string Title { get; set; }
    public string? File_Url { get; set; }

    #region Link Exercise
    public Exercise Exercise { get; } = null!;
    public required long Exercise_Id { get; set; }
    #endregion
    public required DateTime Created_At { get; set; } = DateTime.UtcNow;
    public required DateTime Updated_At { get; set; } = DateTime.UtcNow;
}
