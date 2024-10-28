using RPSSL.Domain.Common.Errors.Abstractions;
using RPSSL.Domain.Common.Errors.Codes;

namespace RPSSL.Domain.Common.Errors;

public class PageOutOfRangeError(string fieldName, string valueOutOfRange) : IError
{
    public string ErrorCode { get; init; } = ErrorCodes.PageOutOfRangeErrorCode;

    public string ErrorDescription { get; init; } = $"{fieldName} is out of range with value {valueOutOfRange}";
}