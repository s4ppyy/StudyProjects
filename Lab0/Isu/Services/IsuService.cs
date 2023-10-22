using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private readonly List<Group> _groups;
    private readonly List<Student> _students;
    public IsuService()
    {
        _groups = new List<Group>();
        _students = new List<Student>();
    }

    public Group AddGroup(GroupName name)
    {
        if (_groups.Exists(group => group.GroupName == name))
        {
            throw new GroupIsAlreadyExistsException(name);
        }

        Group group = new Group(name);
        _groups.Add(group);
        return group;
    }

    public Student AddStudent(Group group, string name, string surname, int id)
    {
        if (_groups.SingleOrDefault(groupPredicate => groupPredicate == group) != null)
        {
            if (_students.Exists(studentPredicate => studentPredicate.Id == id))
                throw new IdIsAlreadyUsedException(id);
            Student student = new Student(name, surname, id, group);
            _groups.Find(groupToFind => groupToFind == group)?.AddStudentToGroup(student);
            _students.Add(student);
            return student;
        }
        else
        {
            throw new GroupDoesNotExistsException(group);
        }
    }

    public Student GetStudent(int id)
    {
        return _students.Find(studentToFind => studentToFind.Id == id) ?? throw new NoSuchStudentException(id);
    }

    public Student? FindStudent(int id)
    {
        return _students.Find(studentToFind => studentToFind.Id == id);
    }

    public List<Student> FindStudents(GroupName groupName)
    {
        return _students.Where(student => student.Group.GroupName == groupName).ToList();
    }

    public List<Student> FindStudents(CourseNumber courseNumber)
    {
        return _students.Where(students => students.Group.CourseNumber == courseNumber).ToList();
    }

    public Group? FindGroup(GroupName groupName)
    {
        return _groups.Find(group => group.GroupName == groupName);
    }

    public Group GetGroup(GroupName groupName)
    {
        return _groups.Find(group => group.GroupName == groupName) ?? throw new GroupDoesNotExistsException(groupName);
    }

    public List<Group> FindGroups(CourseNumber courseNumber)
    {
        return _groups.Where(groups => groups.CourseNumber == courseNumber).ToList();
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        _groups.Find(group => group == student.Group) !.RemoveStudentFromGroup(student);
        GetGroup(newGroup.GroupName).AddStudentToGroup(student);
        GetStudent(student.Id).ChangeGroup(newGroup);
    }
}
