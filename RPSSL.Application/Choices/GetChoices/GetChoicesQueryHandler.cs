using CSharpFunctionalExtensions;
using MediatR;
using RPSSL.Domain.Choices.Persistence;
using RPSSL.Domain.Common.Lists;
using Serilog;

namespace RPSSL.Application.Choices.GetChoices;

public class GetChoicesQueryHandler(IChoiceRepository choiceRepository, ILogger logger)
    : IRequestHandler<GetChoicesQuery, Result<IEnumerable<ChoiceResponse>, ErrorList>>
{
    public async Task<Result<IEnumerable<ChoiceResponse>, ErrorList>> Handle(GetChoicesQuery request, CancellationToken cancellationToken)
    {
        var fetchedChoices = await choiceRepository.GetAsync(cancellationToken);

        return fetchedChoices
            .TapError(errosList => logger.Error(errosList.ToString()))
            .Map(choices => choices.Select(c => new ChoiceResponse(c.Code.Value, c.Name.Value)));
    }
}