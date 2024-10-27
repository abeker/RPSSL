using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Errors;
using RPSSL.Domain.Common.Errors.Extensions;
using RPSSL.Domain.Common.Lists;

namespace RPSSL.Application.Common.Extensions;

public static class EnumExtensions
{
    public static Result<TEnum, ErrorList> TryConvertToEnum<TEnum>(this int value) where TEnum : struct, Enum
    {
        if (!Enum.IsDefined(typeof(TEnum), value)) 
            return Result.Failure<TEnum, ErrorList>(new EnumOutOfRangeError(nameof(TEnum), value.ToString()).ToList());
        
        return (TEnum)(object)value;
    }

}