using MSEkinci.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSEkinci.Northwind.Business.Abstract
{
    public interface IProductService
    {
        List<Product> GetAll();
        List<Product> GetByCategory(int categoryId);
        Product GetProduct(int productId);
        void Add(Product product);
        void Update(Product product);
        void Delete(int productId);
    }
}
