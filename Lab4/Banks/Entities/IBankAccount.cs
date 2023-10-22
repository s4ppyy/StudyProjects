namespace Banks.Entities;

public interface IBankAccount
{
    decimal Balance { get; }

    Client Client { get; }

    Bank Bank { get; }

    bool Approved { get; }

    decimal BankUnapproveWithdrawLimit { get; }

    void TimeChanges(int daysPassed);

    void BalanceChanges();

    public void RenewApprove();

    void UpBalance(decimal sum);

    void Withdraw(decimal sum);

    Transaction Transfer(IBankAccount accountToTransfer, decimal sum);
}