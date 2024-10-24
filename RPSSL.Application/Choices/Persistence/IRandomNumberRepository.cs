using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Lists;

namespace RPSSL.Application.Choices.Persistence;

public interface IRandomNumberRepository
{
    Task<Result<int, ErrorList>> GenerateAsync(CancellationToken cancellationToken);
}