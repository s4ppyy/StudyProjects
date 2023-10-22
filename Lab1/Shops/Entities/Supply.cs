using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class Supply
{
    private readonly List<Products> _supplyCart;
    public Supply()
    {
        _supplyCart = new List<Products>();
        TotalCost = 0;
    }

    public IReadOnlyCollection<Products> SupplyList => _supplyCart;

    public decimal TotalCost { get; private set; }

    public void AddToSupply(Products products)
    {
        if (products == null)
            throw new ArgumentNullException();
        _supplyCart.Add(products);
    }

    public void UpCost(decimal cost)
    {
        if (cost <= 0)
            throw new ZeroPriceException(cost);
        TotalCost += cost;
    }

    public void ClearSupply()
    {
        _supplyCart.Clear();
        TotalCost = 0;
    }
}