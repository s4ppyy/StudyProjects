using Shops.Models;

namespace Shops.Entities;

public class Buyer
{
    private List<Products> _buyersProducts;
    public Buyer(string name, string surname)
    {
        Name = name;
        Surname = surname;
        Balance = 0;
        _buyersProducts = new List<Products>();
        ShoppingCart = new ShoppingCart();
    }

    public decimal Balance { get; private set; }
    public string Name { get; }
    public string Surname { get; }

    public ShoppingCart ShoppingCart { get; }

    public void UpBalance(decimal sum)
    {
        Balance += sum;
    }

    public void AddProductToCart(Products product)
    {
        ShoppingCart.AddToCart(product);
    }

    public void DownBalance(decimal sum)
    {
        Balance -= sum;
    }

    public List<Products> ReturnBuyersProductsList()
    {
        return _buyersProducts;
    }
}