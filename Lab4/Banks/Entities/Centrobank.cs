namespace Banks.Entities;

public class Centrobank
{
    private static Centrobank instance = null!;
    private List<Bank> _banks;

    protected Centrobank()
    {
        _banks = new List<Bank>();
    }

    public IReadOnlyCollection<Bank> Banks => _banks;

    public static Centrobank GetCentrobank()
    {
        if (instance == null)
            instance = new Centrobank();
        return instance;
    }

    public Bank AddBank(string name, string address)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException();
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentNullException();
        Bank newBank = new Bank(name, address);
        _banks.Add(newBank);
        return newBank;
    }

    public void TimeShift(int amountOfDays)
    {
        foreach (Bank bank in _banks)
        {
            foreach (Client bankClient in bank.Clients)
            {
                foreach (IBankAccount bankClientBankAccount in bankClient.BankAccounts)
                {
                    bankClientBankAccount.TimeChanges(amountOfDays);
                }
            }
        }
    }
}