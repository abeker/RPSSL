﻿using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RPSSL.Api.Common.Errors;
using RPSSL.Api.Common.Errors.ErrorFactory;
using RPSSL.Application.Choices.GetChoices;
using RPSSL.Application.Choices.GetRandomChoice;

namespace RPSSL.Api.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class ChoicesController(ISender mediator, IErrorFactory errorFactory, IErrorResponseFactory errorResponseFactory) : ControllerBase
{
    /// <summary>
    /// Returns all available choices
    /// </summary>
    [HttpGet]
    [ActionName(nameof(GetAsync))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ChoiceResponse>))]
    public async Task<ActionResult<IEnumerable<ChoiceResponse>>> GetAsync() =>
        Ok(await mediator.Send(new GetChoicesQuery()));
    
    /// <summary>
    /// Returns random choice
    /// </summary>
    [HttpGet, Route("/random")]
    [ActionName(nameof(GetRandomAsync))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ChoiceResponse>))]
    public async Task<ActionResult<IEnumerable<ChoiceResponse>>> GetRandomAsync() =>
        await mediator
            .Send(new GetRandomChoiceQuery())
            .MapError(errorFactory.From)
            .Match(onSuccess: Ok, onFailure: errorResponseFactory.From);
}