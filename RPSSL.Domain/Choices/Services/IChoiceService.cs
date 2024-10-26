using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Lists;

namespace RPSSL.Domain.Choices.Services;

public interface IChoiceService
{
    Result<Choice, ErrorList> GetByRandomNumber(PositiveNumber choiceNumber);
    Maybe<Choice> CalculateWinner(Choice choice1, Choice choice2);
}