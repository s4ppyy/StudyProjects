namespace Isu.Exceptions;

public class IdIsAlreadyUsedException : Exception
{
    public IdIsAlreadyUsedException(int id)
    {
        Id = id;
    }

    public int Id { get; }
}