using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services.Abstraction
{
    public interface IServiceManager
    {
        //Mau dibuat internal juga gpp
        ICategoryService CategoryService { get; }
        IProductService ProductService { get; } 
        IOrderService OrderService { get; }
        IProductPhotoService ProductPhotoService { get; }
        IShipperService ShipperService { get; }

        ISupplierService SupplierService { get; }
    }
}
