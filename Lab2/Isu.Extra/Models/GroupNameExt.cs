using Isu.Exceptions;
using Isu.Extra.Exceptions;
using Isu.Extra.Services;
using Isu.Models;

namespace Isu.Extra.Models;

public class GroupNameExt
{
    private static int _maxGroupsAmount = 15;
    public GroupNameExt(GroupName name)
    {
        if (name == null)
            throw new ArgumentNullException();
        if (name.GroupNumber > _maxGroupsAmount)
            throw new InvalidGroupsAmountException(name.GroupNumber);
        Faculty = new Faculty(char.Parse(name.FullName[..1]));
        CourseNumber = name.CourseNumber;
        GroupNumber = name.GroupNumber;
        Specialisation = name.Specialisation;
        FullName = name.FullName;
        GroupName = name;
    }

    public Faculty Faculty { get; }
    public CourseNumber CourseNumber { get; }
    public int GroupNumber { get; }
    public int? Specialisation { get; }
    public string FullName { get; }

    public GroupName GroupName { get; }
}