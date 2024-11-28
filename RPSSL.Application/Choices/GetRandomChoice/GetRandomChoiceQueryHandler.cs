using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.Extensions.Logging;
using RPSSL.Application.Choices.Persistence;
using RPSSL.Domain.Choices;
using RPSSL.Domain.Choices.Services;
using RPSSL.Domain.Common.Collections;

namespace RPSSL.Application.Choices.GetRandomChoice;

public class GetRandomChoiceQueryHandler(IRandomNumberRepository randomNumberRepository, IChoiceService choiceService, ILogger<GetRandomChoiceQueryHandler> logger) 
    : IRequestHandler<GetRandomChoiceQuery, Result<RandomChoiceResponse, ErrorList>>
{
    public async Task<Result<RandomChoiceResponse, ErrorList>> Handle(GetRandomChoiceQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching random choice");
        return await randomNumberRepository.GenerateAsync(cancellationToken)
            .Bind(PositiveNumber.Create)
            .Bind(choiceService.GetByRandomNumber)
            .Map(choice => new RandomChoiceResponse((int)choice, choice.ToString()))
            .Tap(choice => logger.LogInformation("Random choice '{Choice}' is generated", choice))
            .TapError(err => logger.LogError(err.ToString()));
    }
}