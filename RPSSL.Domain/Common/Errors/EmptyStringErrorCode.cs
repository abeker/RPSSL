namespace RPSSL.Domain.Common.Errors;

public class EmptyStringError(string fieldName) : IError
{
    public string ErrorCode { get; init; } = ErrorCodes.EmptyStringErrorCode;

    public string ErrorDescription { get; init; } = $"{fieldName} must be a non-empty string";
}