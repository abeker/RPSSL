using System.ComponentModel.DataAnnotations;
using RPSSL.Api.Common.Errors;

namespace RPSSL.Api.Contracts.Error;

public record ErrorResponse([property: Required] IEnumerable<IError> Errors);