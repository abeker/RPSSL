using MediatR;
using RPSSL.Domain.Choices;
using Serilog;

namespace RPSSL.Application.Choices.GetChoices;

public class GetChoicesQueryHandler(ILogger logger) : IRequestHandler<GetChoicesQuery, IEnumerable<ChoiceResponse>>
{
    public Task<IEnumerable<ChoiceResponse>> Handle(GetChoicesQuery request, CancellationToken cancellationToken)
    {
        logger.Information("Fetching choices");
        
        return Task.FromResult(Enum.GetValues(typeof(Choice))
            .Cast<Choice>()
            .Select(choice => new ChoiceResponse((int)choice, choice.ToString())));
    }
}