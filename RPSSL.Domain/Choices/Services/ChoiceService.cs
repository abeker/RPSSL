using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Errors;
using RPSSL.Domain.Common.Lists;

namespace RPSSL.Domain.Choices.Services;

public class ChoiceService : IChoiceService
{
    public Result<Choice, ErrorList> GetByRandomNumber(PositiveNumber positiveNumber)
    {
        if (positiveNumber is null)
            return Result.Failure<Choice, ErrorList>(new ErrorList(new NullValueError(nameof(PositiveNumber))));
            
        var enumLength = Enum.GetValues(typeof(Choice)).Length;
        var choiceIndex = (positiveNumber.Value - 1) % enumLength + 1;

        return (Choice)choiceIndex; 
    }
}