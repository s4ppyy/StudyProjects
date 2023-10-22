using Banks.Exception;

namespace Banks.Entities;

public class CreditAccount : IBankAccount
{
    public CreditAccount(Client client, Bank bank, decimal creditLimit, decimal bankUnapproveLimit, int bankComissionPercent)
    {
        Client = client ?? throw new ArgumentNullException();
        Bank = bank ?? throw new ArgumentNullException();
        if (creditLimit <= 0)
            throw new ArgumentNullException();
        CreditLimit = creditLimit;
        if (client.PassportID != 0 && client.Address != null)
        {
            Approved = true;
        }
        else
        {
            Approved = false;
        }

        if (bankUnapproveLimit <= 0)
            throw new ArgumentNullException();
        BankUnapproveWithdrawLimit = bankUnapproveLimit;
        if (bankComissionPercent <= 0)
            throw new ArgumentNullException();
        BankComissionPercent = bankComissionPercent;
    }

    public decimal Balance { get; private set; }
    public Client Client { get; }

    public Bank Bank { get; }
    public bool Approved { get; private set; }
    public decimal BankUnapproveWithdrawLimit { get; }

    public int BankComissionPercent { get; }
    public decimal CreditLimit { get; }

    public void RenewApprove()
    {
        if (Client.Address != null && Client.PassportID != null)
            Approved = true;
        Approved = false;
    }

    public void TimeChanges(int daysPassed)
    {
        if (daysPassed <= 0)
            throw new ArgumentNullException();
        for (int i = 0; i < daysPassed; i++)
        {
            BalanceChanges();
        }
    }

    public void BalanceChanges()
    {
        if (Balance < 0)
            Balance -= Balance * BankComissionPercent / 100;
    }

    public void UpBalance(decimal sum)
    {
        if (sum <= 0)
            throw new ArgumentNullException();
        if (Balance + sum > Math.Abs(CreditLimit))
            throw new CreditLimitOverflowed(sum);
        Balance += sum;
    }

    public void Withdraw(decimal sum)
    {
        RenewApprove();
        if (sum <= 0)
            throw new InvalidBalanceException(sum);
        if (!Approved && sum > BankUnapproveWithdrawLimit)
            throw new WithdrawLimitOverflowedException(sum);
        if (Math.Abs(Balance - sum) > Math.Abs(CreditLimit))
            throw new CreditLimitOverflowed(sum);
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
            throw new DepositIsLockedException(sum);
        Transaction newTransaction = new Transaction(this, accountToTransfer, sum);
        return newTransaction;
    }
}