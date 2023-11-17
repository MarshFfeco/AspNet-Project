namespace ProfessorHelp.Shared.Comunication.Request;

public class RequestSignUpProfessor
{
    public string First_Name { get; set; } = string.Empty;
    public string Last_Name { get; set; } = string.Empty;
    public string Sex { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
