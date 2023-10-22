using Banks.Entities;

namespace Banks.Exception;

public class CantFindBankAccountException : System.Exception
{
    public CantFindBankAccountException(IBankAccount bankAccount)
    {
        BankAccount = bankAccount;
    }

    public IBankAccount BankAccount { get; }
}