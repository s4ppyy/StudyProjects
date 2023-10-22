using Banks.Exception;

namespace Banks.Entities;

public class Client : IObserver
{
    private const int MinimalID = 100000;
    private const int MaximalID = 999999;
    private List<IBankAccount> _bankAccounts;

    public Client(int id)
    {
        ID = id;
        _bankAccounts = new List<IBankAccount>();
        Notifications = new List<string>();
    }

    public int ID { get; }
    public string? Name { get; private set; }

    public string? Surname { get; private set; }

    public string? Address { get; private set; }

    public int? PassportID { get; private set; }

    public List<string> Notifications { get; private set; }

    public IReadOnlyCollection<IBankAccount> BankAccounts => _bankAccounts;

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException();
        Name = name;
    }

    public void SetSurname(string surname)
    {
        if (string.IsNullOrWhiteSpace(surname))
            throw new ArgumentNullException();
        Surname = surname;
    }

    public void SetAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentNullException();
        Address = address;
    }

    public void SetPassportID(int id)
    {
        if (id < MinimalID || id > MaximalID)
            throw new InvalidPassportIDException(id);
        PassportID = id;
    }

    public void AddBankAccount(IBankAccount bankAccount)
    {
        if (bankAccount == null)
            throw new ArgumentNullException();
        _bankAccounts.Add(bankAccount);
    }

    public void RemoveAccount(IBankAccount bankAccount)
    {
        if (bankAccount == null)
            throw new ArgumentNullException();
        var toRemove = _bankAccounts.Find(toFind => toFind == bankAccount)
                       ?? throw new CantFindBankAccountException(bankAccount);
        _bankAccounts.Remove(toRemove);
    }

    public bool MoreThanOneAccountInBank(Bank bank)
    {
        List<IBankAccount> accountsInBank = _bankAccounts.Where(bankToCompare => bankToCompare.Bank == bank).ToList();
        if (accountsInBank.Count > 1)
            return true;
        return false;
    }

    public void Update(string message)
    {
        Notifications.Add(message);
    }
}