using ProfessorHelp.Models.Enum;

namespace ProfessorHelp.Shared.Comunication.Response.GenerateStudent;

public class ResponseGenerateStudentCreate
{
    public long Id { get; set; }
    public string First_Name { get; set; } = string.Empty;
    public string Last_Name { get; set; } = string.Empty;
    public Sex Sex { get; set; }
}
