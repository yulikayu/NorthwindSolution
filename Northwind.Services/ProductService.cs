using AutoMapper;
using Northwind.Contracts.Dto.Category;
using Northwind.Contracts.Dto.OrderDetail;
using Northwind.Contracts.Dto.Orders;
using Northwind.Contracts.Dto.Product;
using Northwind.Domain.Base;
using Northwind.Domain.Models;
using Northwind.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services
{
    internal class ProductService : IProductService
    {
        private IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ProductService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void edit(ProductDto productDto)
        {
            var edit = _mapper.Map<Product>(productDto);
            _repositoryManager.ProductRepository.edit(edit);
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<ProductDto>> GetAllProduct(bool trackChanges)
        {
            var ProductMDL = await _repositoryManager.ProductRepository.GetAllProduct(trackChanges);
            //source= ProductMDL,targer CategoryDto
            var productDto = _mapper.Map<IEnumerable<ProductDto>>(ProductMDL);
            return productDto;
        }

        public async Task<ProductDto> GetProductById(int ProductId, bool trackChanges)
        {
            var ProductMDL = await _repositoryManager.ProductRepository.GetProductById(ProductId, trackChanges);
            //source= ProductMDL,targer CategoryDto
            var productDto = _mapper.Map<ProductDto>(ProductMDL);
            return productDto;
        }

        public void remove(ProductDto productDto)
        {
            var edit = _mapper.Map<Product>(productDto);
            _repositoryManager.ProductRepository.remove(edit);
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<ProductDto>> GetProductPaged(int pageIndex, int pageSize, bool trackChanges)
        {
            var ProductMDL = await _repositoryManager.ProductRepository.GetProductPaged(pageIndex, pageSize, trackChanges);
            //source= ProductMDL,targer CategoryDto
            var productDto = _mapper.Map<IEnumerable<ProductDto>>(ProductMDL);
            return productDto;
        }

        public void Insert(ProductForCreatDto productForCreatDto)
        {
            var insert = _mapper.Map<Product>(productForCreatDto);
            _repositoryManager.ProductRepository.insert(insert);
            _repositoryManager.Save();
        }

        public ProductDto CreateProductId(ProductForCreatDto productForCreateDto)
        {
            var productModel = _mapper.Map<Product>(productForCreateDto);
            _repositoryManager.ProductRepository.insert(productModel);
            _repositoryManager.Save();
            var productDto = _mapper.Map<ProductDto>(productModel);
            return productDto;
        }

        public void CreateProductManyPhoto(ProductForCreatDto productForCreateDto, List<ProductPhotoForCreateDto> productPhotoForCreateDtos)
        {
            //1. insert into table product
            var productModel = _mapper.Map<Product>(productForCreateDto);
            _repositoryManager.ProductRepository.insert(productModel);
            _repositoryManager.Save();

            // insert into table productPhoto
            foreach (var item in productPhotoForCreateDtos)
            {
                item.PhotoProductId = productModel.ProductId;
                var photoModel = _mapper.Map<ProductPhoto>(item);
                _repositoryManager.ProductPhotoRepository.insert(photoModel);
            }
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<ProductDto>> GetProductIdandPaged(int productId, int pageIndex, int pageSize, bool trackChanges)
        {
            var ProductMDL = await _repositoryManager.ProductRepository.GetProductIdandPaged(productId, pageIndex, pageSize, trackChanges); 
            //source= ProductMDL,targer CategoryDto
            var productDto = _mapper.Map<IEnumerable<ProductDto>>(ProductMDL);
            return productDto;
        }

        public async Task<IEnumerable<ProductDto>> GetProductOnSales(bool trackChanges)
        {
            
            var ProductModel = await _repositoryManager.ProductRepository.GetProductOnSales(trackChanges);
            var ProductDto = _mapper.Map<IEnumerable<ProductDto>>(ProductModel);
            return ProductDto;
        }

        public async Task<ProductDto> GetProductPhotoById(int ProductId, bool trackChanges)
        {
            var ProductMDL = await _repositoryManager.ProductRepository.GetProductPhotoById(ProductId, trackChanges);
            //source= ProductMDL,targer CategoryDto
            var productDto = _mapper.Map<ProductDto>(ProductMDL);
            return productDto;
        }

        public async Task<ProductOrderGroupDto> GetProductOrderOnSalesById(int productId, bool trackChanges)
        {
            var productModel = await _repositoryManager.ProductRepository.GetProductOrderOnSalesById(productId, trackChanges);
            var productDto = _mapper.Map<ProductOrderGroupDto>(productModel);
            return productDto;
        }

        public void CreateOrder(OrderForCreateDto orderForCreateDto, OrderDetailForCreateDto orderDetailCreateDtos)
        {
            //insert order
            var order = _mapper.Map<Order>(orderForCreateDto);
            _repositoryManager.OrderRepository.insert(order);
            _repositoryManager.Save();

            //insert order detail
            var orderDetail = _mapper.Map<OrderDetail>(orderDetailCreateDtos);
            orderDetail.OrderId = order.OrderId;
            _repositoryManager.OrderDetailRepository.Insert(orderDetail);
            _repositoryManager.Save();
        }

        public void EditProductPhoto(ProductDto productDto, List<ProductPhotoDto> productPhotoDto)
        {
            // insert product
            var productModel = _mapper.Map<Product>(productDto);
            _repositoryManager.ProductRepository.edit(productModel);
            _repositoryManager.Save();

            // insert photo product
            foreach (var item in productPhotoDto)
            {
                item.PhotoProductId = productModel.ProductId;
                var photoModel = _mapper.Map<ProductPhoto>(item);
                _repositoryManager.ProductPhotoRepository.edit(photoModel);
            }
            _repositoryManager.Save();
        }
    }
}
