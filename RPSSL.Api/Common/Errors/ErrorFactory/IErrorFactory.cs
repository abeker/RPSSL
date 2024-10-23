using DomainError = RPSSL.Domain.Common.Errors.Abstractions;

namespace RPSSL.Api.Common.Errors.ErrorFactory;

public interface IErrorFactory
{
    ApiErrorList From(IEnumerable<DomainError.IError> errors);
}