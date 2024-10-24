using CSharpFunctionalExtensions;
using RPSSL.Application.Choices.Persistence;
using RPSSL.Domain.Common.Lists;
using RPSSL.Infrastructure.ApiClients.CodeChallenge;

namespace RPSSL.Infrastructure.Persistence;

public class RandomNumberRepository(ICodeChallengeApiClient codeChallengeApiClient) : IRandomNumberRepository
{
    public async Task<Result<int, ErrorList>> GenerateAsync(CancellationToken cancellationToken)
    {
        // todo: add retry policy and error handling
        var response = await codeChallengeApiClient.GetRandomNumberAsync();

        return response.RandomNumber;
    }
}