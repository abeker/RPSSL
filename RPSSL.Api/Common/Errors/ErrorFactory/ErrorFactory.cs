using RPSSL.Api.Common.Errors._4xx;
using RPSSL.Api.Common.Errors._5xx;
using RPSSL.Domain.Common.Errors;
using DomainError = RPSSL.Domain.Common.Errors.Abstractions;

namespace RPSSL.Api.Common.Errors.ErrorFactory;

public class ErrorFactory(ILogger<ErrorFactory> logger) : IErrorFactory
{
    public ApiErrorList From(IEnumerable<DomainError.IError> errors)
    {
        return errors
            .Select(MapCrossCuttingErrorToApiError)
            .Aggregate(new ApiErrorList(), (combinedResult, result) =>
            {
                combinedResult.Add(result);
                return combinedResult;
            });
    }
    
    private IError MapCrossCuttingErrorToApiError(DomainError.IError error)
    {
        IError apiError = error switch
        {
            EmptyGuidError emptyGuidError => new Status400Error(emptyGuidError),
            EmptyStringError emptyStringError => new Status400Error(emptyStringError),
            InvalidChoiceIdError invalidChoiceIdError => new Status400Error(invalidChoiceIdError),
            _ => new Status500Error(error),
        };

        LogError(error, apiError);
        
        return apiError;
    }
    
    private void LogError(DomainError.IError error, IError apiError)
    {
        if (IsRequestError(apiError.Status))
        {
            logger.LogError(
                "Error in the client request occurred. Type: {@Name}, Code: {@ErrorErrorCode}, Description: {@ErrorErrorDescription}",
                error.GetType().Name,
                error.ErrorCode,
                error.ErrorDescription);
        }
        else if (IsServerError(apiError.Status))
        {
            logger.LogError("Server error occurred. Type: {@Name}, Code: {@ErrorErrorCode}, Description: {@ErrorErrorDescription}",
                error.GetType().Name,
                error.ErrorCode,
                error.ErrorDescription);
        }
    }

    private static bool IsRequestError(int statusCode) => statusCode is >= 400 and < 500;

    private static bool IsServerError(int statusCode) => statusCode >= 500;
}