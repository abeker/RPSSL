namespace RPSSL.Api.Common.Errors;

public class PointerWrapperError(string pointer, IError innerError) : IError
{
    public int Status { get; } = innerError.Status;

    public string Code { get; } = innerError.Code;

    public string Title { get; } = innerError.Title;

    public ErrorSource Source { get; } = new() { Pointer = $"{pointer}/{innerError.Source.Pointer}" };
}
