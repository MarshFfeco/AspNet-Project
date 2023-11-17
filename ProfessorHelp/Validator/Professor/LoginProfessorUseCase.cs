using Microsoft.AspNetCore.Http.HttpResults;
using ProfessorHelp.Interfaces;
using ProfessorHelp.Services.Criptografia;
using ProfessorHelp.Services.Token;
using ProfessorHelp.Shared.Comunication.Request;
using ProfessorHelp.Shared.Comunication.Response;
using ProfessorHelp.Validator.Professor.Interfaces;
using System.Net;

namespace ProfessorHelp.Validator.Professor;

public class LoginProfessorUseCase : ILoginProfessorUseCase
{
    private readonly IProfessorReadOnlyRepository _professorReadOnlyRepo;
    private readonly EncryptPassword _encriptPassword;
    private readonly TokenController _tokenController;

    public LoginProfessorUseCase(IProfessorReadOnlyRepository professorReadOnlyRepo, EncryptPassword encriptPassword, TokenController tokenController)
    {
        _professorReadOnlyRepo = professorReadOnlyRepo;
        _encriptPassword = encriptPassword;
        _tokenController = tokenController;
    }

    public async Task<ResponseProfessorLogin> Execute(RequestProfessorLogin request)
    {
        var criptPassword = _encriptPassword.Encrypt(request.Password);

        var entity = await _professorReadOnlyRepo.Login(request.Email, criptPassword);

        if(entity is null) 
        {
            throw new Exception("code: 401");
        }

        return new ResponseProfessorLogin
        {
            Name = entity.First_Name,
            Token = _tokenController.GenerateToken(entity.Email),
        };
    }
}
