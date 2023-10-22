using Isu.Extra.Models;

namespace Isu.Extra.Exceptions;

public class InvalidFacultyNameException : Exception
{
    public InvalidFacultyNameException(char letter)
    {
        InvalidFacultyLetter = letter;
    }

    public char InvalidFacultyLetter { get; }
}