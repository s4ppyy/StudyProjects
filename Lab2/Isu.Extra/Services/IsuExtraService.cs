using Isu.Entities;
using Isu.Exceptions;
using Isu.Extra.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Models;
using Isu.Services;
namespace Isu.Extra.Services;

public class IsuExtraService : IsuService, IIsuExtraService
{
    private const int MinCourseNumber = 1;
    private const int MaxCourseNumber = 4;
    private readonly List<GroupExt> _groupsExt;
    private readonly List<StudentExt> _studentsExts;
    private readonly List<MegaFaculty> _megaFaculties;
    private readonly List<OGNPLesson> _ognpLessons;
    private readonly List<Flow> _flows;
    private IsuService _isuService;
    private int idCounter = 1;
    public IsuExtraService()
    {
        _isuService = new IsuService();
        _megaFaculties = new List<MegaFaculty>(4)
        {
            new MegaFaculty(new Faculty('m')), new MegaFaculty(new Faculty('p')),
            new MegaFaculty(new Faculty('n')), new MegaFaculty(new Faculty('z')),
        };
        _groupsExt = new List<GroupExt>();
        _studentsExts = new List<StudentExt>();
        _ognpLessons = new List<OGNPLesson>();
        _flows = new List<Flow>();
    }

    public IReadOnlyList<OGNPLesson> OgnpLessons => _ognpLessons;
    public Flow GetFlow(char facultyLetter, int numberOfFlow)
    {
        if (numberOfFlow < 1 || numberOfFlow > 2)
            throw new InvalidFlowNumberException(numberOfFlow);
        var megaFaculty = _megaFaculties.Single(letter => letter.Faculty.FacultyLetter == facultyLetter);
        if (numberOfFlow == 1)
            return megaFaculty.ReturnHalfFlow(1);
        return megaFaculty.ReturnHalfFlow(2);
    }

    public List<Flow> GetFlows(int courseNumber)
    {
        if (courseNumber < MinCourseNumber && courseNumber > MaxCourseNumber)
            throw new InvalidCourseNumberException(courseNumber);
        List<Flow> flows = new List<Flow>();
        foreach (MegaFaculty megaFaculty in _megaFaculties)
        {
            foreach (var flow in megaFaculty.Flows)
            {
                if (flow.CourseNumber == courseNumber)
                    flows.Add(flow);
            }
        }

        return flows;
    }

    public MegaFaculty GetMegaFacultyFromLetter(char letter)
    {
        return _megaFaculties.Find(faculty => faculty.Faculty.FacultyLetter == letter) ??
               throw new CantFindMegaFacultyException(letter);
    }

    public Lesson AddLessonToFlow(string name, string lector, LectureTimes lectureTime, uint audience, DayOfWeek dayOfWeek, Flow flow)
    {
        if (flow == null)
            throw new ArgumentNullException();
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException();
        if (string.IsNullOrWhiteSpace(lector))
            throw new ArgumentNullException();
        if (audience == 0)
            throw new ArgumentNullException();
        return flow.Schedule.AddLesson(name, lector, lectureTime, audience, dayOfWeek);
    }

    public GroupExt AddGroup(GroupNameExt groupNameExt)
    {
        if (groupNameExt == null)
            throw new ArgumentNullException();
        GroupExt newGroup = new GroupExt(groupNameExt, CalculateFlowForGroup(groupNameExt), CalculateMegaFacultyForGroup(groupNameExt));
        _isuService.AddGroup(groupNameExt.GroupName);
        _groupsExt.Add(newGroup);
        CalculateFlowForGroup(groupNameExt).AddGroupExt(newGroup);
        return newGroup;
    }

    public StudentExt AddStudent(string name, string surname, GroupExt groupExt)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException();
        if (string.IsNullOrWhiteSpace(surname))
            throw new ArgumentNullException();
        if (groupExt == null)
            throw new ArgumentNullException();
        StudentExt newStudent = new StudentExt(name, surname, idCounter, groupExt);
        groupExt.AddStudentToGroup(newStudent);
        _studentsExts.Add(newStudent);
        idCounter++;
        return newStudent;
    }

    public List<StudentExt> GetListOfUnsignedStudents(GroupExt groupExt)
    {
        if (groupExt == null)
            throw new ArgumentNullException();
        return groupExt.ReturnStudentsWithoutOgnp();
    }

    public void SignStudentToOgnp(StudentExt studentExt, OGNPLesson ognpLesson)
    {
        if (studentExt == null)
            throw new ArgumentNullException();
        if (ognpLesson == null)
            throw new ArgumentNullException();
        if (OGNPCollision(ognpLesson, studentExt))
            throw new CantSignForOGNPException(ognpLesson, studentExt);
        studentExt.SignForOgnp(ognpLesson);
        ognpLesson.AddToOgnp(studentExt);
    }

    public void UnsignStudentFromOgnp(StudentExt studentExt, OGNPLesson ognpLesson)
    {
        if (studentExt == null)
            throw new ArgumentNullException();
        if (ognpLesson == null)
            throw new ArgumentNullException();
        studentExt.UnsignFromOgnp(ognpLesson);
        ognpLesson.RemoveFromOgnp(studentExt);
    }

    public OGNPLesson CreateOgnpLessonForMegaFaculty(
        MegaFaculty megaFaculty,
        string name,
        string lector,
        LectureTimes lectureTime,
        uint audience,
        DayOfWeek dayOfWeek)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException();
        if (string.IsNullOrWhiteSpace(lector))
            throw new ArgumentNullException();
        if (megaFaculty == null)
            throw new ArgumentNullException();
        if (audience == 0)
            throw new ArgumentNullException();
        var ognp = new OGNPLesson(megaFaculty, name, lector, lectureTime, audience, dayOfWeek);
        _ognpLessons.Add(ognp);
        return ognp;
    }

    private MegaFaculty CalculateMegaFacultyForGroup(GroupNameExt groupNameExt)
    {
        return _megaFaculties.Find(name => name.Faculty.FacultyLetter == groupNameExt.Faculty.FacultyLetter) ??
               throw new CantFindMegaFacultyException(groupNameExt.Faculty);
    }

    private Flow CalculateFlowForGroup(GroupNameExt groupNameExt)
    {
        if (groupNameExt == null)
            throw new ArgumentNullException();
        int lastGroupOfFirstFlow = 7;
        var megaFacultyOfFlow = CalculateMegaFacultyForGroup(groupNameExt);
        if (groupNameExt.GroupNumber <= lastGroupOfFirstFlow)
            return megaFacultyOfFlow.ReturnHalfFlow(1);
        return megaFacultyOfFlow.ReturnHalfFlow(2);
    }

    private bool OGNPCollision(OGNPLesson ognpLesson, StudentExt studentExt)
    {
        if (ognpLesson == null)
            throw new ArgumentNullException();
        if (studentExt == null)
            throw new ArgumentNullException();
        return studentExt.GroupExt.Flow.Schedule.IsLectureAtTime(ognpLesson.DayOfWeek, ognpLesson.Lesson.LectureTime);
    }
}