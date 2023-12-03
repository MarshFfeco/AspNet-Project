namespace ProfessorHelp.Shared.Exception.ExceptionBase;

public class ValidationErroException : ProfessorHelpException
{
    public List<string> MsgsError { get; set; }

    public ValidationErroException(List<string> msgsError) : base(string.Empty)
    {
        MsgsError = msgsError;
    }
}
