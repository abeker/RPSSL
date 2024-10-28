using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Polly;
using RPSSL.Application.Choices.Persistence;
using RPSSL.Domain.Common.Errors;
using RPSSL.Domain.Common.Errors.Extensions;
using RPSSL.Domain.Common.Lists;
using RPSSL.Infrastructure.ApiClients.CodeChallenge;

namespace RPSSL.Infrastructure.Persistence;

public class RandomNumberRepository(ICodeChallengeApiClient codeChallengeApiClient, ILogger<RandomNumberRepository> logger) : IRandomNumberRepository
{
    private const int RetryCount = 3;
    
    public async Task<Result<int, ErrorList>> GenerateAsync(CancellationToken cancellationToken)
    {
        var retryPolicy = Policy
            .Handle<HttpRequestException>()
            .WaitAndRetryAsync(
                retryCount: RetryCount,
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                onRetry: (exception, timespan, attempt, _) =>
                {
                    logger.LogWarning($"Retry {attempt} encountered an error: {exception.Message}. Waiting {timespan} before next retry.");
                });

        var fallbackPolicy = Policy<Result<int, ErrorList>>
            .Handle<Exception>()
            .FallbackAsync(_ =>
            {
                logger.LogError("All random number fetch retries failed.");
                return Task.FromResult(Result.Failure<int, ErrorList>(new ExternalApiError(nameof(ICodeChallengeApiClient)).ToList()));
            });
        
        var response = await fallbackPolicy.WrapAsync(retryPolicy)
            .ExecuteAsync(async () => 
            {
                var randomNumberResponse = await codeChallengeApiClient.GetRandomNumberAsync();
                return Result.Success<int, ErrorList>(randomNumberResponse.RandomNumber);
            });

        return response;
    }
}