using MSEkinci.Northwind.Business.Abstract;
using MSEkinci.Northwind.DataAccess.Abstract;
using MSEkinci.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSEkinci.Northwind.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDataAccessLayer _productDal;

        public ProductManager(IProductDataAccessLayer productDal)
        {
            _productDal = productDal;
        }

        public void Add(Product product)
        {
            _productDal.Add(product);
        }

        public void Delete(int productId)
        {
            var model = _productDal.Get(p => p.ProductId == productId);
            _productDal.Delete(model);
        }

        public List<Product> GetAll()
        {
            return _productDal.GetList();
        }

        public List<Product> GetByCategory(int categoryId)
        {
            return _productDal.GetList(p => p.CategoryId == categoryId || categoryId == 0);
        }

        public Product GetProduct(int productId)
        {
            return _productDal.Get(p => p.ProductId == productId);
        }

        public void Update(Product product)
        {
            _productDal.Update(product); 
        }
    }
}
