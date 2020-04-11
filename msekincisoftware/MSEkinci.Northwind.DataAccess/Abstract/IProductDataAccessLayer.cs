using MSEkinci.Northwind.Entities.Concrete;
using MSEkinci.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSEkinci.Northwind.DataAccess.Abstract
{
    public interface IProductDataAccessLayer:IEntityRepository<Product>
    {
        //IEntityRepository'den farklı Product operasyonları için farklı işlemler ypılabilir
        //Örneğin join işlemi
    }
}
