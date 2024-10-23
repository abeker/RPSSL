using DomainError = RPSSL.Domain.Common.Errors.Abstractions;

namespace RPSSL.Api.Common.Errors._5xx;

public class Status500Error(DomainError.IError error) : StatusError(error)
{
    public override int Status => 500;
}