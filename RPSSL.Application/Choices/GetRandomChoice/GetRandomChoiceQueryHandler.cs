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
        return await randomNumberRepository.GenerateAsync(cancellationToken)
            .Bind(PositiveNumber.Create)
            .Bind(choiceService.GetByRandomNumber)
            .Map(choice => new RandomChoiceResponse((int)choice, choice.ToString()));
    }
}