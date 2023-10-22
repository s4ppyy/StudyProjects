using System.Net.Security;
using Banks.Exception;

namespace Banks.Entities;

public class DepositAccount : IBankAccount
{
    private List<DepositConfiguration> _depositConfiguration;
    public DepositAccount(
        Client client,
        Bank bank,
        decimal startSum,
        List<DepositConfiguration> configuration,
        int validity,
        decimal bankUnapproveLimit)
    {
        Client = client ?? throw new ArgumentNullException();
        Bank = bank ?? throw new ArgumentNullException();
        if (startSum <= 0)
            throw new ArgumentNullException();

        Balance = startSum;
        if (!configuration.Any())
            throw new ArgumentNullException();
        _depositConfiguration = configuration;
        if (client.PassportID != 0 && client.Address != null)
        {
            Approved = true;
        }
        else
        {
            Approved = false;
        }

        Income = CountIncome(startSum);
        if (validity <= 0)
            throw new ArgumentNullException();
        Validity = validity;
        OperationsUnlocked = false;
        if (bankUnapproveLimit <= 0)
            throw new ArgumentNullException();
        BankUnapproveWithdrawLimit = bankUnapproveLimit;
    }

    public decimal Balance { get; private set; }
    public int Validity { get; private set; }
    public Client Client { get; }

    public Bank Bank { get; }
    public bool Approved { get; private set; }
    public decimal BankUnapproveWithdrawLimit { get; }

    public bool OperationsUnlocked { get; private set; }

    public decimal Income { get; }

    public IReadOnlyCollection<DepositConfiguration> DepositConfiguration => _depositConfiguration;

    public void TimeChanges(int daysPassed)
    {
        if (daysPassed <= 0)
            throw new ArgumentNullException();
        if (Validity <= daysPassed)
        {
            Validity = 0;
            BalanceChanges();
            OperationsUnlocked = true;
        }
        else
        {
            Validity -= daysPassed;
        }
    }

    public void RenewApprove()
    {
        if (Client.Address != null && Client.PassportID != null)
            Approved = true;
        Approved = false;
    }

    public void AddValidity(int daysAmount)
    {
        if (daysAmount <= 0)
            throw new ArgumentNullException();
        Validity += daysAmount;
        OperationsUnlocked = false;
    }

    public void BalanceChanges()
    {
        Balance += Income;
    }

    public void UpBalance(decimal sum)
    {
        if (sum <= 0)
            throw new ArgumentNullException();
        if (!OperationsUnlocked)
            throw new DepositIsLockedException(sum);
        Balance += sum;
    }

    public void Withdraw(decimal sum)
    {
        RenewApprove();
        if (sum <= 0)
            throw new ArgumentNullException();
        if (!OperationsUnlocked)
            throw new DepositIsLockedException(sum);
        if (Balance < sum)
            throw new NotEnoughMoneyException(sum);
        if (!Approved && BankUnapproveWithdrawLimit < sum)
            throw new DepositIsLockedException(sum);
        Balance -= sum;
    }

    public Transaction Transfer(IBankAccount accountToTransfer, decimal sum)
    {
        RenewApprove();
        if (!OperationsUnlocked)
            throw new DepositIsLockedException(sum);
        if (accountToTransfer == null)
            throw new ArgumentNullException();
        if (sum <= 0)
            throw new ArgumentNullException();
        if (!Approved && sum > BankUnapproveWithdrawLimit)
            throw new WithdrawLimitOverflowedException(sum);
        Transaction newTransaction = new Transaction(this, accountToTransfer, sum);
        return newTransaction;
    }

    private decimal CountIncome(decimal startSum)
    {
        // List<decimal> sumsMoreThenStart = (from configuration in _depositConfiguration where configuration.UpperLimit >= startSum select configuration.UpperLimit).ToList();
        List<decimal> sumsMoreThanStart = new List<decimal>();
        foreach (DepositConfiguration depositConfiguration in _depositConfiguration)
        {
            if (depositConfiguration.UpperLimit > startSum)
                sumsMoreThanStart.Add(depositConfiguration.UpperLimit);
        }

        decimal sumOfIncome = sumsMoreThanStart.Min();
        return startSum / 100 *
               _depositConfiguration.Find(percentOfSum => percentOfSum.UpperLimit == sumOfIncome) !.Percent;
    }
}