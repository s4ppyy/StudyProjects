using Isu.Extra.Models;

namespace Isu.Extra.Exceptions;

public class LectureIsAlreadyExistsException : Exception
{
    public LectureIsAlreadyExistsException(LectureTimes lesson)
    {
        LectureTime = lesson;
    }

    public LectureTimes LectureTime { get; }
}