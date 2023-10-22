using Isu.Extra.Exceptions;

namespace Isu.Extra.Models;

public class Faculty
{
    public Faculty(char letter)
    {
        if (letter == 'm')
        {
            FacultyName = "FITiP";
            FacultyLetter = 'm';
        }
        else if (letter == 'p')
        {
            FacultyName = "CT";
            FacultyLetter = 'p';
        }
        else if (letter == 'n')
        {
            FacultyName = "FBIT";
            FacultyLetter = 'n';
        }
        else if (letter == 'z')
        {
            FacultyName = "FizFac";
            FacultyLetter = 'z';
        }
        else
        {
            throw new InvalidFacultyNameException(letter);
        }
    }

    public string FacultyName { get; private set; }
    public char FacultyLetter { get; }

    public void SetFaculty(char letter)
    {
        char facultyLetter = char.ToLower(letter);
        if (facultyLetter == 'm')
            FacultyName = "FITiP";
        if (facultyLetter == 'p')
            FacultyName = "CT";
        if (facultyLetter == 'n')
            FacultyName = "FBIT";
        if (letter == 'z')
            FacultyName = "FizFac";
        throw new InvalidFacultyNameException(letter);
    }
}