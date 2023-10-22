namespace Banks.Exception;

public class CreditLimitOverflowed : System.Exception
{
    public CreditLimitOverflowed(decimal sum)
    {
        Sum = sum;
    }

    public decimal Sum { get; }
}