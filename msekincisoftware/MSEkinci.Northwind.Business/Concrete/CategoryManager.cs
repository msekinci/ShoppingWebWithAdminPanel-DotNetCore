using MSEkinci.Northwind.Business.Abstract;
using MSEkinci.Northwind.DataAccess.Abstract;
using MSEkinci.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSEkinci.Northwind.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryDataAccessLayer _categoryDal;

        public CategoryManager(ICategoryDataAccessLayer categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public void Add(Category category)
        {
            _categoryDal.Add(category);
        }

        public void Delete(int categoryId)
        {
            var model = _categoryDal.Get(p => p.CategoryId == categoryId);
            _categoryDal.Delete(model);
        }

        public List<Category> GetAll()
        {
            return _categoryDal.GetList();
        }

        public void Update(Category category)
        {
            _categoryDal.Update(category);
        }
    }
}
