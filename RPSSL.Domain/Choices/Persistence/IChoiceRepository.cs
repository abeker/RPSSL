using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Lists;

namespace RPSSL.Domain.Choices.Persistence;

public interface IChoiceRepository
{
    Task<Result<IEnumerable<Choice>, ErrorList>> GetAsync(CancellationToken cancellationToken);
    Task<Result<Choice, ErrorList>> GetByCodeAsync(ChoiceCode choiceCode, CancellationToken cancellationToken);
}