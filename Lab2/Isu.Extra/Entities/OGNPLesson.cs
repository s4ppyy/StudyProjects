using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class OGNPLesson
{
    private readonly List<StudentExt> _studentExtsOnThisOgnp;
    public OGNPLesson(MegaFaculty megaFaculty, string name, string lector, LectureTimes lectureTime, uint audience, DayOfWeek dayOfWeek)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException();
        if (string.IsNullOrWhiteSpace(lector))
            throw new ArgumentNullException();
        if (audience == 0)
            throw new ArgumentNullException();
        Lesson = new Lesson(name, lector, lectureTime, audience);
        MegaFaculty = megaFaculty ?? throw new ArgumentNullException();
        DayOfWeek = dayOfWeek;
        _studentExtsOnThisOgnp = new List<StudentExt>();
    }

    public MegaFaculty MegaFaculty { get; }
    public Lesson Lesson { get; }

    public DayOfWeek DayOfWeek { get; }
    public IReadOnlyList<StudentExt> Students => _studentExtsOnThisOgnp;

    public void AddToOgnp(StudentExt studentExt)
    {
        if (studentExt == null)
            throw new ArgumentNullException();
        _studentExtsOnThisOgnp.Add(studentExt);
    }

    public void RemoveFromOgnp(StudentExt studentExt)
    {
        if (studentExt == null)
            throw new ArgumentNullException();
        _studentExtsOnThisOgnp.Remove(studentExt);
    }
}