using Microsoft.AspNetCore.Mvc;
using ProfessorHelp.Interfaces;
using ProfessorHelp.Shared.Comunication.Request;
using ProfessorHelp.Shared.Comunication.Response;
using ProfessorHelp.Validator.Professor.Interfaces;

namespace ProfessorHelp.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProfessorController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseSignUpProfessor), StatusCodes.Status201Created)]
    public async Task<IActionResult> SignUpProfessor([FromServices] ISignUpProfessorUseCase useCase, [FromBody] RequestSignUpProfessor request)
    {
        var resultado = await useCase.Execute(request);

        return Created(string.Empty, resultado);
    }
}
