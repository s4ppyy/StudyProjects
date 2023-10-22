using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class Lesson
{
    public Lesson(string name, string lector, LectureTimes lectureTime, uint audience)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException();
        Name = name;
        if (string.IsNullOrWhiteSpace(lector))
            throw new ArgumentNullException();
        Lector = lector;
        LectureTime = lectureTime;
        if (audience == 0)
            throw new ArgumentNullException();
        Audience = audience;
    }

    public string Name { get; }
    public string Lector { get; }
    public LectureTimes LectureTime { get; }
    public uint Audience { get; }
}