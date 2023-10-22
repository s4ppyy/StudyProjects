using Isu.Extra.Entities;

namespace Isu.Extra.Exceptions;

public class CantFindOgnpException : Exception
{
    public CantFindOgnpException(OGNPLesson ognpLesson)
    {
        OgnpLesson = ognpLesson;
    }

    public OGNPLesson OgnpLesson { get; }
}