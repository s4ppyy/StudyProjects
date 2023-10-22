using Banks.Exception;

namespace Banks.Entities;

public class DebitAccount : IBankAccount
{
    private const int DaysInMonth = 31;
    public DebitAccount(Client client, Bank bank, int bankPercent, decimal bankUnapproveLimit)
    {
        Client = client ?? throw new ArgumentNullException();
        Bank = bank ?? throw new ArgumentNullException();
        Balance = 0;
        if (bankPercent == 0)
            throw new ArgumentNullException();
        BankPercent = bankPercent;
        if (client.PassportID != 0 && client.Address != null)
        {
            Approved = true;
        }
        else
        {
            Approved = false;
        }

        if (bankUnapproveLimit == 0)
            throw new ArgumentNullException();
        BankUnapproveWithdrawLimit = bankUnapproveLimit;
        DaysTillIncome = DaysInMonth;
    }

    public decimal Balance { get; private set; }
    public Client Client { get; }
    public Bank Bank { get; }
    public int BankPercent { get; }

    public decimal Income { get; private set; }

    public int DaysTillIncome { get; private set; }
    public decimal BankUnapproveWithdrawLimit { get; }

    public bool Approved { get; private set; }

    public void TimeChanges(int daysPassed)
    {
        if (daysPassed <= 0)
            throw new ArgumentNullException();
        for (int i = 0; i < daysPassed; i++)
            BalanceChanges();
        if (DaysTillIncome <= daysPassed)
        {
            Balance += Income;
            Income = 0;
            DaysTillIncome = DaysInMonth;
        }
        else
        {
            DaysTillIncome -= daysPassed;
        }
    }

    public void RenewApprove()
    {
        if (Client.Address != null && Client.PassportID != null)
            Approved = true;
        Approved = false;
    }

    public void BalanceChanges()
    {
        decimal percent = Balance * BankPercent / 100;
        Income += percent;
    }

    public void UpBalance(decimal sum)
    {
        if (sum <= 0)
            throw new InvalidBalanceException(sum);
        Balance += sum;
    }

    public void Withdraw(decimal sum)
    {
        RenewApprove();
        if (sum <= 0)
            throw new InvalidBalanceException(sum);
        if (Balance < sum)
            throw new NotEnoughMoneyException(sum);
        if (!Approved && sum > BankUnapproveWithdrawLimit)
            throw new WithdrawLimitOverflowedException(sum);
        Balance -= sum;
    }

    public Transaction Transfer(IBankAccount accountToTransfer, decimal sum)
    {
        RenewApprove();
        if (accountToTransfer == null)
            throw new ArgumentNullException();
        if (sum <= 0)
            throw new ArgumentNullException();
        if (!Approved && sum > BankUnapproveWithdrawLimit)
            throw new WithdrawLimitOverflowedException(sum);
        Transaction newTransaction = new Transaction(this, accountToTransfer, sum);
        return newTransaction;
    }
}