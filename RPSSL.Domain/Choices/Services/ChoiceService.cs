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

    public Maybe<Choice> CalculateWinner(Choice choice1, Choice choice2)
    {
        if (choice1 == choice2)
            return Maybe<Choice>.None;

        return choice1 switch
        {
            Choice.Rock => choice2 is Choice.Scissors or Choice.Lizard ? choice1 : choice2,
            Choice.Paper => choice2 is Choice.Rock or Choice.Spock ? choice1 : choice2,
            Choice.Scissors => choice2 is Choice.Paper or Choice.Lizard ? choice1 : choice2,
            Choice.Lizard => choice2 is Choice.Spock or Choice.Paper ? choice1 : choice2,
            Choice.Spock => choice2 is Choice.Scissors or Choice.Rock ? choice1 : choice2,
            _ => throw new ArgumentOutOfRangeException(nameof(choice1), choice1, null)
        };
    }
}