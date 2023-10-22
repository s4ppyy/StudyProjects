namespace Shops.Exceptions;

public class InappropriateParameterException : Exception
{
    public InappropriateParameterException(uint amount)
    {
        Amount = amount;
    }

    public InappropriateParameterException(decimal price)
    {
        Price = price;
    }

    public uint Amount { get; }
    public decimal Price { get; }
}