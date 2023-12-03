namespace ProfessorHelp.Shared.Exception.ExceptionBase;

public class LoginInvalidException : ProfessorHelpException
{
    public LoginInvalidException() : base(ResourceErrorMessage.LOGIN_INVALID)
    {
    }
}
