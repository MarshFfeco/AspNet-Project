using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfessorHelp.Filter;
using ProfessorHelp.Shared.Comunication.Request.Professor;
using ProfessorHelp.Shared.Comunication.Response.Professor;
using ProfessorHelp.UseCase.Professor.Interfaces;
using ProfessorHelp.Validator.Professor.Interfaces;

namespace ProfessorHelp.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProfessorController : ControllerBase
{
    [HttpPost("SignUp")]
    [ProducesResponseType(typeof(ResponseLoginProfessor), StatusCodes.Status201Created)]
    public async Task<IActionResult> SignUpProfessor([FromServices] ISignUpProfessorUseCase useCase, [FromBody] RequestSignUpProfessor request)
    {
        var resultado = await useCase.Execute(request);

        return Created(string.Empty, resultado);
    }

    [HttpPost("Login")]
    [ProducesResponseType(typeof(ResponseLoginProfessor), StatusCodes.Status200OK)]
    public async Task<IActionResult> LoginProfessor([FromServices] ILoginProfessorUseCase useCase, [FromBody] RequestLoginProfessor request)
    {
        var result = await useCase.Execute(request);

        return Ok(result);
    }

    [HttpPut("Update")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ServiceFilter(typeof(ProfessorAuth))]
    public async Task<IActionResult> UpdatePasswordProfessor([FromServices] IUpdatePasswordProfessorUseCase useCase, [FromBody] RequestUpdatePasswordProfessor request)
    {
        await useCase.Execute(request);

        return NoContent();
    }
}
