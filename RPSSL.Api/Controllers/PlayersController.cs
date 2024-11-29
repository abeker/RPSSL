using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RPSSL.Api.Common.Errors;
using RPSSL.Api.Common.Errors.ErrorFactory;
using RPSSL.Api.Contracts.Common;
using RPSSL.Api.Contracts.Players;
using RPSSL.Api.Factories.Players;
using RPSSL.Application.Players.GetPlayerByName;
using RPSSL.Application.Players.GetScoreboard;

namespace RPSSL.Api.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class PlayersController(ISender mediator, IErrorFactory errorFactory, IErrorResponseFactory errorResponseFactory) : ControllerBase
{
    /// <summary>
    /// Creates a player.
    /// </summary>
    /// <param name="request">The details of the player to be created.</param>
    /// <response code="200">Player successfully created.</response>
    /// <response code="400">Invalid input data for the player.</response>
    [HttpPost]
    [ActionName(nameof(CreateAsync))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateAsync([FromBody] CreatePlayerRequest request) =>
        await mediator
            .Send(CreatePlayerCommandFactory.Create(request))
            .MapError(errorFactory.From)
            .Match(onSuccess: Ok, onFailure: errorResponseFactory.From);
    
    /// <summary>
    /// Returns the scoreboard.
    /// </summary>
    /// <remarks>
    /// This endpoint retrieves the scoreboard, which contains the ranking of players based on their performance.
    /// </remarks>
    /// <param name="request">The pagination parameters for the scoreboard.</param>
    /// <response code="200">Scoreboard successfully retrieved.</response>
    /// <response code="400">Invalid request parameters or a client error.</response>
    [HttpGet("scoreboard")]
    [ActionName(nameof(GetScoreboardAsync))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ScoreboardResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ScoreboardResponse>> GetScoreboardAsync([FromQuery] PageRequest request) =>
        await mediator
            .Send(new GetScoreboardQuery(request.Index, request.Size))
            .MapError(errorFactory.From)
            .Match(onSuccess: Ok, onFailure: errorResponseFactory.From);
    
    /// <summary>
    /// Returns player details by name.
    /// </summary>
    /// <param name="name">The name of the player to retrieve.</param>
    /// <response code="200">Player details successfully retrieved.</response>
    /// <response code="400">Invalid player name provided.</response>
    /// <response code="404">Player not found.</response>
    [HttpGet("name/{name}")]
    [ActionName(nameof(GetScoreboardAsync))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ScoreboardResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ScoreboardResponse>> GetByNameAsync(string name) =>
        await mediator
            .Send(new GetPlayerByNameQuery(name))
            .MapError(errorFactory.From)
            .Match(onSuccess: Ok, onFailure: errorResponseFactory.From);
}