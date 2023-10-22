namespace Banks.Exception;

public class DepositIsLockedException : System.Exception
{
    public DepositIsLockedException(decimal sum)
    {
        Sum = sum;
    }

    public decimal Sum { get; }
}