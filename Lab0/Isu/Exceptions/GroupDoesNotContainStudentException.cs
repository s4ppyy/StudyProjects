using Isu.Entities;

namespace Isu.Exceptions;

public class GroupDoesNotContainStudentException : Exception
{
    public GroupDoesNotContainStudentException(Student student)
    {
        Student = student;
    }

    public Student Student { get; }
}