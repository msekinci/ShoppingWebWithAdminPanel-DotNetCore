using MSEkinci.Northwind.Entities.Concrete;
using MSEkinci.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSEkinci.Northwind.DataAccess.Abstract
{
    public interface ICategoryDataAccessLayer:IEntityRepository<Category>
    {
        //Custom Operations
    }
}
