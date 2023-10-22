using Shops.Entities;

namespace Shops.Models;

public class BestBuyingSolution
{
    public BestBuyingSolution(Shop shop, decimal lowestPrice)
    {
        ShopWithLowestPrice = shop;
        LowestPrice = lowestPrice;
    }

    public Shop ShopWithLowestPrice { get; }
    public decimal LowestPrice { get; }
}