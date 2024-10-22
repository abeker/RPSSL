namespace RPSSL.Domain.Common.Errors;

public static class ErrorExtensions
{
    public static ErrorList ToList(this IError error)
    {
        return new ErrorList(error);
    }
}