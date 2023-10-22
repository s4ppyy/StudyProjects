using ClassLibrary1.BusinessLayer;

namespace ClassLibrary1.Exceptions;

public class NoWorkerInSystemException : Exception
{
    public NoWorkerInSystemException(MessageProcessingSystem messageProcessingSystem)
    {
        MessageProcessingSystem = messageProcessingSystem;
    }

    public MessageProcessingSystem MessageProcessingSystem { get; }
}