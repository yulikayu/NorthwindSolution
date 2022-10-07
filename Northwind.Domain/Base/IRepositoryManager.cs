using Northwind.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Domain.Base
{
    public interface IRepositoryManager 
    {
        ICategoryRepository CategoryRepository { get; }
        ICustomerRepository CustomerRepository { get; }

        IProductRepository ProductRepository { get; }
        IOrderRepository OrderRepository { get; } 
        IProductPhotoRepository ProductPhotoRepository { get; }

        ISupplierRepository SupplierRepository { get; }

        IShipperRepository ShipperRepository { get; }

        IOrderDetailRepository OrderDetailRepository { get; }



        void Save();
        Task SaveAsync();

    }
}
