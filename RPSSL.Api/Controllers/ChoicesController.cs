using MediatR;
using Microsoft.AspNetCore.Mvc;
using RPSSL.Application.Choices.GetChoices;

namespace RPSSL.Api.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class ChoicesController(ISender mediator) : ControllerBase
{
    /// <summary>
    /// Returns all available choices
    /// </summary>
    [HttpGet]
    [ActionName(nameof(GetAsync))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ChoiceResponse>))]
    public async Task<ActionResult<IEnumerable<ChoiceResponse>>> GetAsync()
    {
        var choices = await mediator.Send(new GetChoicesQuery());
        return Ok(choices);
    }
}