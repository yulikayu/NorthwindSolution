﻿using AutoMapper;
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
        private readonly Lazy<ISupplierService> _lazyupplierService;
        private readonly Lazy<IShipperService> _lazyShipperService;
        private readonly Lazy<IOrderDetailService> _lazyorderDetailService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _lazyorderDetailService = new Lazy<IOrderDetailService>(
               () => new OrderDetailService(repositoryManager, mapper));
            _lazyCategoryService = new Lazy<ICategoryService>(
                () => new CategoryService(repositoryManager, mapper));
            _lazyproductService = new Lazy<IProductService>(
                () => new ProductService(repositoryManager, mapper));
            _lazyderOrderService = new Lazy<IOrderService>(
                () => new OrderService(repositoryManager, mapper));
            _lazyproductPhotoService = new Lazy<IProductPhotoService>(
                () => new ProductPhotoService(repositoryManager, mapper));
            _lazyupplierService = new Lazy<ISupplierService>(
                () => new SupplierService(repositoryManager, mapper));
            _lazyShipperService = new Lazy<IShipperService>(
               () => new ShipperService(repositoryManager, mapper));
        }

        public ICategoryService CategoryService => _lazyCategoryService.Value;

        public IProductService ProductService => _lazyproductService.Value;

        public IOrderService OrderService => _lazyderOrderService.Value;

        public IProductPhotoService ProductPhotoService => _lazyproductPhotoService.Value;

        public ISupplierService SupplierService => _lazyupplierService.Value;

        public IShipperService ShipperService => _lazyShipperService.Value;

        public IOrderDetailService OrderDetailService => _lazyorderDetailService.Value;
    }
}
