using Isu.Models;

namespace Isu.Exceptions;

public class GroupIsAlreadyExistsException : Exception
{
    public GroupIsAlreadyExistsException(GroupName groupName)
    {
        GroupName = groupName;
    }

    public GroupName GroupName { get; }
}