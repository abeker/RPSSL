using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RPSSL.Api.Common.Errors;
using RPSSL.Api.Common.Errors.ErrorFactory;
using RPSSL.Api.Contracts.Games;
using RPSSL.Api.Factories.Games;
using RPSSL.Application.Games.PlayGameCommand;

namespace RPSSL.Api.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class GamesController(ISender mediator, IErrorFactory errorFactory, IErrorResponseFactory errorResponseFactory) : ControllerBase
{
    /// <summary>
    /// Play a round with a computer
    /// </summary>
    [HttpPost]
    [ActionName(nameof(PlayAsync))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlayGameResponse))]
    public async Task<ActionResult<PlayGameResponse>> PlayAsync([FromBody] PlayGameRequest testRequest) =>
        await mediator
            .Send(PlayGameCommandFactory.Create(testRequest))
            .MapError(errorFactory.From)
            .Match(onSuccess: Ok, onFailure: errorResponseFactory.From);
}