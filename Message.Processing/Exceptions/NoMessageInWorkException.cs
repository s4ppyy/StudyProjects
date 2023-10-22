using ClassLibrary1.BusinessLayer;
using ClassLibrary1.DataAccessLayer;

namespace ClassLibrary1.Exceptions;

public class NoMessageInWorkException : Exception
{
    public NoMessageInWorkException(MessageHandler workerWithNoMessageInWork)
    {
        WorkerWithNoMessageInWork = workerWithNoMessageInWork;
    }

    public NoMessageInWorkException(MessagesRepository messagesRepository)
    {
        MessagesRepository = messagesRepository;
    }

    public MessageHandler? WorkerWithNoMessageInWork { get; }

    public MessagesRepository? MessagesRepository { get; }
}