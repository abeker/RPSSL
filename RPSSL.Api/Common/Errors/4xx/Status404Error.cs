using DomainError = RPSSL.Domain.Common.Errors.Abstractions;

namespace RPSSL.Api.Common.Errors._4xx;

public class Status404Error(DomainError.IError error) : StatusError(error)
{
    public override int Status => 404;
}