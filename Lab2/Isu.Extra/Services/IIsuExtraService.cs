using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Services;

namespace Isu.Extra.Services;

public interface IIsuExtraService
{
    public Flow GetFlow(char facultyLetter, int numberOfFlow);
    public Lesson AddLessonToFlow(string name, string lector, LectureTimes lectureTime, uint audience, DayOfWeek dayOfWeek, Flow flow);
    public GroupExt AddGroup(GroupNameExt groupNameExt);
    public StudentExt AddStudent(string name, string surname, GroupExt groupExt);
    private MegaFaculty CalculateMegaFacultyForGroup()
    {
        throw new NotImplementedException();
    }

    private Flow CalculateFlowForGroup()
    {
        throw new NotImplementedException();
    }
}