using RPSSL.Domain.Common.Errors.Abstractions;
using RPSSL.Domain.Common.Errors.Codes;

namespace RPSSL.Domain.Common.Errors;

public class EntityNotFoundError(string entityName) : IError
{
    public string ErrorCode { get; init; } = ErrorCodes.EntityNotFoundErrorCode;

    public string ErrorDescription { get; init; } = $"{entityName} is not found";
}