using Isu.Extra.Entities;

namespace Isu.Extra.Exceptions;

public class GroupExtOverflowException : Exception
{
    public GroupExtOverflowException(GroupExt groupExt)
    {
        GroupExt = groupExt;
    }

    public GroupExt GroupExt { get; }
}