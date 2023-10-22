using Isu.Exceptions;

namespace Isu.Models;

public class GroupName
{
    private static int _minCourseNumber = 1;
    private static int _maxCourseNumber = 4;
    private static int _minSpecialisationNumber = 1;
    private static int _maxSpecialisationNumber = 2;
    private static int _minTempCodeNumber = 100;
    private static int _maxTempCodeNumber = 4992;
    public GroupName(string name)
    {
        if (!(name = name.ToLower()).StartsWith("m3"))
        {
            throw new InvalidGroupNameException(name);
        }

        string tempName = name;
        if (!int.TryParse(tempName.Remove(0, 2), out int tempGroupCode))
        {
            throw new InvalidGroupNameException(name);
        }

        if (tempGroupCode < _minTempCodeNumber || tempGroupCode > _maxTempCodeNumber)
            throw new InvalidGroupNameException(name);
        if (tempGroupCode / 10 > _minTempCodeNumber)
        {
            if (tempGroupCode / 1000 < _minCourseNumber || tempGroupCode / 1000 > _maxCourseNumber
                                                       || tempGroupCode % 10 < _minSpecialisationNumber
                                                       || tempGroupCode % 10 > _maxSpecialisationNumber)
            {
                throw new InvalidGroupNameException(name);
            }

            CourseNumber = new CourseNumber(tempGroupCode / 1000);
            GroupNumber = (tempGroupCode / 10) % 100;
            Specialisation = tempGroupCode % 10;
            FullName = name;
        }
        else
        {
            if (tempGroupCode / 100 < _minCourseNumber || tempGroupCode / 100 > _maxCourseNumber)
                throw new InvalidGroupNameException(name);

            CourseNumber = new CourseNumber(tempGroupCode / 100);
            GroupNumber = tempGroupCode % 100;
            Specialisation = null;
            FullName = name;
        }
    }

    public CourseNumber CourseNumber { get; }
    public int GroupNumber { get; }
    public int? Specialisation { get; }
    public string FullName { get; }
}
