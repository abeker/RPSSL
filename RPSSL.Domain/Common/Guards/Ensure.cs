namespace RPSSL.Domain.Common.Guards;

public static class Ensure
{
    public static void NotEmpty(Guid value, string message, string argumentName)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException(message, argumentName);
        }
    }

    public static void NotNull<T>(T value, string argumentName)
        where T : class
    {
        if (value is null)
        {
            throw  new ArgumentNullException(argumentName);
        }
    }
}