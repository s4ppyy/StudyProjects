using Isu.Entities;
using Isu.Extra.Exceptions;

namespace Isu.Extra.Entities;

public class StudentExt
{
    public StudentExt(string name, string surname, int id, GroupExt groupExt)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException();
        if (string.IsNullOrWhiteSpace(surname))
            throw new ArgumentNullException();
        if (groupExt == null)
            throw new ArgumentNullException();
        Student = new Student(name, surname, id, groupExt.Group);
        GroupExt = groupExt;
        IsSignedToOgnp = false;
    }

    public OGNPLesson? FirstOGNP { get; private set; }
    public OGNPLesson? SecondOGNP { get; private set; }
    public Student Student { get; }
    public GroupExt GroupExt { get; }
    public bool IsSignedToOgnp { get; private set; }

    public void SignForOgnp(OGNPLesson ognpLesson)
    {
        if (ognpLesson == null)
            throw new ArgumentNullException();
        if (FirstOGNP != null && SecondOGNP != null)
            throw new CantSignForOGNPException(ognpLesson, this);
        if (FirstOGNP != null)
            SecondOGNP = ognpLesson;
        else
            FirstOGNP = ognpLesson;
        IsSignedToOgnp = true;
    }

    public void UnsignFromOgnp(OGNPLesson ognpLesson)
    {
        if (ognpLesson == null)
            throw new ArgumentNullException();
        if (FirstOGNP != null && FirstOGNP.Lesson.Name != ognpLesson.Lesson.Name &&
            SecondOGNP != null && SecondOGNP.Lesson.Name != ognpLesson.Lesson.Name)
            throw new CantFindOgnpException(ognpLesson);
        if (FirstOGNP != null && FirstOGNP.Lesson.Name == ognpLesson.Lesson.Name)
            FirstOGNP = null;
        if (SecondOGNP != null && SecondOGNP.Lesson.Name == ognpLesson.Lesson.Name)
            SecondOGNP = null;
        if (!IsSignedToOgnpCheck())
            IsSignedToOgnp = false;
    }

    public bool IsSignedToOgnpCheck()
    {
        if (FirstOGNP == null && SecondOGNP == null)
            return false;
        return true;
    }
}