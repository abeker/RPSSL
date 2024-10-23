using RPSSL.Domain.Common.Errors.Abstractions;
using RPSSL.Domain.Common.Errors.Codes;

namespace RPSSL.Domain.Common.Errors;

public class InvalidChoiceIdError(int invalidChoiceId) : IError
{
    public string ErrorCode { get; } = ErrorCodes.InvalidChoiceIdErrorCode;
    public string ErrorDescription { get; } = $"Choice with id {invalidChoiceId} could not be created.";
}