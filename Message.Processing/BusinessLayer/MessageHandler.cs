using ClassLibrary1.DataAccessLayer;
using ClassLibrary1.Exceptions;

namespace ClassLibrary1.BusinessLayer;

public class MessageHandler : IEmployee
{
    private List<IEmployee> _employees;
    public MessageHandler(Identification identification, string name, string surname, int id, MessageProcessingSystem system)
    {
        Identification = identification ?? throw new ArgumentNullException();
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException();
        if (string.IsNullOrWhiteSpace(surname))
            throw new ArgumentNullException();
        if (id < 1)
            throw new ArgumentNullException();
        Role = RoleLevel.MessageHandler;
        Name = name;
        Surname = surname;
        Id = id;
        _employees = new List<IEmployee>();
        System = system ?? throw new ArgumentNullException();
    }

    public RoleLevel Role { get; }

    public Identification Identification { get; }

    public string Name { get; }

    public string Surname { get; }

    public int Id { get; }

    public Message? CurrentMessage { get; private set; }

    public MessageProcessingSystem System { get; }

    public void AddEmployee(IEmployee newEmployee)
    {
        if (newEmployee == null)
            throw new ArgumentNullException();
        _employees.Add(newEmployee);
    }

    public void DeleteEmployee(IEmployee toDelete)
    {
        if (toDelete == null)
            throw new ArgumentNullException();
        _employees.Add(toDelete);
    }

    public List<IEmployee> GetAll()
    {
        return _employees;
    }

    public void Work()
    {
        CurrentMessage = System.GetMessageInWork();
        WriteAnswer(System.LastAnswer!);
        CloseCurrentMessage();
    }

    public void WriteAnswer(string answer)
    {
        if (string.IsNullOrWhiteSpace(answer))
            throw new ArgumentNullException();
        if (CurrentMessage == null)
            throw new NoMessageInWorkException(this);
        CurrentMessage.AddToAnswer(answer);
    }

    public void CloseCurrentMessage()
    {
        if (CurrentMessage == null)
            throw new NoMessageInWorkException(this);
        CurrentMessage.CloseMessage();
        System.CloseMessage(CurrentMessage.MessageId);
    }
}