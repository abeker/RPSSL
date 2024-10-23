using DomainError = RPSSL.Domain.Common.Errors.Abstractions;

namespace RPSSL.Api.Common.Errors;

public abstract class StatusError(DomainError.IError error, ErrorSource? source = null) : IError
{
    public abstract int Status { get; }

    public string Code { get; } = error.ErrorCode;

    public string Title { get; } = $"{error.GetType().Name}: {error.ErrorDescription}";

    public ErrorSource Source { get; } = source ?? new ErrorSource();
}