using MSEkinci.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MSEkinci.Core.DataAccess
{
    // IEntity Interface'sinden türeyen somut(new ile türeyebilen) nesneleri gönderebiliyoruz.
    public interface IEntityRepository<T> where T:class, IEntity, new()
    {
        //Değer girmeyebilir (filter == null)
        T Get(Expression<Func<T,bool>> filter = null);

        List<T> GetList(Expression<Func<T, bool>> filter = null);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
