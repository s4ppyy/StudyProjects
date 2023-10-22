using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class Shop
{
    private List<ProductInShop> _productInShops;
    private List<Products> _stock;

    public Shop(string name, decimal startBalance, Guid id, string address)
    {
        Name = name;
        Balance = startBalance;
        Id = id;
        Address = address;
        _productInShops = new List<ProductInShop>();
        _stock = new List<Products>();
        Supply = new Supply();
    }

    public string Name { get; }

    public decimal Balance { get; private set; }
    public Guid Id { get; }
    public string Address { get; }

    public IReadOnlyCollection<ProductInShop> ShopRange => _productInShops;

    public Supply Supply { get; }

    public void AddToSupply(Products products, decimal wholesomeCost)
    {
        if (products == null || wholesomeCost == 0)
            throw new ArgumentNullException();
        Supply.AddToSupply(products);
        Supply.UpCost(wholesomeCost);
    }

    public void ClearSupply()
    {
        Supply.ClearSupply();
    }

    public void ChangeBalance(decimal newBalance)
    {
        Balance = newBalance;
    }

    public List<Products> ReturnStock()
    {
        return _stock;
    }

    public List<ProductInShop> ReturnProductInShops()
    {
        return _productInShops;
    }

    public void ChangeProductPrice(ProductInShop productToChangePrice, decimal newPrice)
    {
        if (productToChangePrice == null || newPrice == 0)
            throw new ArgumentNullException();
        _productInShops.Find(productToFind => productToFind.Name == productToChangePrice.Name) !.ChangePrice(newPrice);
    }

    public ProductInShop PutOnRangeFromStock(Products product, decimal productCost)
    {
        if ((_stock.Find(pred => pred.Name == product.Name) ?? throw new CouldNotFindProductException(product)).Amount < product.Amount)
            throw new NotEnoughProductsException(product);
        var newProd = new ProductInShop(product.Name, product.Amount, productCost);
        _productInShops.Add(newProd);
        if (_stock.Find(pred => pred.Name == product.Name) !.Amount == product.Amount)
        {
            _stock.Remove(product);
        }
        else
        {
            _stock.Find(pred => pred.Name == product.Name) !.ChangeAmount(
                _stock.Find(pred => pred.Name == product.Name) !.Amount - product.Amount);
        }

        return newProd;
    }
}