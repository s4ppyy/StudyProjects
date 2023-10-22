using System.Text;
using ClassLibrary1.DataAccessLayer;
using ClassLibrary1.Exceptions;
using Newtonsoft.Json;

namespace ClassLibrary1.BusinessLayer;

public class MessageProcessingSystem
{
    private EmployeeRepository _employees;
    private MessagesRepository _messagesInWork;
    private MessagesRepository _handledMessages;
    private IdentificatorsRepository _identificators;
    private int employeeIdCounter;
    private int messagesIdCounter;
    private int identificatorsIdCounter;
    private int reportCounter;
    public MessageProcessingSystem(Identification identification, string name, string surname)
    {
        if (identification == null)
            throw new ArgumentNullException();
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException();
        if (string.IsNullOrWhiteSpace(surname))
            throw new ArgumentNullException();
        Manager manager = new Manager(identification, name, surname, employeeIdCounter, this);
        _employees = new EmployeeRepository();
        _messagesInWork = new MessagesRepository();
        _handledMessages = new MessagesRepository();
        _identificators = new IdentificatorsRepository();
        _employees.Add(manager);
        _identificators.Add(identification);
        CurrentWorker = null;
        employeeIdCounter++;
        identificatorsIdCounter++;
    }

    public IEmployee? CurrentWorker { get; private set; }

    public string? LastAnswer { get; private set; }

    public List<Message>? LastReport { get; private set; }

    public bool HireEmployee(string login, string password, string name, string surname)
    {
        if (CurrentWorker == null || CurrentWorker.Role != RoleLevel.Manager)
            return false;
        var newIdentificator = new Identification(login, password, identificatorsIdCounter);
        MessageHandler newWorker = new MessageHandler(newIdentificator, name, surname, employeeIdCounter, this);
        _employees.Add(newWorker);
        CurrentWorker!.AddEmployee(newWorker);
        _identificators.Add(newIdentificator);
        employeeIdCounter++;
        return true;
    }

    public bool FireEmployee(IEmployee toFire)
    {
        if (CurrentWorker == null || CurrentWorker.Role != RoleLevel.Manager)
            return false;
        _employees.Delete(toFire.Id);
        _identificators.Delete(toFire.Identification.Id);
        CurrentWorker!.DeleteEmployee(toFire);
        return true;
    }

    public List<IEmployee> GetAllEmployeesOfCurrentWorker()
    {
        if (CurrentWorker != null)
            return CurrentWorker.GetAll();
        throw new NoWorkerInSystemException(this);
    }

    public bool AnswerMessage(string answer)
    {
        if (string.IsNullOrWhiteSpace(answer))
            throw new ArgumentNullException();
        if (CurrentWorker == null || CurrentWorker.Role != RoleLevel.MessageHandler)
            return false;
        LastAnswer = answer;
        CurrentWorker.Work();
        return true;
    }

    public bool MakeReport()
    {
        if (CurrentWorker == null || CurrentWorker.Role != RoleLevel.Manager)
            throw new AccessDeniedException(this);
        CurrentWorker.Work();
        if (LastReport == null)
            return false;
        string report = "Messages and Answers:\n";
        foreach (Message message in LastReport)
        {
            report = report + message.MessageText + " - " + message.Answer + "\n";
        }

        File.WriteAllText(@"C:\Users\Danee\OneDrive\Рабочий стол\C#\Reports\Report-" + $"{reportCounter}" + ".txt", report);
        reportCounter++;
        return true;
    }

    public Message GetMessageInWork()
    {
        if (CurrentWorker == null || CurrentWorker.Role != RoleLevel.MessageHandler)
            throw new AccessDeniedException(this);
        return _messagesInWork.GetFirstMessage();
    }

    public void CloseMessage(int messageId)
    {
        if (CurrentWorker == null || CurrentWorker.Role != RoleLevel.MessageHandler)
            throw new AccessDeniedException(this);
        if (messageId < 0)
            throw new ArgumentNullException();
        var closed = _messagesInWork.GetById(messageId);
        _messagesInWork.Delete(messageId);
        _handledMessages.Add(closed);
    }

    public List<Message> GetHandledMessages()
    {
        if (CurrentWorker == null || CurrentWorker.Role != RoleLevel.Manager)
            throw new AccessDeniedException(this);
        return _handledMessages.GetAll();
    }

    public void SetLastReport(List<Message> report)
    {
        if (CurrentWorker == null || CurrentWorker.Role != RoleLevel.Manager)
            throw new AccessDeniedException(this);
        LastReport = report;
    }

    public void SendMessage(MessageSource source, string messageText)
    {
        if (string.IsNullOrWhiteSpace(messageText))
            throw new ArgumentNullException();
        if (CurrentWorker != null)
            throw new WorkerCantSendMessageException(CurrentWorker);
        Message newMessage = new Message(source, MessageStatus.NewMessage, messageText, messagesIdCounter);
        _messagesInWork.Add(newMessage);
        messagesIdCounter++;
    }

    public bool Authentication(string login, string password)
    {
        if (CurrentWorker != null)
            throw new WorkerInSystemException(CurrentWorker);
        if (string.IsNullOrWhiteSpace(login))
            throw new ArgumentNullException();
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentNullException();
        var userToLogIn = _identificators.GetByLogin(login);
        if (userToLogIn == null)
            return false;
        if (!userToLogIn.Authentication(password))
            return false;
        CurrentWorker = _employees.GetByLogin(login);
        return true;
    }

    public void LogOut()
    {
        CurrentWorker = null;
    }

    public bool SerializeSystem()
    {
        if (CurrentWorker == null || CurrentWorker.Role != RoleLevel.Manager)
            return false;
        _employees.Serialize(@"C:\Users\Danee\OneDrive\Рабочий стол\C#\Serealise\EmployeeRepository.json");
        _handledMessages.Serialize(@"C:\Users\Danee\OneDrive\Рабочий стол\C#\Serealise\HandledMessagesRepository.json");
        _messagesInWork.Serialize(@"C:\Users\Danee\OneDrive\Рабочий стол\C#\Serealise\MessagesInWorkRepository.json");
        _identificators.Serialize(@"C:\Users\Danee\OneDrive\Рабочий стол\C#\Serealise\IdentificatorsRepository.json");
        return true;
    }

    public bool DeserializeSystem()
    {
        if (CurrentWorker == null || CurrentWorker.Role != RoleLevel.Manager)
            return false;
        _employees.Deserialize(@"C:\Users\Danee\OneDrive\Рабочий стол\C#\Serealise\EmployeeRepository.json");
        _identificators.Deserialize(@"C:\Users\Danee\OneDrive\Рабочий стол\C#\Serealise\IdentificatorsRepository.json");
        _handledMessages.Deserialize(@"C:\Users\Danee\OneDrive\Рабочий стол\C#\Serealise\HandledMessagesRepository.json");
        _messagesInWork.Deserialize(@"C:\Users\Danee\OneDrive\Рабочий стол\C#\Serealise\MessagesInWorkRepository.json");
        return true;
    }
}