using MSEkinci.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSEkinci.Northwind.Business.Abstract
{
    public interface ICartService
    {
        void AddToCart(Cart cart, Product product);
        void RemoveFromCart(Cart cart, int productId);
        void RemoveOneFromCart(Cart cart, int productId);
        void AddOneToCart(Cart cart, int productId);
        List<CartLine> List(Cart cart);
    }
}
