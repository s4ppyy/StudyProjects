using Banks.Entities;

namespace Banks.Exception;

public class TransactionIsClosedException : System.Exception
{
    public TransactionIsClosedException(Transaction transaction)
    {
        Transaction = transaction;
    }

    public Transaction Transaction { get; }
}