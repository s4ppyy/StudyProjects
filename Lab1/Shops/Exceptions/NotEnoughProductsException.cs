using Shops.Models;

namespace Shops.Exceptions;

public class NotEnoughProductsException : Exception
{
    public NotEnoughProductsException(Products products)
    {
        Products = products;
    }

    public NotEnoughProductsException(ProductInShop productInShop)
    {
        ProductsInShop = productInShop;
    }

    public Products? Products { get; }
    public ProductInShop? ProductsInShop { get; }
}