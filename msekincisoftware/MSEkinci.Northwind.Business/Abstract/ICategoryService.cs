using MSEkinci.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSEkinci.Northwind.Business.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        void Add(Category product);
        void Update(Category product);
        void Delete(int categoryId);
    }
}
