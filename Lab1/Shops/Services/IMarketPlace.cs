using Shops.Entities;

namespace Shops.Services;

public interface IMarketPlace
{
    Buyer AddBuyer(string name, string surname);

    Shop AddShop(string name);

    void BuyProductsWithLowestPrice(Buyer buyer);
    void BuyProducts(Buyer buyer, Shop shop);
    void SupplyToShop(Shop shopToSupply);

    Shop? FindShopWithLessPrice(ShoppingCart shoppingCart);
}