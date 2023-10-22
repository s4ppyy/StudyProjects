namespace ClassLibrary1.Exceptions;

public class NoSuchIdentificatorException : Exception
{
    public NoSuchIdentificatorException(int id)
    {
        Id = id;
    }

    public int Id { get; }
}