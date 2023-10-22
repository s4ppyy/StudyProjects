using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class Schedule
{
    private const int StudyingDaysPerWeek = 6;
    private readonly List<StudyingDay> _scheduleForStudyingWeek;

    public Schedule()
    {
        _scheduleForStudyingWeek = new List<StudyingDay>(StudyingDaysPerWeek)
        {
            new StudyingDay(DayOfWeek.Monday), new StudyingDay(DayOfWeek.Tuesday),
            new StudyingDay(DayOfWeek.Wednesday), new StudyingDay(DayOfWeek.Thursday),
            new StudyingDay(DayOfWeek.Friday), new StudyingDay(DayOfWeek.Saturday),
        };
    }

    public Lesson AddLesson(string name, string lector, LectureTimes lectureTime, uint audience, DayOfWeek dayOfWeek)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException();
        if (string.IsNullOrWhiteSpace(lector))
            throw new ArgumentNullException();
        if (audience == 0)
            throw new ArgumentNullException();
        if (dayOfWeek == DayOfWeek.Sunday)
            throw new InvalidDayForLectionException(dayOfWeek);
        return _scheduleForStudyingWeek.Find(studDay => studDay.DayOfWeek == dayOfWeek)?.AddLesson(name, lector, lectureTime, audience) !;
    }

    public bool IsLectureAtTime(DayOfWeek dayOfWeek, LectureTimes lectureTime)
    {
        if (dayOfWeek == DayOfWeek.Sunday)
            throw new InvalidDayForLectionException(dayOfWeek);
        return _scheduleForStudyingWeek.Find(day => day.DayOfWeek == dayOfWeek) !.IsLessonExists(lectureTime);
    }
}