using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Errors;
using RPSSL.Domain.Common.Errors.Extensions;
using RPSSL.Domain.Common.Lists;

namespace RPSSL.Domain.Common.Models;

public class Page : ValueObject
{
    public int Index { get; }
    public int Size { get; }

    private Page(int index, int size)
    {
        Index = index;
        Size = size;
    }
    
    public static Result<Page, ErrorList> Create(int index, int size)
    {
        return index < 0
            ? new PageOutOfRangeError(nameof(Index), index.ToString()).ToList()
            : size < 0
                ? new PageOutOfRangeError(nameof(Size), size.ToString()).ToList()
                : Result.Success<Page, ErrorList>(new Page(index, size));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Index + Size;
    }
}