using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Lists;

namespace RPSSL.Domain.Choices.Services;

public interface IChoiceService
{
    public Result<Choice, ErrorList> GetByRandomNumber(PositiveNumber choiceNumber);
}