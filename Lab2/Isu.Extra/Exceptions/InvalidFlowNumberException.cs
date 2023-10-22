namespace Isu.Extra.Exceptions;

public class InvalidFlowNumberException : Exception
{
    public InvalidFlowNumberException(int number)
    {
        Number = number;
    }

    public int Number { get; }
}