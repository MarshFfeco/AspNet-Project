using Microsoft.Extensions.DependencyInjection;
using ProfessorHelp.Interfaces;
using ProfessorHelp.Repository;
using ProfessorHelp.Services.Criptografia;
using ProfessorHelp.Services.Token;
using ProfessorHelp.Validator.Professor;
using ProfessorHelp.Validator.Professor.Interfaces;

namespace ProfessorHelp.Services.Bootstrapper;

public static class Bootstrapper
{
    public static void AddAplication(this IServiceCollection service, IConfiguration configuration)
    {
        AddEncryptPassword(service, configuration);
        AddTokenJWT(service, configuration);

        AddRepositories(service);

        AddUseCase(service);

        service.AddScoped<IMatterRepository, MatterRepository>();
    }

    public static void AddUseCase(this IServiceCollection service)
    {
        service.AddScoped<ISignUpProfessorUseCase, SignUpProfessorUseCase>()
            .AddScoped<ILoginProfessorUseCase, LoginProfessorUseCase>();
    }

    public static void AddRepositories(this IServiceCollection service)
    {
        service.AddScoped<IProfessorWriteOnlyRepository, ProfessorRepositoy>()
            .AddScoped<IProfessorReadOnlyRepository, ProfessorRepositoy>();
    }

    public static void AddEncryptPassword(this IServiceCollection service, IConfiguration configuration)
    {
        var keyInitial = configuration.GetRequiredSection("Configs:KeyInitial");
        var keyFinal = configuration.GetRequiredSection("Configs:KeyFinal");

        if (keyInitial is null || keyFinal is null)
        {
            throw new ArgumentException("Config Keys is empty! Encript");
        }

        service.AddScoped(option => new EncryptPassword(keyInitial.Value!, keyFinal.Value!));
    }

    public static void AddTokenJWT(this IServiceCollection service, IConfiguration configuration)
    {
        var lifeToken = configuration.GetRequiredSection("Configs:LifeToken");
        var securityKey = configuration.GetRequiredSection("Configs:SecurityKey");

        if (lifeToken is null || securityKey is null)
        {
            throw new ArgumentException("Config Keys is empty! JWT");
        }

        service.AddScoped(option => new TokenController(int.Parse(lifeToken.Value!), securityKey.Value!));
    }
}
