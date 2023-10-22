using Shops.Models;
using Shops.Services;
using Xunit;

namespace Shops.Test;

public class ShopTests
{
    [Fact]
    public void SupplyProductsToShop_ProductsSupplied()
    {
        var marketPlace1 = new MarketPlace();
        var shop1 = marketPlace1.AddShop("Pyaterochka", 1000000, "Gazovikov 12");
        var bread = new Products("Bread", 100);
        shop1.AddToSupply(bread, 2000);
        marketPlace1.SupplyToShop(shop1);
        var breadInShop = shop1.PutOnRangeFromStock(bread, 35);
        Assert.Contains(breadInShop, shop1.ShopRange);
    }

    [Fact]
    public void AddProductToRangeAndChangePrice_ProductAddedAndPriceChanged()
    {
        var marketPlace2 = new MarketPlace();
        var shop2 = marketPlace2.AddShop("Perekrestok", 1000000, "Lenina 47");
        var milk = new Products("Milk", 100);
        shop2.AddToSupply(milk, 5000);
        marketPlace2.SupplyToShop(shop2);
        var milkInShop = shop2.PutOnRangeFromStock(milk, 80);
        Assert.Contains(milkInShop, shop2.ShopRange);
        shop2.ChangeProductPrice(milkInShop, 75);
        Assert.True(milkInShop.Cost == 75);
    }

    [Fact]
    public void FindShopWithLowestPrice_ShopFounded()
    {
        var marketPlace3 = new MarketPlace();
        var shop3 = marketPlace3.AddShop("Pyaterochka", 1000000, "Gazovikov 12");
        var shop4 = marketPlace3.AddShop("Monetka", 500000, "Krasnozelenih 25");
        var beef = new Products("Beef", 90);
        shop3.AddToSupply(beef, 20000);
        marketPlace3.SupplyToShop(shop3);
        var beefInShop3 = shop3.PutOnRangeFromStock(beef, 450);
        shop4.AddToSupply(beef, 18000);
        marketPlace3.SupplyToShop(shop4);
        var beefInShop4 = shop4.PutOnRangeFromStock(beef, 400);
        var buyer1 = marketPlace3.AddBuyer("Ivan", "Ivanov");
        buyer1.UpBalance(40000);
        buyer1.AddProductToCart(beef);
        var whereToBuy = marketPlace3.FindShopWithLessPrice(buyer1.ShoppingCart);
        Assert.Equal(whereToBuy!.ShopWithLowestPrice, shop4);
    }

    [Fact]
    public void NotEnoughProductsInShop_BestSolutionIsNull()
    {
        var marketPlace4 = new MarketPlace();
        var shop5 = marketPlace4.AddShop("Lenta", 1000000, "Gazovikov 12");
        var bread = new Products("Bread", 100);
        shop5.AddToSupply(bread, 2000);
        marketPlace4.SupplyToShop(shop5);
        var breadInShop = shop5.PutOnRangeFromStock(bread, 35);
        var buyer2 = marketPlace4.AddBuyer("Petr", "Vasilyev");
        var breadForBuyer = new Products("Bread", 120);
        buyer2.AddProductToCart(breadForBuyer);
        var buyer2Solution = marketPlace4.FindShopWithLessPrice(buyer2.ShoppingCart);
        Assert.True(buyer2Solution == null);
    }

    [Fact]
    public void BuyProducts_ProductsBuyedAndPriceAndAmountHaveChanged()
    {
        var marketPlace5 = new MarketPlace();
        var buyer3 = marketPlace5.AddBuyer("Ivan", "Ivanov");
        var shop6 = marketPlace5.AddShop("Magnit", 100000, "Lenina 1");
        buyer3.UpBalance(1500);
        var milkForShop = new Products("Milk", 50);
        shop6.AddToSupply(milkForShop, 2500);
        marketPlace5.SupplyToShop(shop6);
        shop6.PutOnRangeFromStock(milkForShop, 100);
        var milkForBuyer = new Products("Milk", 10);
        buyer3.AddProductToCart(milkForBuyer);
        marketPlace5.BuyProducts(buyer3, shop6);
        Assert.True(shop6.Balance == 98500);
        Assert.True(buyer3.Balance == 500);
        Assert.True(shop6.ReturnProductInShops().Find(prodToFind => prodToFind.Name == "Milk") !.Amount == 40);
    }
}