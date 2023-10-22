using Isu.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra.Entities;

public class GroupExt
{
    private const int _maxStudentPerGroup = 30;
    private readonly List<StudentExt> _studentExts;
    public GroupExt(GroupNameExt groupName, Flow flow, MegaFaculty megaFaculty)
    {
        GroupNameExt = groupName ?? throw new ArgumentNullException();
        if (flow == null)
            throw new ArgumentNullException();
        if (megaFaculty == null)
            throw new ArgumentNullException();
        Group = new Group(groupName.GroupName);
        Flow = flow;
        MegaFaculty = megaFaculty;
        _studentExts = new List<StudentExt>(_maxStudentPerGroup);
    }

    public Group Group { get; }
    public GroupNameExt GroupNameExt { get; }

    public Flow Flow { get; }

    public MegaFaculty MegaFaculty { get; }

    public void AddStudentToGroup(StudentExt studentExt)
    {
        if (studentExt == null)
            throw new ArgumentNullException();
        if (_studentExts.Count == _maxStudentPerGroup)
            throw new GroupExtOverflowException(this);
        _studentExts.Add(studentExt);
    }

    public List<StudentExt> ReturnStudentsWithoutOgnp()
    {
        return _studentExts.FindAll(studentToFind => studentToFind.IsSignedToOgnp == false).ToList();
    }
}