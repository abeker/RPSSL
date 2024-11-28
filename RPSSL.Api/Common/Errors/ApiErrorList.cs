using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Collections;

namespace RPSSL.Api.Common.Errors;

public class ApiErrorList : CombinableList<IError>
{
    public ApiErrorList()
    {
    }

    public ApiErrorList(IEnumerable<IError> errors) : base(errors)
    {
    }

    protected override ICombine CreateCombinedList(IEnumerable<IError> items)
        => new ApiErrorList(items);
}