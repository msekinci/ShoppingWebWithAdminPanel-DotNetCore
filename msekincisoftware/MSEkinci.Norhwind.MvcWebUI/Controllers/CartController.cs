using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MSEkinci.Northwind.Business.Abstract;
using MSEkinci.Northwind.Business.Concrete;
using MSEkinci.Northwind.Entities.Concrete;
using MSEkinci.Northwind.MvcWebUI.Services;
using MSEkinci.Northwind.MvcWebUI.ViewModels;
using Newtonsoft.Json;
using System;
using Microsoft.AspNetCore.DataProtection;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using System.IO;

namespace MSEkinci.Northwind.MvcWebUI.Controllers
{
    public class CartController : Controller
    {
        private ICartSessionService _cartSessionService;
        private ICartService _cartService;
        public IProductService _productService;
        private Services.ICipherService _cipherService;

        public CartController(ICartSessionService cartSessionService, ICartService cartService, IProductService productService, IHostingEnvironment hostingEnvironment,
            Services.ICipherService cipherService)
        {
            _cartSessionService = cartSessionService;
            _cartService = cartService;
            _productService = productService;
            _cipherService = cipherService;
        }

        public IActionResult AddToCart(int productId)
        {
            var productToBeAdded = _productService.GetProduct(productId);

            if (productToBeAdded.UnitsInStock == 0)
            {
                TempData.Add("StockMessage", String.Format("Unfortunately there are no products left in stock."));
            }
            else
            {
                var cart = _cartSessionService.GetCart();
                _cartService.AddToCart(cart, productToBeAdded);
                _cartSessionService.SetCart(cart);

                TempData.Add("message", String.Format("Your Product: {0} was successfully added to the cart", productToBeAdded.ProductName));
            }

            return RedirectToAction("Index", "Product");
        }
        public IActionResult AddOne(int productId)
        {
            var cart = _cartSessionService.GetCart();
            var existCartItem = cart.CartLines.FirstOrDefault(x => x.Product.ProductId == productId);

            if (existCartItem != null)
            {
                if (existCartItem.Quantity < existCartItem.Product.UnitsInStock)
                {
                    _cartService.AddOneToCart(cart, productId);
                }
                else
                {
                    TempData["message"] = "This product has not more in the stock";
                }
                _cartSessionService.SetCart(cart);
            }

            return RedirectToAction("ListCart", "Cart");
        }

        public IActionResult RemoveOne(int productId)
        {
            var cart = _cartSessionService.GetCart();
            var existCartItem = cart.CartLines.FirstOrDefault(x => x.Product.ProductId == productId);

            if (existCartItem != null)
            {
                if (existCartItem.Quantity != 1)
                {
                    _cartService.RemoveOneFromCart(cart, productId);
                }
                else
                {
                    _cartService.RemoveFromCart(cart, productId);
                }
                _cartSessionService.SetCart(cart);
            }

            return RedirectToAction("ListCart", "Cart");
        }
        public IActionResult RemoveFromCart(int productId)
        {
            var cart = _cartSessionService.GetCart();
            var existCartItem = cart.CartLines.FirstOrDefault(x => x.Product.ProductId == productId);

            if (existCartItem != null)
            {
                var productToBeRemoved = _productService.GetProduct(productId);
                _cartService.RemoveFromCart(cart, productId);
                _cartSessionService.SetCart(cart);
                TempData.Add("RemovedMessage", String.Format("Your Product: {0} was successfully removed from the cart", productToBeRemoved.ProductName));
            }
            else
            {
                TempData.Add("message", String.Format("Already, there are no this product in your cart!"));
            }

            return RedirectToAction("ListCart", "Cart");
        }
        public IActionResult ListCart()
        {
            var cartViewModel = new CartSummaryViewModel
            {
                Cart = _cartSessionService.GetCart()
            };
            return View(cartViewModel);
        }

        public IActionResult Complete()
        {
            var shippingDetailViewModel = new ShippingDetailViewModel
            {
                ShippingDetails = new ShippingDetails()
            };

            return View(shippingDetailViewModel);
        }

        [HttpPost]
        public IActionResult Complete(ShippingDetailViewModel shippingDetail)
        {
            if (!ModelState.IsValid)
            {
                return View(shippingDetail);
            }
            else
            {
                ConfirmedOrderViewModel confirmedOrder = new ConfirmedOrderViewModel
                {
                    ShippingDetails = shippingDetail.ShippingDetails,
                    Cart = _cartSessionService.GetCart()
                };

                string objectString = JsonConvert.SerializeObject(confirmedOrder);
                string encryptKey = _cipherService.Encrypt(objectString);

                _cartSessionService.RemoveCart();

                return RedirectToAction("ConfirmedOrder", new RouteValueDictionary(
                        new { controller = "Cart", action = "ConfirmedOrder", confirmedOrder = encryptKey }));
            }
        }
        public IActionResult ConfirmedOrder(string confirmedOrder)
        {
            string dencryptKey =_cipherService.Decrypt(confirmedOrder);
            var model = JsonConvert.DeserializeObject<ConfirmedOrderViewModel>(dencryptKey);
            return View(model);
        }

    }
}
