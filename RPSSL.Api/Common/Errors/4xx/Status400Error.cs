using DomainError = RPSSL.Domain.Common.Errors.Abstractions;

namespace RPSSL.Api.Common.Errors._4xx;

public class Status400Error(DomainError.IError error) : StatusError(error)
{
    public override int Status => 400;
}