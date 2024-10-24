namespace RPSSL.Domain.Choices.Services;

public interface IChoiceService
{
    public Choice GetByRandomNumber(PositiveNumber choiceNumber);
}