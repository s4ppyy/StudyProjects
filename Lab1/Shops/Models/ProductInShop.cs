using Shops.Exceptions;

namespace Shops.Models;

public class ProductInShop
{
    public ProductInShop(string name, uint amount, decimal cost)
    {
        if (amount == 0)
            throw new ZeroAmountException(amount);
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException();
        if (cost <= 0)
            throw new ZeroPriceException(cost);
        Name = name;
        Amount = amount;
        Cost = cost;
    }

    public string Name { get; }
    public uint Amount { get; private set; }

    public decimal Cost { get; private set; }

    public void ChangePrice(decimal newPrice)
    {
        if (newPrice <= 0)
            throw new ZeroPriceException(newPrice);
        Cost = newPrice;
    }

    public void ChangeAmount(uint downAmount)
    {
        if (downAmount >= Amount)
            throw new InappropriateParameterException(downAmount);
        Amount -= downAmount;
    }
}