using RPSSL.Domain.Common.Errors.Abstractions;
using RPSSL.Domain.Common.Errors.Codes;

namespace RPSSL.Domain.Common.Errors;

public class NullValueError(string valueName) : IError
{
    public string ErrorCode { get; init; } = ErrorCodes.NullValueErrorCode;

    public string ErrorDescription { get; init; } = $"{valueName} could not be null";
}