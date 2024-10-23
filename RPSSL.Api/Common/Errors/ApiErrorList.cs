using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Lists;

namespace RPSSL.Api.Common.Errors;

public class ApiErrorList : CombinableList<IError>
{
    public ApiErrorList() : base()
    {
    }

    public ApiErrorList(IError error) : base(error)
    {
    }

    public ApiErrorList(IEnumerable<IError> errors) : base(errors)
    {
    }

    public ApiErrorList WrapPointers(string pointer)
    {
        var list = new ApiErrorList();
        var enumerator = GetEnumerator();
        while (enumerator.MoveNext())
        {
            list.Add(new PointerWrapperError(pointer, enumerator.Current));
        }
        return list;
    }

    protected override ICombine CreateCombinedList(IEnumerable<IError> items)
        => new ApiErrorList(items);
}