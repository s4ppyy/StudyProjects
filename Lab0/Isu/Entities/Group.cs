using System.Net;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Group
{
    private static int _capacity = 30;
    private readonly List<Student> _studentsInGroup;

    public Group(GroupName groupName)
    {
        _studentsInGroup = new List<Student>(_capacity);
        GroupName = groupName;
        CourseNumber = groupName.CourseNumber;
        Specialisation = groupName.Specialisation;
    }

    public GroupName GroupName { get; }
    public CourseNumber CourseNumber { get; }
    public int? Specialisation { get; }

    public bool AddStudentToGroup(Student newStudent)
    {
        if (IsFull()) throw new GroupOverflowException(this);
        _studentsInGroup.Add(newStudent);
        return true;
    }

    public void RemoveStudentFromGroup(Student exStudent)
    {
        if (!_studentsInGroup.Remove(exStudent))
        {
            throw new GroupDoesNotContainStudentException(exStudent);
        }
    }

    public bool IsStudentExists(Student student)
    {
        return _studentsInGroup.Exists(studentPredicate => studentPredicate.Group == student.Group);
    }

    private bool IsFull()
         {
             return _capacity == _studentsInGroup.Count;
         }
}