using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RPSSL.Api.Common.Errors;
using RPSSL.Api.Common.Errors.ErrorFactory;
using RPSSL.Api.Contracts.Common;
using RPSSL.Api.Contracts.Games;
using RPSSL.Api.Factories.Games;
using RPSSL.Application.Games.GetScoreboardQuery;
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
    public async Task<ActionResult<PlayGameResponse>> PlayAsync([FromBody] PlayGameRequest request) =>
        await mediator
            .Send(PlayGameCommandFactory.Create(request))
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