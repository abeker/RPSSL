using RPSSL.Domain.Common.Errors.Abstractions;
using RPSSL.Domain.Common.Errors.Codes;

namespace RPSSL.Domain.Common.Errors;

public class EnumOutOfRangeError(string enumName, string value) : IError
{
    public string ErrorCode { get; init; } = ErrorCodes.EnumOutOfRangeErrorCode;

    public string ErrorDescription { get; init; } = $"{enumName} is out of range with a value {value}";
}