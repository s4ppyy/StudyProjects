using Shops.Models;

namespace Shops.Entities;

public class ShoppingCart
{
    private readonly List<Products> _shoppingCart;

    public ShoppingCart()
    {
        _shoppingCart = new List<Products>();
    }

    public IReadOnlyCollection<Products> ProductsCollection => _shoppingCart;

    public void AddToCart(Products products)
    {
        if (products == null)
            throw new ArgumentNullException();
        _shoppingCart.Add(products);
    }

    public void ClearCart()
    {
        _shoppingCart.Clear();
    }
}