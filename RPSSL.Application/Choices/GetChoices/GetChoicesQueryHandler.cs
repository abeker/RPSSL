using MediatR;
using Microsoft.Extensions.Logging;
using RPSSL.Domain.Choices;

namespace RPSSL.Application.Choices.GetChoices;

public class GetChoicesQueryHandler(ILogger<GetChoicesQueryHandler> logger) : IRequestHandler<GetChoicesQuery, IEnumerable<ChoiceResponse>>
{
    public Task<IEnumerable<ChoiceResponse>> Handle(GetChoicesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching choices");
        return Task.FromResult(Enum.GetValues(typeof(Choice))
            .Cast<Choice>()
            .Select(choice => new ChoiceResponse((int)choice, choice.ToString())));
    }
}