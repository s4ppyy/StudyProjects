using Isu.Entities;

namespace Isu.Exceptions;

public class NoSuchStudentException : Exception
{
    public NoSuchStudentException(int id)
    {
        Id = id;
    }

    public int Id { get; }
}