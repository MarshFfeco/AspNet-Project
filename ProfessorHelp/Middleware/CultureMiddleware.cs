using System.Globalization;

namespace ProfessorHelp.Middleware;

public class CultureMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string[] _languages =
    {
        "pt",
        "en"
    };
    public CultureMiddleware(RequestDelegate next) 
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        CultureInfo culture = new CultureInfo("en");

        if(context.Request.Headers.ContainsKey("accept-Language"))
        {
            var language = context.Request.Headers["accept-Language"];

            if(_languages.Any(c => c.Equals(language)))
            {
                culture = new CultureInfo(language);
            }
        }

        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;

        await _next(context);
    }
}
