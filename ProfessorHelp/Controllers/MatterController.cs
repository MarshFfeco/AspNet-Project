using Microsoft.AspNetCore.Mvc;
using ProfessorHelp.Filter;
using ProfessorHelp.Shared.Comunication.Request.Matter;
using ProfessorHelp.Shared.Comunication.Response.Matter;
using ProfessorHelp.UseCase.Matter.Interfaces;

namespace ProfessorHelp.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[ServiceFilter(typeof(ProfessorAuth))]
public class MatterController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseMatterCreate), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromServices] ICreateMatterUseCase usecase, [FromBody] RequestMatterCreate req)
    {
        var response = await usecase.Execute(req);

        return Created(string.Empty, response);
    }
}
