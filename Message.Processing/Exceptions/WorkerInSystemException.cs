using ClassLibrary1.BusinessLayer;
using ClassLibrary1.DataAccessLayer;

namespace ClassLibrary1.Exceptions;

public class WorkerInSystemException : Exception
{
    public WorkerInSystemException(IEmployee worker)
    {
        Worker = worker;
    }

    public IEmployee Worker { get; }
}