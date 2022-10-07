
using Northwind.Contracts.Dto.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services.Abstraction
{
    public interface ICategoryService
    {
        //trackChanges => feature untuk mendekteksi peribahan data diobject category
        Task<IEnumerable<CategoryDto>> GetAllCategory(bool trackChanges);

        //craete 1 record with this code
        Task<CategoryDto> GetCategoryById(int categoryId, bool trackChanges);
        void insert(CategoryForCreateDto categoryDto);
        void edit(CategoryDto categoryDto);
        void remove(CategoryDto categoryDto);
    }
}
