using AutoMapper;
using Northwind.Domain.Base;
using Northwind.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICategoryService> _lazyCategoryService;
        private readonly Lazy<IProductService> _lazyproductService;
        private readonly Lazy<IOrderService> _lazyderOrderService;
        private readonly Lazy<IProductPhotoService> _lazyproductPhotoService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _lazyCategoryService = new Lazy<ICategoryService>(
                () => new CategoryService(repositoryManager, mapper));
            _lazyproductService = new Lazy<IProductService>(
                () => new ProductService(repositoryManager, mapper));
            _lazyderOrderService = new Lazy<IOrderService>(
                () => new OrderService(repositoryManager, mapper));
            _lazyproductPhotoService = new Lazy<IProductPhotoService>(
                () => new ProductPhotoService(repositoryManager, mapper));
        }

        public ICategoryService CategoryService => _lazyCategoryService.Value;

        public IProductService ProductService => _lazyproductService.Value;

        public IOrderService OrderService => _lazyderOrderService.Value;

        public IProductPhotoService ProductPhotoService => _lazyproductPhotoService.Value;
    }
}
