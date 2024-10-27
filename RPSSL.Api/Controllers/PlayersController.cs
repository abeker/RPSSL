using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RPSSL.Api.Common.Errors;
using RPSSL.Api.Common.Errors.ErrorFactory;
using RPSSL.Api.Contracts.Players;
using RPSSL.Api.Factories.Players;
using RPSSL.Application.Games.PlayGameCommand;

namespace RPSSL.Api.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class PlayersController(ISender mediator, IErrorFactory errorFactory, IErrorResponseFactory errorResponseFactory) : ControllerBase
{
    /// <summary>
    /// Creates a player
    /// </summary>
    [HttpPost]
    [ActionName(nameof(CreateAsync))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PlayGameResponse>> CreateAsync([FromBody] CreatePlayerRequest request) =>
        await mediator
            .Send(CreatePlayerCommandFactory.Create(request))
            .MapError(errorFactory.From)
            .Match(onSuccess: Ok, onFailure: errorResponseFactory.From);
}