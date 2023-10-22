namespace Shops.Exceptions;

public class ZeroAmountException : Exception
{
    public ZeroAmountException(uint amount)
    {
        Amount = amount;
    }

    public uint Amount { get; }
}