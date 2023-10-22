using Shops.Exceptions;

namespace Shops.Models;

public class Products
{
    public Products(string name, uint amount)
    {
        if (amount == 0)
            throw new ZeroAmountException(amount);
        Name = name ?? throw new ArgumentNullException();
        Amount = amount;
    }

    public string Name { get; }
    public uint Amount { get; private set; }

    public void ChangeAmount(uint newAmount)
    {
        if (newAmount == 0)
            throw new ZeroAmountException(newAmount);
        Amount = newAmount;
    }
}