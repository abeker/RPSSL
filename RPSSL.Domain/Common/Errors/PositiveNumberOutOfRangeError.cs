using RPSSL.Domain.Common.Errors.Abstractions;
using RPSSL.Domain.Common.Errors.Codes;

namespace RPSSL.Domain.Common.Errors;

public class PositiveNumberOutOfRangeError(int choiceNumber) : IError
{
    public string ErrorCode { get; } = ErrorCodes.InvalidChoiceIdErrorCode;
    public string ErrorDescription { get; } = $"{choiceNumber} is not a positive number.";
}