using Isu.Models;

namespace Isu.Entities;

public class Student
{
    public Student(string name, string surname, int id, Group group)
    {
        Name = name;
        Surname = surname;
        Id = id;
        Group = group;
    }

    public string Name { get; }
    public string Surname { get; }
    public int Id { get; }
    public Group Group { get; private set; }

    public void ChangeGroup(Group newGroup)
    {
        Group = newGroup;
    }
}