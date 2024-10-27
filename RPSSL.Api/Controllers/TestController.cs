using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RPSSL.Api.Common.Errors;
using RPSSL.Api.Common.Errors.ErrorFactory;
using RPSSL.Api.Contracts.Games;
using RPSSL.Api.Factories.Games;
using RPSSL.Application.Choices.GetChoices;
using RPSSL.Application.Choices.GetRandomChoice;
using RPSSL.Application.Games.PlayGameCommand;

namespace RPSSL.Api.Controllers;

/// <summary>
/// TestController is designed to facilitate testing of routes as required by the specifications.
/// It operates without versioning and API prefix, allowing for straightforward route access during testing.
/// </summary>
[ApiController]
public class TestController(ISender mediator, IErrorFactory errorFactory, IErrorResponseFactory errorResponseFactory) : ControllerBase
{
    /// <summary>
    /// Returns all available choices
    /// </summary>
    [HttpGet, Route("/choices")]
    [ActionName(nameof(GetChoicesAsync))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ChoiceResponse>))]
    public async Task<ActionResult<IEnumerable<ChoiceResponse>>> GetChoicesAsync()
    {
        var choices = await mediator.Send(new GetChoicesQuery());
        return Ok(choices);
    }
    
    /// <summary>
    /// Returns a random choice
    /// </summary>
    [HttpGet, Route("/choice")]
    [ActionName(nameof(GetRandomChoiceAsync))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ChoiceResponse>))]
    public async Task<ActionResult<IEnumerable<ChoiceResponse>>> GetRandomChoiceAsync() =>
        await mediator
            .Send(new GetRandomChoiceQuery())
            .MapError(errorFactory.From)
            .Match(onSuccess: Ok, onFailure: errorResponseFactory.From);
    
    /// <summary>
    /// Play a round with a computer
    /// </summary>
    [HttpPost, Route("/play")]
    [ActionName(nameof(PlayAsync))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlayGameResponse))]
    public async Task<ActionResult<PlayGameResponse>> PlayAsync([FromBody] PlayGameTestRequest testRequest) =>
        await mediator
            .Send(PlayGameCommandFactory.Create(testRequest))
            .MapError(errorFactory.From)
            .Match(onSuccess: Ok, onFailure: errorResponseFactory.From);
}