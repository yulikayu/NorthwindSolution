using AutoMapper;
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
    internal class ProductPhotoService : IProductPhotoService
    {
        private IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ProductPhotoService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductPhotoDto>> GetAllProductPhoto(bool trackChanges)
        {
            var OrderMDL = await _repositoryManager.ProductPhotoRepository.GetAllProductPhoto(trackChanges);
            //source= ProductMDL,targer CategoryDto
            var OrderDto = _mapper.Map<IEnumerable<ProductPhotoDto>>(OrderMDL);
            return OrderDto;
        }

        public async Task<ProductPhotoDto> GetProductPhotoById(int ProductPhotoId, bool trackChanges)
        {
            var OrderMDL = await _repositoryManager.ProductPhotoRepository.GetProductPhotoById(ProductPhotoId, trackChanges);
            //source= ProductMDL,targer CategoryDto
            var OrderDto = _mapper.Map<ProductPhotoDto>(OrderMDL);
            return OrderDto;
        }

        public void insert(ProductForCreatDto productForCreatDto)
        {
            var prophoModel = _mapper.Map<ProductPhoto>(productForCreatDto);
            _repositoryManager.ProductPhotoRepository.insert(prophoModel);
            _repositoryManager.Save();
        }

        public void edit(ProductPhotoDto productPhotoDto)
        {
            var prophoModel = _mapper.Map<ProductPhoto>(productPhotoDto);
            _repositoryManager.ProductPhotoRepository.edit(prophoModel);
            _repositoryManager.Save();
        }

        public void remove(ProductPhotoDto productPhotoDto)
        {
            var prophoModel = _mapper.Map<ProductPhoto>(productPhotoDto);
            _repositoryManager.ProductPhotoRepository.remove(prophoModel);
            _repositoryManager.Save();
        }
    }
}
