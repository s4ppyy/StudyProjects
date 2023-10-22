using Isu.Exceptions;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra.Entities;

public class Flow
{
    private const int _maxGroupsPerFlow = 8;
    private const int _firstFlowNumber = 1;
    private const int _lastFlowNumber = 2;
    private const int _firstCourseNumber = 1;
    private const int _lastCourseNumber = 4;
    private readonly List<GroupExt> _groupExtsOfFlow;
    public Flow(int numberOfFlow, int courseNumber)
    {
        if (numberOfFlow < _firstFlowNumber || numberOfFlow > _lastFlowNumber)
            throw new InvalidFlowNumberException(numberOfFlow);
        if (courseNumber < _firstCourseNumber || courseNumber > _lastCourseNumber)
            throw new InvalidCourseNumberException(courseNumber);
        _groupExtsOfFlow = new List<GroupExt>(_maxGroupsPerFlow);
        NumberOfFlow = numberOfFlow;
        Schedule = new Schedule();
        CourseNumber = courseNumber;
    }

    public int CourseNumber { get; }
    public int NumberOfFlow { get; }

    public Schedule Schedule { get; }

    public void AddGroupExt(GroupExt groupExt)
    {
        if (groupExt == null)
            throw new ArgumentNullException();
        _groupExtsOfFlow.Add(groupExt);
    }
}