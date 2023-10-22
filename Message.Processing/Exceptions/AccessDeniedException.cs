using ClassLibrary1.BusinessLayer;
using ClassLibrary1.DataAccessLayer;

namespace ClassLibrary1.Exceptions;

public class AccessDeniedException : Exception
{
    public AccessDeniedException(MessageProcessingSystem system)
    {
        System = system;
    }

    public MessageProcessingSystem System { get; }
}