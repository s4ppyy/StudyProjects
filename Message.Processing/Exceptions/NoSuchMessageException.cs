namespace ClassLibrary1.Exceptions;

public class NoSuchMessageException : Exception
{
    public NoSuchMessageException(int id)
    {
        MessageId = id;
    }

    public int MessageId { get; }
}