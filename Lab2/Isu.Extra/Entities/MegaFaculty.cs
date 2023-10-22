using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class MegaFaculty
{
    private const int FirstCourseNumber = 1;
    private const int SecondCourseNumber = 2;
    private const int ThirdCourseNumber = 3;
    private const int FourthCourseNumber = 4;
    private const int TotalAmountOfHalfFlows = 8;
    private readonly List<Flow> _flows;

    public MegaFaculty(Faculty faculty)
    {
        if (faculty == null)
            throw new ArgumentNullException();
        Faculty = faculty;
        _flows = new List<Flow>(TotalAmountOfHalfFlows)
        {
            new Flow(1, FirstCourseNumber), new Flow(2, FirstCourseNumber),
            new Flow(1, SecondCourseNumber), new Flow(2, SecondCourseNumber),
            new Flow(1, ThirdCourseNumber), new Flow(2, ThirdCourseNumber),
            new Flow(1, FourthCourseNumber), new Flow(2, FourthCourseNumber),
        };
    }

    public Faculty Faculty { get; }
    public IReadOnlyList<Flow> Flows => _flows;

    public Flow ReturnHalfFlow(int number)
    {
        if (number == 0)
            throw new ArgumentNullException();
        if (number > 2)
            throw new InvalidFlowNumberException(number);
        return number == 1 ? _flows[0] : _flows[1];
    }
}