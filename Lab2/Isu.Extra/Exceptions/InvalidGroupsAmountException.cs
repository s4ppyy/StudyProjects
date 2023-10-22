namespace Isu.Extra.Exceptions;

public class InvalidGroupsAmountException : Exception
{
    public InvalidGroupsAmountException(int numberOfGroups)
    {
        NumberOfGroups = numberOfGroups;
    }

    public int NumberOfGroups { get; }
}