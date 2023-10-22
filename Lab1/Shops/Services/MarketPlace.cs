using Shops.Entities;
using Shops.Exceptions;
using Shops.Models;

namespace Shops.Services;

public class MarketPlace
{
    private readonly List<Buyer> _buyers;
    private readonly List<Shop> _shops;
    public MarketPlace()
    {
        _buyers = new List<Buyer>();
        _shops = new List<Shop>();
    }

    public IReadOnlyCollection<Buyer> Buyers => _buyers;
    public IReadOnlyCollection<Shop> Shops => _shops;

    public void SupplyToShop(Shop shopToSupply)
    {
        if (shopToSupply.Balance < shopToSupply.Supply.TotalCost)
            throw new NotEnoughMoneyException(shopToSupply);
        var tempSupplyCart = shopToSupply.Supply.SupplyList;
        var tempStock = shopToSupply.ReturnStock();
        foreach (var product in tempSupplyCart)
        {
            if (tempStock.Contains(product))
            {
                var productToFind = tempStock.Find(prod => prod.Name == product.Name);
                productToFind!.ChangeAmount(productToFind.Amount + product.Amount);
            }
            else
            {
                tempStock.Add(product);
            }
        }

        shopToSupply.ChangeBalance(shopToSupply.Balance - shopToSupply.Supply.TotalCost);
        shopToSupply.ClearSupply();
    }

    public Buyer AddBuyer(string name, string surname)
    {
        var tempBuyer = new Buyer(name, surname);
        _buyers.Add(tempBuyer);
        return tempBuyer;
    }

    public Shop AddShop(string name, decimal startBalance, string address)
    {
        var tempShop = new Shop(name, startBalance, Guid.NewGuid(), address);
        _shops.Add(tempShop);
        return tempShop;
    }

    public void BuyProducts(Buyer buyer, Shop shop)
    {
        var shoppigCart = buyer.ShoppingCart.ProductsCollection;
        var shopRange = shop.ReturnProductInShops();
        decimal totalCost = 0;
        foreach (var prodToBuy in shoppigCart)
        {
            var foundedProd = shopRange.Find(prodInShop => prodInShop.Name == prodToBuy.Name) ??
                              throw new CouldNotFindProductException(prodToBuy);
            if (foundedProd.Amount < prodToBuy.Amount)
                throw new NotEnoughProductsException(foundedProd);
            totalCost += foundedProd.Cost * prodToBuy.Amount;
        }

        if (totalCost > buyer.Balance)
            throw new NotEnoughMoneyException(buyer);
        buyer.DownBalance(totalCost);
        var buyersBoughtProducts = buyer.ReturnBuyersProductsList();
        foreach (var boughtProduct in shoppigCart)
        {
            var prodToDelete = shopRange.Find(deleteFromShop => deleteFromShop.Name == boughtProduct.Name);
            if (prodToDelete!.Amount == boughtProduct.Amount)
            {
                shopRange.Remove(prodToDelete);
            }
            else
            {
                prodToDelete.ChangeAmount(boughtProduct.Amount);
            }

            buyersBoughtProducts.Add(boughtProduct);
        }

        buyer.ShoppingCart.ClearCart();

        shop.ChangeBalance(shop.Balance + totalCost);
    }

    public BestBuyingSolution? FindShopWithLessPrice(ShoppingCart shoppingCart)
    {
        decimal cheapestCost = decimal.MaxValue;
        Shop? shopWithCheapestCost = null;
        var tempShoppingCart = shoppingCart.ProductsCollection;
        foreach (var shop in _shops)
        {
            var tempShopRange = shop.ReturnProductInShops();
            decimal tempIShopAllCost = 0;
            foreach (var prodToBuy in tempShoppingCart)
            {
                if (!tempShopRange.Any(isProdInShop =>
                        isProdInShop.Name == prodToBuy.Name && isProdInShop.Amount >= prodToBuy.Amount))
                    break;
                var foundedProduct = tempShopRange.Find(_ => _.Name == prodToBuy.Name) ??
                                     throw new CouldNotFindProductException(prodToBuy);
                tempIShopAllCost += foundedProduct.Cost * prodToBuy.Amount;
            }

            if (tempIShopAllCost == 0)
            {
                continue;
            }

            if (tempIShopAllCost < cheapestCost)
            {
                cheapestCost = tempIShopAllCost;
                shopWithCheapestCost = shop;
            }
        }

        return shopWithCheapestCost != null ? new BestBuyingSolution(shopWithCheapestCost, cheapestCost) : null;
    }
}