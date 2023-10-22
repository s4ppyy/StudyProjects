using Banks.Exception;

namespace Banks.Entities;

public class Transaction
{
    public Transaction(IBankAccount from, IBankAccount to, decimal sum)
    {
        BankAccountFrom = from ?? throw new ArgumentNullException();
        BankAccountTo = to ?? throw new ArgumentNullException();
        if (sum == 0)
            throw new ArgumentNullException();
        Sum = sum;
        BankAccountFrom.Withdraw(sum);
        BankAccountTo.UpBalance(sum);
        Active = true;
    }

    public IBankAccount BankAccountFrom { get; }

    public IBankAccount BankAccountTo { get; }

    public decimal Sum { get; }

    public bool Active { get; private set; }

    public void UndoOperation()
    {
        if (!Active)
            throw new TransactionIsClosedException(this);
        if (BankAccountTo.Balance < Sum)
            throw new NotEnoughMoneyException(Sum);
        if (!BankAccountTo.Approved && Sum > BankAccountTo.BankUnapproveWithdrawLimit)
            throw new WithdrawLimitOverflowedException(Sum);
        BankAccountTo.Withdraw(Sum);
        BankAccountFrom.UpBalance(Sum);
        Active = false;
    }
}