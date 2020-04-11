using MSEkinci.Northwind.DataAccess.Abstract;
using MSEkinci.Northwind.Entities.Concrete;
using MSEkinci.Core.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSEkinci.Northwind.DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDataAccessLayer : EfEntityRepositoryBase<Category, NorthwindContext>, ICategoryDataAccessLayer
    {

    }
}
