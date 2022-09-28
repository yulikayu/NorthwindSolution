using AutoMapper;
using Northwind.Contracts.Dto.Category;
using Northwind.Domain.Base;
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
            throw new NotImplementedException();
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
            var ProductMDL = await _repositoryManager.ProductRepository.GetProductById(ProductId,trackChanges);
            //source= ProductMDL,targer CategoryDto
            var productDto = _mapper.Map<ProductDto>(ProductMDL);
            return productDto;
        }

        public void remove(ProductDto productDto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductDto>> GetProductPaged(int pageIndex, int pageSize, bool trackChanges)
        {
            var ProductMDL = await _repositoryManager.ProductRepository.GetProductPaged(pageIndex, pageSize,trackChanges);
            //source= ProductMDL,targer CategoryDto
            var productDto = _mapper.Map<IEnumerable<ProductDto>>(ProductMDL);
            return productDto;
        }
    }
}
