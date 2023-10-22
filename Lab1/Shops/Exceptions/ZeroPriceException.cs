namespace Shops.Exceptions;

public class ZeroPriceException : Exception
{
    public ZeroPriceException(decimal price)
    {
        Price = price;
    }

    public decimal Price { get; }
}