using RPSSL.Domain.Common.Collections;
using RPSSL.Domain.Common.Errors.Abstractions;

namespace RPSSL.Domain.Common.Errors.Extensions;

public static class ErrorExtensions
{
    public static ErrorList ToList(this IError error)
    {
        return new ErrorList(error);
    }
}