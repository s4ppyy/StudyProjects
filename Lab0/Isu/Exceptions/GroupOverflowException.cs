using Isu.Entities;

namespace Isu.Exceptions;

public class GroupOverflowException : Exception
{
    public GroupOverflowException(Group group)
    {
        Group = group;
    }

    public Group Group { get; }
}