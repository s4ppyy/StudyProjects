using Shops.Entities;

namespace Shops.Exceptions;

public class NotEnoughMoneyException : Exception
{
    public NotEnoughMoneyException(Shop shop)
    {
        Shop = shop;
    }

    public NotEnoughMoneyException(Buyer buyer)
    {
        Buyer = buyer;
    }

    public Shop? Shop { get; }
    public Buyer? Buyer { get; }
}