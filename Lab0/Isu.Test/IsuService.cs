using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Test;

public class IsuServiceTests
{
    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        IsuService isuService = new IsuService();
        GroupName groupName = new GroupName("M32101");
        Group group = isuService.AddGroup(groupName);
        Student student = isuService.AddStudent(isuService.GetGroup(groupName), "Yan", "Ivanov", 335588);
        Assert.True(isuService.GetStudent(335588).Group.Equals(group));
        Assert.True(isuService.GetGroup(groupName).IsStudentExists(student));
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        IsuService isuService = new IsuService();
        GroupName groupName4 = new GroupName("m34222");
        Group group = isuService.AddGroup(groupName4);
        for (int i = 100000; i < 100030; i++)
        {
            isuService.AddStudent(isuService.GetGroup(groupName4), "Ching", "Chongov", i);
        }

        Assert.Throws<GroupOverflowException>(() => isuService.AddStudent(group, "Petr", "Ivanov", 100031));
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        Assert.Throws<InvalidGroupNameException>(() => new GroupName("m31103"));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        IsuService isuService = new IsuService();
        GroupName groupName1 = new GroupName("M33101");
        GroupName groupName2 = new GroupName("M33111");
        Group group1 = isuService.AddGroup(groupName1);
        Group group2 = isuService.AddGroup(groupName2);
        Student student = isuService.AddStudent(group1, "Misha", "Petrov", 124256);
        isuService.ChangeStudentGroup(student, group2);
        Assert.True(student.Group == group2);
    }
}