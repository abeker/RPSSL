using CSharpFunctionalExtensions;
using MediatR;
using RPSSL.Application.Choices.Persistence;
using RPSSL.Domain.Choices;
using RPSSL.Domain.Choices.Services;
using RPSSL.Domain.Common.Lists;
using Serilog;

namespace RPSSL.Application.Choices.GetRandomChoice;

public class GetRandomChoiceQueryHandler(IRandomNumberRepository randomNumberRepository, IChoiceService choiceService, ILogger logger) 
    : IRequestHandler<GetRandomChoiceQuery, Result<RandomChoiceResponse, ErrorList>>
{
    public async Task<Result<RandomChoiceResponse, ErrorList>> Handle(GetRandomChoiceQuery request, CancellationToken cancellationToken)
    {
        logger.Information("Generating random choice");
        var randomNumberResult = await randomNumberRepository.GenerateAsync(cancellationToken);
        if (randomNumberResult.IsFailure)
            return Result.Failure<RandomChoiceResponse, ErrorList>(randomNumberResult.Error);

        var positiveNumberResult = PositiveNumber.Create(randomNumberResult.Value);
        if (positiveNumberResult.IsFailure)
            return Result.Failure<RandomChoiceResponse, ErrorList>(positiveNumberResult.Error);

        var randomChoice = choiceService.GetByRandomNumber(positiveNumberResult.Value);
        
        return new RandomChoiceResponse((int)randomChoice, randomChoice.ToString());
    }
}