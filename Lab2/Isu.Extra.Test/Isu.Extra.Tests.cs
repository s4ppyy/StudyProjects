using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Extra.Services;
using Isu.Models;
using Xunit;
namespace Isu.Extra.Test;

public class Isu_Extra_Tests
{
    [Fact]
    public void AddOGNPCourse_CourseAdded()
    {
        IsuExtraService isuExtraService = new IsuExtraService();
        var megaFaculty = isuExtraService.GetMegaFacultyFromLetter('p');
        var ognp1 = isuExtraService.CreateOgnpLessonForMegaFaculty(
            megaFaculty,
            "Insane Math",
            "Morozova",
            LectureTimes.FifthLesson,
            1232,
            DayOfWeek.Friday);
        Assert.Contains(ognp1, isuExtraService.OgnpLessons);
    }

    [Fact]
    public void AddStudentToOGNP_StudentAdded()
    {
        IsuExtraService isuExtraService = new IsuExtraService();
        var megaFaculty = isuExtraService.GetMegaFacultyFromLetter('p');
        var ognp1 = isuExtraService.CreateOgnpLessonForMegaFaculty(
            megaFaculty,
            "Insane Math",
            "Morozova",
            LectureTimes.FifthLesson,
            1232,
            DayOfWeek.Friday);
        GroupNameExt groupNameExt = new GroupNameExt(new GroupName("m32101"));
        var group = isuExtraService.AddGroup(groupNameExt);
        var student = isuExtraService.AddStudent("Ivan", "Ivanov", group);
        isuExtraService.SignStudentToOgnp(student, ognp1);
        Assert.Contains(student, ognp1.Students);
        Assert.Equal(student.FirstOGNP, ognp1);
    }

    [Fact]
    public void UnsignStudentFromOgnp_StudentUnsigned()
    {
        IsuExtraService isuExtraService = new IsuExtraService();
        var megaFaculty = isuExtraService.GetMegaFacultyFromLetter('p');
        var ognp1 = isuExtraService.CreateOgnpLessonForMegaFaculty(
            megaFaculty,
            "Insane Math",
            "Morozova",
            LectureTimes.FifthLesson,
            1232,
            DayOfWeek.Friday);
        GroupNameExt groupNameExt = new GroupNameExt(new GroupName("m32101"));
        var group = isuExtraService.AddGroup(groupNameExt);
        var student = isuExtraService.AddStudent("Ivan", "Ivanov", group);
        isuExtraService.SignStudentToOgnp(student, ognp1);
        Assert.Contains(student, ognp1.Students);
        Assert.Equal(student.FirstOGNP, ognp1);
        isuExtraService.UnsignStudentFromOgnp(student, ognp1);
        Assert.True(!student.IsSignedToOgnp);
        Assert.True(!ognp1.Students.Contains(student));
    }

    [Fact]
    public void GetFlowsViaCourse_FlowsFounded()
    {
        IsuExtraService isuExtraService = new IsuExtraService();
        var flows = isuExtraService.GetFlows(2);
        foreach (Flow flow in flows)
        {
            Assert.True(flow.CourseNumber == 2);
        }
    }

    [Fact]
    public void GetStudentsFromOgnpGroup_StudentsFound()
    {
        IsuExtraService isuExtraService = new IsuExtraService();
        var megaFaculty = isuExtraService.GetMegaFacultyFromLetter('p');
        var ognp1 = isuExtraService.CreateOgnpLessonForMegaFaculty(
            megaFaculty,
            "Insane Math",
            "Morozova",
            LectureTimes.FifthLesson,
            1232,
            DayOfWeek.Friday);
        GroupNameExt groupNameExt = new GroupNameExt(new GroupName("m32101"));
        var group = isuExtraService.AddGroup(groupNameExt);
        var student1 = isuExtraService.AddStudent("Ivan", "Ivanov", group);
        var student2 = isuExtraService.AddStudent("Sergey", "Sergeev", group);
        isuExtraService.SignStudentToOgnp(student1, ognp1);
        isuExtraService.SignStudentToOgnp(student2, ognp1);
        Assert.Contains(student1, ognp1.Students);
        Assert.Contains(student2, ognp1.Students);
    }

    [Fact]
    public void GetStudentsWithoutOgnp_StudentsFound()
    {
        IsuExtraService isuExtraService = new IsuExtraService();
        var megaFaculty = isuExtraService.GetMegaFacultyFromLetter('p');
        var ognp1 = isuExtraService.CreateOgnpLessonForMegaFaculty(
            megaFaculty,
            "Insane Math",
            "Morozova",
            LectureTimes.FifthLesson,
            1232,
            DayOfWeek.Friday);
        GroupNameExt groupNameExt = new GroupNameExt(new GroupName("m32101"));
        var group = isuExtraService.AddGroup(groupNameExt);
        var student = isuExtraService.AddStudent("Ivan", "Ivanov", group);
        var listOfUnsignedStudents = isuExtraService.GetListOfUnsignedStudents(group);
        Assert.Contains(student, listOfUnsignedStudents);
    }
}