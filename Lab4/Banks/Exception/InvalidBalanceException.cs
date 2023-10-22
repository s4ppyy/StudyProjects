namespace Banks.Exception;

public class InvalidBalanceException : System.Exception
{
    public InvalidBalanceException(decimal sum)
    {
        Sum = sum;
    }

    public decimal Sum { get; }
}