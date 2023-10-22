using Banks.Exception;

namespace Banks.Entities;

public class Bank : IObservable
{
    private List<IObserver> _observers;
    private List<Client> _clients;
    private List<DepositConfiguration> _depositConfiguration;
    public Bank(string name, string address)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException();
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentNullException();
        Name = name;
        Address = address;
        _clients = new List<Client>();
        _depositConfiguration = new List<DepositConfiguration>();
        _observers = new List<IObserver>();
    }

    public string Name { get; }
    public string Address { get; }

    public decimal BankUnapproveTransferLimit { get; private set; }

    public int DebitPercent { get; private set; }

    public int CreditComissionPercent { get; private set; }

    public decimal CreditLimit { get; private set; }

    public int Validity { get; private set; }

    public IReadOnlyCollection<Client> Clients => _clients;

    public IReadOnlyCollection<DepositConfiguration> DepositCondition => _depositConfiguration;

    public void SetDebitPercent(int percent)
    {
        if (percent <= 0)
            throw new ArgumentNullException();
        DebitPercent = percent;
        NotifyObservers($"Debit Percent Had Changed! Now it is = {percent}");
    }

    public void SetUnapproveTransferLimit(decimal sum)
    {
        if (sum <= 0)
            throw new ArgumentNullException();
        BankUnapproveTransferLimit = sum;
        NotifyObservers($"Unapprove Transfer Limit Had Changed! Now it is = {sum}");
    }

    public void SetDepositPercent(int percent)
    {
        if (percent <= 0)
            throw new ArgumentNullException();
        DebitPercent = percent;
        NotifyObservers($"Deposit Percent Had Changed! Now it is = {percent}");
    }

    public void SetCreditComissionPercent(int percent)
    {
        if (percent <= 0)
            throw new ArgumentNullException();
        CreditComissionPercent = percent;
        NotifyObservers($"Credit Comission Percent Had Changed! Now it is = {percent}");
    }

    public void SetCreditLimit(decimal sum)
    {
        if (sum <= 0)
            throw new ArgumentNullException();
        CreditLimit = sum;
        NotifyObservers($"Credit Limit Sum Had Changed! Now it is = {sum}");
    }

    public void SetDepositValidity(int days)
    {
        if (days <= 0)
            throw new ArgumentNullException();
        Validity = days;
        NotifyObservers($"Deposit Validity Had Changed! Now it is = {days} days");
    }

    public void AddDepositConfiguration(decimal upperLimit, int percent)
    {
        if (upperLimit <= 0)
            throw new ArgumentNullException();
        if (percent <= 0)
            throw new ArgumentNullException();
        if (_depositConfiguration.Exists(limitToFind => limitToFind.UpperLimit == upperLimit))
            throw new DepositConfigurationIsAlreadyExistsException(upperLimit);
        _depositConfiguration.Add(new DepositConfiguration(upperLimit, percent));
    }

    public DebitAccount AddClientToDebit(Client client)
    {
        if (BankUnapproveTransferLimit <= 0)
            throw new ArgumentNullException();
        if (DebitPercent <= 0)
            throw new ArgumentNullException();
        if (client == null)
            throw new ArgumentNullException();
        DebitAccount newDebitAccount = new DebitAccount(client, this, DebitPercent, BankUnapproveTransferLimit);
        client.AddBankAccount(newDebitAccount);
        _clients.Add(client);
        return newDebitAccount;
    }

    public DepositAccount AddClientToDeposit(Client client, decimal startSum)
    {
        if (client == null)
            throw new ArgumentNullException();
        if (startSum <= 0)
            throw new ArgumentNullException();
        if (!_depositConfiguration.Any())
            throw new ArgumentNullException();
        if (Validity <= 0)
            throw new ArgumentNullException();
        DepositAccount newDepositAccount =
            new DepositAccount(client, this, startSum, _depositConfiguration, Validity, BankUnapproveTransferLimit);
        client.AddBankAccount(newDepositAccount);
        _clients.Add(client);
        return newDepositAccount;
    }

    public CreditAccount AddClientToCredit(Client client)
    {
        if (client == null)
            throw new ArgumentNullException();
        if (CreditLimit <= 0)
            throw new ArgumentNullException();
        if (BankUnapproveTransferLimit <= 0)
            throw new ArgumentNullException();
        if (CreditComissionPercent <= 0)
            throw new ArgumentNullException();
        CreditAccount newCreditAccount =
            new CreditAccount(client, this, CreditLimit, BankUnapproveTransferLimit, CreditComissionPercent);
        client.AddBankAccount(newCreditAccount);
        _clients.Add(client);
        return newCreditAccount;
    }

    public void RemoveClient(IBankAccount bankAccount)
    {
        if (bankAccount == null)
            throw new ArgumentNullException();
        if (bankAccount.Client.MoreThanOneAccountInBank(this))
        {
            bankAccount.Client.RemoveAccount(bankAccount);
        }
        else
        {
            bankAccount.Client.RemoveAccount(bankAccount);
            _clients.Remove(bankAccount.Client);
        }
    }

    public void AddObserver(IObserver observer)
    {
        if (observer == null)
            throw new ArgumentNullException();
        _observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        if (observer == null)
            throw new ArgumentNullException();
        _observers.Remove(observer);
    }

    public void NotifyObservers(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentNullException();
        foreach (IObserver observer in _observers)
        {
            observer.Update(message);
        }
    }
}