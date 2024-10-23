namespace RPSSL.Domain.Common.Errors.Abstractions;

public interface IError
{
    /// <summary>
    /// Contains application specific error code
    /// </summary>
    string ErrorCode { get; }

    /// <summary>
    /// Can contain cause of error or steps on how to resolve it
    /// </summary>
    string ErrorDescription { get; }
}