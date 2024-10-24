namespace RPSSL.Domain.Choices.Services;

public class ChoiceService : IChoiceService
{
    public Choice GetByRandomNumber(PositiveNumber positiveNumber)
    {
        var enumLength = Enum.GetValues(typeof(Choice)).Length;
        var choiceIndex = (positiveNumber.Value - 1) % enumLength + 1;

        return (Choice)choiceIndex; 
    }
}