using Isu.Entities;
using Isu.Models;

namespace Isu.Exceptions;

public class GroupDoesNotExistsException : Exception
{
    public GroupDoesNotExistsException(Group group)
    {
        Group = group;
    }

    public GroupDoesNotExistsException(GroupName groupName)
    {
        GroupName = groupName;
    }

    public Group? Group { get; }
    public GroupName? GroupName { get; }
}