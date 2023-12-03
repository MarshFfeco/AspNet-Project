using ProfessorHelp.Interfaces;
using ProfessorHelp.Repository.Matter;
using ProfessorHelp.Repository.Professor;
using ProfessorHelp.Services.Criptografia;
using ProfessorHelp.Services.ProfessorLogin;
using ProfessorHelp.Services.Token;
using ProfessorHelp.UseCase.Dashboard;
using ProfessorHelp.UseCase.Dashboard.Interfaces;
using ProfessorHelp.UseCase.Matter;
using ProfessorHelp.UseCase.Matter.Interfaces;
using ProfessorHelp.UseCase.Professor;
using ProfessorHelp.UseCase.Professor.Interfaces;
using ProfessorHelp.Validator.Professor;
using ProfessorHelp.Validator.Professor.Interfaces;

namespace ProfessorHelp.Services.Bootstrapper;

public static class Bootstrapper
{
    public static void AddAplication(this IServiceCollection service, IConfiguration configuration)
    {
        AddEncryptPassword(service, configuration);
        AddHashId(service, configuration);
        AddTokenJWT(service, configuration);

        AddRepositories(service);

        AddUseCase(service);

        AddService(service);
    }

    public static void AddUseCase(this IServiceCollection service)
    {
        service.AddScoped<ISignUpProfessorUseCase, SignUpProfessorUseCase>()
            .AddScoped<ILoginProfessorUseCase, LoginProfessorUseCase>()
            .AddScoped<IUpdatePasswordProfessorUseCase, UpdatePasswordProfessorUseCase>()
            .AddScoped<ICreateMatterUseCase, CreateMatterUseCase>()
            .AddScoped<IDashboardUseCase, DashboardUseCase>();
    }

    public static void AddRepositories(IServiceCollection service)
    {
        service.AddScoped<IProfessorWriteOnlyRepository, ProfessorRepositoy>()
            .AddScoped<IProfessorReadOnlyRepository, ProfessorRepositoy>()
            .AddScoped<IMatterWriteOnlyRepository, MatterRepository>()
            .AddScoped<IMatterReadOnlyRepository, MatterRepository>();
    }

    public static void AddEncryptPassword(IServiceCollection service, IConfiguration configuration)
    {
        var keyInitial = configuration.GetRequiredSection("Configs:password:KeyInitial");
        var keyFinal = configuration.GetRequiredSection("Configs:password:KeyFinal");

        if (keyInitial is null || keyFinal is null)
        {
            throw new ArgumentException("Config Keys is empty! Encript");
        }

        service.AddScoped(option => new EncryptPassword(keyInitial.Value!, keyFinal.Value!));
    }

    public static void AddTokenJWT(IServiceCollection service, IConfiguration configuration)
    {
        var lifeToken = configuration.GetRequiredSection("Configs:jwt:LifeTokenMinute");
        var securityKey = configuration.GetRequiredSection("Configs:jwt:SecurityKey");

        if (lifeToken is null || securityKey is null)
        {
            throw new ArgumentException("Config Keys is empty! JWT");
        }

        service.AddScoped(option => new TokenController(int.Parse(lifeToken.Value!), securityKey.Value!));
    }

    private static void AddService(IServiceCollection service)
    {
        service.AddScoped<IProfessorLogin, ProfessorLogin.ProfessorLogin>();
    }

    private static void AddHashId(IServiceCollection service, IConfiguration configuration)
    {
        var salt = configuration.GetRequiredSection("Configs:hash:salt");

        service.AddHashids(setup =>
        {
            setup.Salt = salt.Value;
            setup.MinHashLength = 3;
        });
    }
}
