using Refit;

namespace RPSSL.Infrastructure.ApiClients.CodeChallenge;

public interface ICodeChallengeApiClient
{
    [Get("/random")]
    Task<RandomNumberResponse> GetRandomNumberAsync();
}