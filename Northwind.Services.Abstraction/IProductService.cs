using Northwind.Contracts.Dto.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services.Abstraction
{
    public interface IProductService
    {
        //trackChanges => feature untuk mendekteksi perubahan data diobject category
        Task<IEnumerable<ProductDto>> GetAllProduct(bool trackChanges);

        //craete 1 record with this code
        Task<ProductDto> GetProductById(int ProductId, bool trackChanges);
        Task<IEnumerable<ProductDto>> GetProductPaged(int pageIndex, int pageSize, bool trackChanges);
        void edit(ProductDto productDto);
        void remove(ProductDto productDto);
    }
}
