using RPSSL.Domain.Common.Errors.Abstractions;
using RPSSL.Domain.Common.Lists;

namespace RPSSL.Domain.Common.Errors.Extensions;

public static class ErrorExtensions
{
    public static ErrorList ToList(this IError error)
    {
        return new ErrorList(error);
    }
}