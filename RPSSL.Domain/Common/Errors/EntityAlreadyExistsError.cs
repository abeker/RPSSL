using RPSSL.Domain.Common.Errors.Abstractions;
using RPSSL.Domain.Common.Errors.Codes;

namespace RPSSL.Domain.Common.Errors;

public class EntityAlreadyExistsError(string entityName) : IError
{
    public string ErrorCode { get; init; } = ErrorCodes.EntityAlreadyExistErrorCode;

    public string ErrorDescription { get; init; } = $"{entityName} already exists";
}