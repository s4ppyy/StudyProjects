using Isu.Extra.Models;

namespace Isu.Extra.Exceptions;

public class CantFindMegaFacultyException : Exception
{
    public CantFindMegaFacultyException(Faculty faculty)
    {
        Faculty = faculty;
    }

    public CantFindMegaFacultyException(char facultyLetter)
    {
        FacultyLetter = facultyLetter;
    }

    public Faculty? Faculty { get; }
    public char FacultyLetter { get; }
}