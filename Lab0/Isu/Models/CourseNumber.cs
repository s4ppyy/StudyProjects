using Isu.Exceptions;
using Microsoft.VisualBasic;
namespace Isu.Models;

public class CourseNumber
{
    private static int minCourseNumber = 1;
    private static int maxCourseNumber = 4;
    public CourseNumber(int number)
    {
        if (number < minCourseNumber || number > maxCourseNumber)
        {
            throw new InvalidCourseNumberException(number);
        }

        Course = number;
    }

    public int Course { get; }
}