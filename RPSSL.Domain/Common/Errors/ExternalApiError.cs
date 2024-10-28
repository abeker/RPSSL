using RPSSL.Domain.Common.Errors.Abstractions;
using RPSSL.Domain.Common.Errors.Codes;

namespace RPSSL.Domain.Common.Errors;

public class ExternalApiError(string externalApiName) : IError
{
    public string ErrorCode { get; init; } = ErrorCodes.ExternalApiErrorCode;

    public string ErrorDescription { get; init; } = $"{externalApiName} is not working as expected";
}