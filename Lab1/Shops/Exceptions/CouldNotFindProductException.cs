using Shops.Models;

namespace Shops.Exceptions;

public class CouldNotFindProductException : Exception
{
    public CouldNotFindProductException(Products products)
        : base($"No {products.Name} in shop")
    {
        Products = products;
    }

    public Products Products { get; }
}