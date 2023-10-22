namespace Isu.Exceptions;

public class InvalidCourseNumberException : Exception
{
    public InvalidCourseNumberException(int courseNumber)
    {
        CourseNumber = courseNumber;
    }

    public int CourseNumber { get; }
}