using MSEkinci.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSEkinci.Northwind.MvcWebUI.ViewModels
{
    public class ConfirmedOrderViewModel
    {
        public ShippingDetails ShippingDetails { get; set; }
        public Cart Cart { get; set; }
    }
}
