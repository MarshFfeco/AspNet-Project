namespace ProfessorHelp.Shared.Comunication.Response;

public class ResponseErro
{
    public List<string> Messages { get; set; }

    public ResponseErro(string messages)
    {
        Messages = new List<string>
        {
            messages
        };
    }
    public ResponseErro(List<string> messages)
    {
        Messages = messages;
    }
}
