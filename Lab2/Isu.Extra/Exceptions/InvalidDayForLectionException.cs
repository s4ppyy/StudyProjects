namespace Isu.Extra.Exceptions;

public class InvalidDayForLectionException : Exception
{
    public InvalidDayForLectionException(DayOfWeek dayOfWeek)
    {
        DayOfWeek = dayOfWeek;
    }

    public DayOfWeek DayOfWeek { get; }
}