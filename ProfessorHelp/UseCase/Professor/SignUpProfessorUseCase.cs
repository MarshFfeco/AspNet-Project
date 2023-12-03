using AutoMapper;
using ProfessorHelp.Interfaces;
using ProfessorHelp.Services.Criptografia;
using ProfessorHelp.Services.Token;
using ProfessorHelp.Shared.Comunication.Request.Professor;
using ProfessorHelp.Shared.Comunication.Response.Professor;
using ProfessorHelp.Shared.Exception;
using ProfessorHelp.Shared.Exception.ExceptionBase;
using ProfessorHelp.UseCase.Professor.validation;
using ProfessorHelp.Validator.Professor.Interfaces;

namespace ProfessorHelp.Validator.Professor;

public class SignUpProfessorUseCase : ISignUpProfessorUseCase
{
    private readonly IProfessorReadOnlyRepository _professorReadOnlyRepo;
    private readonly IProfessorWriteOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly EncryptPassword _encriptPassword;
    private readonly TokenController _tokenController;

    public SignUpProfessorUseCase(IProfessorWriteOnlyRepository repository, IMapper mapper, EncryptPassword encriptPassword, TokenController tokenController, IProfessorReadOnlyRepository professorReadOnlyRepo)
    {
        _repository = repository;
        _mapper = mapper;
        _encriptPassword = encriptPassword;
        _tokenController = tokenController;
        _professorReadOnlyRepo = professorReadOnlyRepo;
    }

    public async Task<ResponseLoginProfessor> Execute(RequestSignUpProfessor request)
    {
        await Validator(request);

        var entity = _mapper.Map<Models.Entity.Professor>(request);
        entity.Password = _encriptPassword.Encrypt(request.Password);

        await _repository.CreateProfessor(entity);

        string token = _tokenController.GenerateToken(entity.Email);

        return new ResponseLoginProfessor
        {
            Token = token,
        };
    }

    private async Task Validator(RequestSignUpProfessor request)
    {
        var validator = new SignUpProfessorValidator();
        var result = validator.Validate(request);

        bool isExistProfessor = await _professorReadOnlyRepo.isExistProfessorWithEmail(request.Email);

        if(isExistProfessor)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceErrorMessage.EMAIL_ALREADY_EXIST));
        }

        if(!result.IsValid) 
        {
            var messageError = result.Errors.Select(err => err.ErrorMessage).ToList();
            throw new ValidationErroException(messageError);
        }
    }
}
