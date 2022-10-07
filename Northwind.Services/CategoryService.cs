using AutoMapper;
using Northwind.Contracts.Dto.Category;
using Northwind.Domain.Base;
using Northwind.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Services
{
    public class CategoryService : ICategoryService

    {
        private IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        //depedency injection
        //Blok IRepository and Immapper untuk memunculkan satu
        
        public CategoryService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }//satu



        public void edit(CategoryDto categoryDto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategory(bool trackChanges)
        {
            var categoryModel = await _repositoryManager.CategoryRepository.GetAllCategory(trackChanges);
            //source= categoryModel,targer CategoryDto
            var categoryDto = _mapper.Map<IEnumerable<CategoryDto>>(categoryModel);
            return categoryDto;
        }

        public Task<CategoryDto> GetCategoryById(int categoryId, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public void insert(CategoryForCreateDto categoryDto)
        {
            throw new NotImplementedException();
        }

        public void remove(CategoryDto categoryDto)
        {
            throw new NotImplementedException();
        }
    }
}
