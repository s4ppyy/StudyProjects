using ClassLibrary1.DataAccessLayer;

namespace ClassLibrary1.BusinessLayer;

public class Manager : IEmployee
{
    private List<IEmployee> _messageHandlers;
    public Manager(Identification identification, string name, string surname, int id, MessageProcessingSystem system)
    {
        Identification = identification ?? throw new ArgumentNullException();
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException();
        if (string.IsNullOrWhiteSpace(surname))
            throw new ArgumentNullException();
        if (id < 0)
            throw new ArgumentNullException();
        Role = RoleLevel.Manager;
        Name = name;
        Surname = surname;
        Id = id;
        MessageProcessingSystem = system ?? throw new ArgumentNullException();
        _messageHandlers = new List<IEmployee>();
    }

    public RoleLevel Role { get; }

    public Identification Identification { get; }

    public string Name { get; }

    public string Surname { get; }

    public int Id { get; }

    public MessageProcessingSystem MessageProcessingSystem { get; }

    public void AddEmployee(IEmployee newEmployee)
    {
        if (newEmployee == null)
            throw new ArgumentNullException();
        _messageHandlers.Add(newEmployee);
    }

    public void DeleteEmployee(IEmployee toDelete)
    {
        if (toDelete == null)
            throw new ArgumentNullException();
        _messageHandlers.Remove(toDelete);
    }

    public List<IEmployee> GetAll()
    {
        return _messageHandlers;
    }

    public void Work()
    {
        var handledMessages = MessageProcessingSystem.GetHandledMessages();
        List<Message> report = handledMessages.Where(handledMessage => handledMessage.TimeOfClosing == DateTime.Today).ToList();

        MessageProcessingSystem.SetLastReport(report);
    }
}