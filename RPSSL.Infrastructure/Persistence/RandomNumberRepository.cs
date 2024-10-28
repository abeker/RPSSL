using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Polly;
using RPSSL.Application.Choices.Persistence;
using RPSSL.Domain.Common.Lists;
using RPSSL.Infrastructure.ApiClients.CodeChallenge;

namespace RPSSL.Infrastructure.Persistence;

public class RandomNumberRepository(ICodeChallengeApiClient codeChallengeApiClient, ILogger<RandomNumberRepository> logger) : IRandomNumberRepository
{
    public async Task<Result<int, ErrorList>> GenerateAsync(CancellationToken cancellationToken)
    {
        var retryPolicy = Policy
            .Handle<HttpRequestException>()
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                onRetry: (exception, timespan, attempt, _) =>
                {
                    logger.LogWarning($"Retry {attempt} encountered an error: {exception.Message}. Waiting {timespan} before next retry.");
                });

        var response = await retryPolicy.ExecuteAsync(async () => await codeChallengeApiClient.GetRandomNumberAsync());

        return response.RandomNumber;
    }
}