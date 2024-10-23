using CSharpFunctionalExtensions;
using RPSSL.Application.Common.Queries;
using RPSSL.Domain.Common.Lists;

namespace RPSSL.Application.Choices.GetChoices;

public class GetChoicesQuery : IQuery<Result<IEnumerable<ChoiceResponse>, ErrorList>>
{
}