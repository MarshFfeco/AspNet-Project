using ProfessorHelp.Shared.Comunication.Response.GenerateStudent;

namespace ProfessorHelp.Shared.Comunication.Response.Matter;

public class ResponseMatterCreate
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Shift { get; set; }
    public string? Module { get; set; }
    public string? Classroom { get; set; }
    public List<ResponseGenerateStudentCreate>? GenerateStudents { get; set; }
}
