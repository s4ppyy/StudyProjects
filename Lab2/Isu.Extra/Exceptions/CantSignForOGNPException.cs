using Isu.Extra.Entities;

namespace Isu.Extra.Exceptions;

public class CantSignForOGNPException : Exception
{
    public CantSignForOGNPException(OGNPLesson ognpLesson, StudentExt studentExt)
        : base($"Maximum amount of OGNP for student  {studentExt} reached")
    {
        OgnpLesson = ognpLesson;
    }

    public OGNPLesson OgnpLesson { get; }
}