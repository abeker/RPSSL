using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Errors;
using RPSSL.Domain.Common.Lists;

namespace RPSSL.Application.Common.Extensions;

public static class EnumExtensions
{
    public static Result<TEnum, ErrorList> TryConvertToEnum<TEnum>(this int value) where TEnum : struct, Enum
    {
        if (!Enum.IsDefined(typeof(TEnum), value)) 
            return Result.Failure<TEnum, ErrorList>(new ErrorList(new EnumOutOfRangeError(nameof(TEnum), value.ToString())));
        
        return (TEnum)(object)value;
    }

}