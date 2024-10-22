namespace RPSSL.Domain.Common.Errors;

public class EmptyGuidError(string fieldName) : IError
{
    public string ErrorCode { get; init; } = ErrorCodes.EmptyGuidErrorCode;

    public string ErrorDescription { get; init; } = $"{fieldName} must be a non-empty guid";
}