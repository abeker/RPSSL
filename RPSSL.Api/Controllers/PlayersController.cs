using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RPSSL.Api.Common.Errors;
using RPSSL.Api.Common.Errors.ErrorFactory;
using RPSSL.Api.Contracts.Common;
using RPSSL.Api.Contracts.Players;
using RPSSL.Api.Factories.Players;
using RPSSL.Application.Games.PlayGameCommand;
using RPSSL.Application.Players.GetScoreboardQuery;

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
    
    /// <summary>
    /// Returns a scoreboard 
    /// </summary>
    [HttpGet, Route("/scoreboard")]
    [ActionName(nameof(GetScoreboardAsync))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ScoreboardResponse))]
    public async Task<ActionResult<ScoreboardResponse>> GetScoreboardAsync([FromQuery] PageRequest request) =>
        await mediator
            .Send(new GetScoreboardQuery(request.Index, request.Size))
            .MapError(errorFactory.From)
            .Match(onSuccess: Ok, onFailure: errorResponseFactory.From);
}