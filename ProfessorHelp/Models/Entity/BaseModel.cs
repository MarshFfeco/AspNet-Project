namespace ProfessorHelp.Models.Entity;

public abstract class BaseModel
{
    public long Id { get; set; }
    public required string First_Name { get; set; }
    public required string Last_Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Sex { get; set; }
    public required DateTime Created_At { get; set; } = DateTime.UtcNow;
    public required DateTime Updated_At { get; set; } = DateTime.UtcNow;
}
