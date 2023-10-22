namespace Banks.Exception;

public class WithdrawLimitOverflowedException : System.Exception
{
    public WithdrawLimitOverflowedException(decimal sum)
    {
        Sum = sum;
    }

    public decimal Sum { get; }
}