using RPSSL.Application.Common.Queries;

namespace RPSSL.Application.Choices.GetChoices;

public record GetChoicesQuery : IQuery<IEnumerable<ChoiceResponse>>;