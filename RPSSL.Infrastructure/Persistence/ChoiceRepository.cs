using CSharpFunctionalExtensions;
using RPSSL.Domain.Choices;
using RPSSL.Domain.Choices.Persistence;
using RPSSL.Domain.Common.Lists;

namespace RPSSL.Infrastructure.Persistence;

public class ChoiceRepository : IChoiceRepository
{
    public Task<Result<IEnumerable<Choice>, ErrorList>> GetAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Choice, ErrorList>> GetByCodeAsync(ChoiceCode choiceCode, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}