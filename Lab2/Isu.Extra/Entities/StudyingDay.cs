using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class StudyingDay
{
    private const int MaxLectionsPerDay = 7;
    private readonly List<Lesson> _studyingDay;

    public StudyingDay(DayOfWeek dayOfWeek)
    {
        DayOfWeek = dayOfWeek;
        _studyingDay = new List<Lesson>(MaxLectionsPerDay);
    }

    public DayOfWeek DayOfWeek { get; }

    public Lesson AddLesson(string name, string lector, LectureTimes lectureTime, uint audience)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException();
        if (string.IsNullOrWhiteSpace(lector))
            throw new ArgumentNullException();
        if (audience == 0)
            throw new ArgumentNullException();
        if (_studyingDay.Exists(lectureInTime => lectureInTime.LectureTime == lectureTime))
                 throw new LectureIsAlreadyExistsException(lectureTime);
        Lesson lecture = new Lesson(name, lector, lectureTime, audience);
        _studyingDay.Add(lecture);
        return lecture;
    }

    public bool IsLessonExists(LectureTimes lectureTime)
    {
        return _studyingDay.Exists(lectureOfDay => lectureOfDay.LectureTime == lectureTime);
    }
}