using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSEkinci.Northwind.Business.Abstract;
using MSEkinci.Northwind.Entities.Concrete;
using MSEkinci.Northwind.MvcWebUI.ViewModels;

namespace MSEkinci.Northwind.MvcWebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        public AdminController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        public IActionResult Index(int page = 1, int category = 0, string productName = null)
        {
            int pageSize = 25;
            var products = _productService.GetByCategory(category).AsQueryable();

            #region search
            if (!string.IsNullOrEmpty(productName))
            {
                products = products.Where(x => x.ProductName.Contains(productName));
            }
            #endregion
            ProductListAdminViewModel model = new ProductListAdminViewModel
            {
                Products = products.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                PageCount = (int)Math.Ceiling(products.Count() / (double)pageSize),
                PageSize = pageSize,
                CurrentCategory = category,
                CurrentPage = page,
                TotalProductSize = products.Count()
            };
            return View(model);
        }

        public IActionResult AddNewProduct()
        {
            AddNewProductViewModel productViewModel = new AddNewProductViewModel
            {
                Product = new Product(),
                Categories = _categoryService.GetAll()
            };
            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult AddNewProduct(AddNewProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(productViewModel);
            }
            _productService.Add(productViewModel.Product);
            TempData["message"] = "Your product has been successfully added.";
            return RedirectToAction("Index");
        }
        public IActionResult DeleteProduct(int productId)
        {
            _productService.Delete(productId);
            return RedirectToAction("Index","Admin");
        }
        public IActionResult UpdateProduct(int productId)
        {
            UpdateProductViewModel model = new UpdateProductViewModel
            {
                Product = _productService.GetProduct(productId),
                Categories = _categoryService.GetAll()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult UpdateProduct(UpdateProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(productViewModel);
            }
            _productService.Update(productViewModel.Product);
            TempData["message"] = "Your product has been successfully updated.";
            return RedirectToAction("Index");
        }
    }
}